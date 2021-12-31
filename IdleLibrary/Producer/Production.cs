using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Currency;
using FreakyFreakyNerd.IdleLibrary.Upgrade;
using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Producer
{
    public interface Production : IEffectable
    {
        public void Produce(float deltaT);

        public void UpdateProduction(BigNum amount);
        public BigNum GetProductionValue();
    }

    public class LinearProduction : Production
    {
        private BigNum Starting, Increase;
        private IGeneratable Produced;
        private BigNum Production;
        private EffectsList AppliedEffects = new EffectsList(   new EffectsListInit(EffectType.STARTINGPRODUCTIONADDITION, EffectCombineOperation.ADDITION), 
                                                                new EffectsListInit(EffectType.PRODUCTIONPERADDITION, EffectCombineOperation.ADDITION),
                                                                new EffectsListInit(EffectType.PRODUCTIONPERMULTIPLICATION, EffectCombineOperation.MULTIPLICATION));
        private BigNum Queued;

        public LinearProduction(IGeneratable produces, BigNum starting, BigNum increase)
        {
            Produced = produces;
            Starting = starting;
            Increase = increase;
            Production = new BigNum(starting);
        }
        public LinearProduction(IGeneratable produces, double starting, double increase) : this(produces, new BigNum(starting), new BigNum(increase)) { }

        public void ApplyEffect(Effect effect)
        {
            AppliedEffects.AddEffect(effect);
            UpdateProduction();
        }

        public void RemoveEffect(Effect effect)
        {
            AppliedEffects.RemoveEffect(effect);
            UpdateProduction();
        }

        public void Produce(float deltaT)
        {
            Produced.AddGenerated(Production * deltaT);
        }

        private BigNum GetStartingValue()
        {
            return Starting + AppliedEffects.GetValue(EffectType.STARTINGPRODUCTIONADDITION);
        }
        private BigNum GetIncreaseValue()
        {
            return (Increase + AppliedEffects.GetValue(EffectType.PRODUCTIONPERADDITION)) * AppliedEffects.GetValue(EffectType.PRODUCTIONPERMULTIPLICATION);
        }

        public void UpdateProduction(BigNum amount)
        {
            if (!amount.Equals(null))
                Production = GetStartingValue() + GetIncreaseValue() * amount;
            else
                Production = GetStartingValue();
            Queued = amount;
        }
        public void UpdateProduction()
        {
            if(Queued == null)
                Production = GetStartingValue();
            if (!Queued.Equals(null))
                Production = GetStartingValue() + GetIncreaseValue() * Queued;
        }

        public BigNum GetProductionValue()
        {
            return Production;
        }
    }
}