using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Currency;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Achievement
{
    public interface IRequirement
    {
        public bool HasRequirement();
    }

    public class NumberRequirement : IRequirement
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

    public class RequirementContainer
    {
        private List<IRequirement> Requirements = new List<IRequirement>();

        public void AddRequirement(IRequirement requirement)
        {
            if (!Requirements.Contains(requirement))
                Requirements.Add(requirement);
        }

        public void AddRequirements(IRequirement[] requirements)
        {
            for(int i = 0; i < requirements.Length; i++)
            {
                AddRequirement(requirements[i]);
            }
        }

        public bool HasRequirements()
        {
            foreach(IRequirement req in Requirements)
            {
                if (!req.HasRequirement())
                    return false;
            }
            return true;
        }
    }
}