using FreakyFreakyNerd.IdleLibrary.BigMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Upgrade
{
    public enum EffectType
    {
        //Producer
        STARTINGPRODUCTIONADDITION,
        PRODUCTIONPERADDITION,
        PRODUCTIONPERMULTIPLICATION

        //Upgrade
    }
    public abstract class Effect
    {
        private EffectType _EffectType;
        private List<IEffectable> Effects;

        private bool Applied = false;

        public Effect(EffectType type, List<IEffectable> effecton)
        {
            Effects = effecton;
            _EffectType = type;
        }

        public virtual void Apply()
        {
            Effects.ForEach((eff) =>
            {
                eff.ApplyEffect(this);
            });
            Applied = true;
        }
        public virtual void UnApply()
        {
            if (!Applied)
                Logger.Log("Effect Being Unapplied Without Being Applied");
            Effects.ForEach((eff) =>
            {
                eff.RemoveEffect(this);
            });
            Applied = false;
        }
        public virtual EffectType GetEffectType()
        {
            return _EffectType;
        } 

        public abstract void UpdateValue(BigNum amount);

        public abstract BigNum GetEffectValue();
    }

    public class StaticEffect : Effect
    {
        BigNum Value;
        public StaticEffect(EffectType type, List<IEffectable> effecton, BigNum value) : base(type, effecton)
        {
            Value = value;
        }

        public override BigNum GetEffectValue()
        {
            return Value;
        }

        public override void UpdateValue(BigNum amount)
        {
            return;
        }
    }
}
