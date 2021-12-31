using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Prestige
{
    public class Reset
    {
        public delegate void ResetDelegate(Reset reset);
        public static ResetDelegate OnReset;

        private string ID;
        public Reset(string id)
        {
            ID = id;
        }

        public void DoReset()
        {
            if (OnReset != null)
                OnReset(this);
        }
    }
}
