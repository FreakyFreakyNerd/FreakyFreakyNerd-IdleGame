using FreakyFreakyNerd.IdleLibrary.BigMath;
using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Currency
{
    public interface IContainer
    {
        public BigNum GetAmount();
        public void AddAmount(BigNum amount);
        public void RemoveAmount(BigNum amount);
        public bool HasAmount(BigNum amount);
    }
}