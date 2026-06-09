using System;
using System.Threading;
using Unity.Async;
using UnityEngine;

namespace Unity.Async
{
    public class WaitUntilOwner : IWaitable, IInterruptible
    {
        private Func<bool> done;
        private bool isDone;
        private UnityEngine.Object owner;
        
    

        public WaitUntilOwner(Func<bool> done, UnityEngine.Object owner)
        {
            this.done = done;
            this.owner = owner;
        }

        public bool IsDone
        {
            get
            {
                if (!isDone)
                {
                    if (done())
                    {
                        isDone = true;
                    }
                }
                return isDone;
            }
        }

        public bool Interrupt =>   !owner;
    }

}