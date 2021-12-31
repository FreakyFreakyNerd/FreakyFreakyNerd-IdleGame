using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Producer
{
    public interface ITickable
    {
        public void OnTick(float deltaT);
    }
}