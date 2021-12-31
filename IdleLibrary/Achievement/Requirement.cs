using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Currency;
using UnityEditor;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Achievement
{
    public interface Requirement
    {
        public bool HasRequirement();
    }

    public class NumberRequirement : Requirement
    {
        private IContainer container;
        private BigNum RequiredAmount;
        public NumberRequirement(IContainer cont, BigNum requirement)
        {
            container = cont;
            RequiredAmount = requirement;
        }

        public bool HasRequirement()
        {
            return container.HasAmount(RequiredAmount);
        }
    }
}