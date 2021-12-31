using FreakyFreakyNerd.IdleLibrary.BigMath;
using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Producer
{
    public interface IBuyable
    {
        public BigNum GetBought();
        public void Buy();
        public void UpdateCost();
        public bool CanBuy();
        public BigNum BuyAmount();
    }
}