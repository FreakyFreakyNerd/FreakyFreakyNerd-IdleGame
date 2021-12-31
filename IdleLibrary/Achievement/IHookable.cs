using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Achievement
{
    public abstract class IHookable
    {
        public IHookable()
        {
            SetupHooks();
        }

        public abstract void SetupHooks();
    }
}