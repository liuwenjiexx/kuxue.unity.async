using NUnit.Framework;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.Async.Tests
{
    public class OwnerTest
    {
        [UnityTest]
        public IEnumerator WaitUntil_Owner_Null()
        {
            yield return new Func<Task>(async () =>
            {
                try
                {
                    float timeout = Time.time + 0.5f;
                    await new WaitUntilOwner(() =>
                    {
                        if (Time.time > timeout)
                            throw new TimeoutException();
                        return false;
                    }, null);
                    Assert.Fail();
                }
                catch (ThreadInterruptedException ex)
                {
                    Debug.Log(ex.Message);
                }
                catch (InterruptedException ex)
                {
                    Debug.Log(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    Assert.Fail();
                    return;
                }

            })().AsRoutine();
        }

        [UnityTest]
        public IEnumerator WaitUntil_Owner_Destroy()
        {
            yield return new Func<Task>(async () =>
            {
                GameObject owner = new GameObject();
                UnityEngine.Object.Destroy(owner, 0.1f);
                Assert.IsNotNull(owner);
                try
                {
                    float timeout = Time.time + 0.5f;
                    await new WaitUntilOwner(() =>
                    {
                        if (Time.time > timeout)
                            throw new TimeoutException();
                        return false;
                    }, owner);
                    Assert.Fail();
                }
                catch (ThreadInterruptedException ex)
                {
                    Debug.Log(ex.Message);
                }
                catch (InterruptedException ex)
                {
                    Debug.Log(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    if (owner)
                    {
                        Debug.Log("owner not null");
                    }
                    else
                    {
                        Debug.Log("owner null");
                    }
                    Assert.Fail();
                }

            })().AsRoutine();
        }

        [UnityTest]
        public IEnumerator WaitUntil_Owner()
        {
            yield return new Func<Task>(async () =>
            {
                GameObject owner = new GameObject();
                UnityEngine.Object.Destroy(owner,1f);
                Assert.IsNotNull(owner);
                if (owner)
                {
                    Debug.Log("owner not null");
                }
                else
                {
                    Debug.Log("owner null");
                }
                try
                {
                    float doneTime = Time.time + 0.5f;
                    await new WaitUntilOwner(() =>
                    {
                        if (Time.time > doneTime)
                            return true;
                        return false;
                    }, owner);
                    if (owner)
                    {
                        Debug.Log("owner not null");
                    }
                    else
                    {
                        Debug.Log("owner null");
                    }
                    Assert.IsNotNull(owner);
                } 
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
              
                    Assert.Fail();
                }
                UnityEngine.Object.Destroy(owner);
            })().AsRoutine();
        }
    }
}