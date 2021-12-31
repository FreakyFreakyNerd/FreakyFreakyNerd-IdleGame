using FreakyFreakyNerd.IdleLibrary.BigMath;
using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Currency
{
    public interface IGeneratable
    {
        public void AddGenerated(BigNum amount);
        public BigNum GetGenerated();
    }
}