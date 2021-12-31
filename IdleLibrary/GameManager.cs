using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary
{
    public static class GameManager
    {
        public delegate void VoidDelegate();
        public static VoidDelegate DelayedTick;

        public delegate void FloatDelegate(float deltaT);
        public static FloatDelegate Tick;
    }
}