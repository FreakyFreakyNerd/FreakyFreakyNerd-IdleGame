using FreakyFreakyNerd.IdleLibrary.BigMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Upgrade
{
    public enum EffectCombineOperation
    {
        ADDITION,
        MULTIPLICATION
    }
    public struct EffectsListInit
    {
        public EffectType type;
        public EffectCombineOperation operation;

        public EffectsListInit(EffectType type, EffectCombineOperation operation)
        {
            this.type = type;
            this.operation = operation;
        }
    }
    public class EffectsList
    {
        private EffectType[] PossibleTypes;
        private class EffectHolder
        {
            List<Effect> Effects;
            public BigNum Value { private set; get; }
            EffectCombineOperation Operation;

            public EffectHolder(EffectCombineOperation oper)
            {
                Operation = oper;
                Effects = new List<Effect>();
                Value = new BigNum();
                RecalcualteValue();
            }

            public override string ToString()
            {
                return String.Format("EffectType:{0}, Effects:{1}, Value:{2}", Operation, Effects.Count, Value);
            }

            public void RecalcualteValue()
             {
                var value = new BigNum();
                switch (Operation)
                {
                    case EffectCombineOperation.ADDITION:
                        this.Effects.ForEach((eff) =>
                        {
                            value += eff.GetEffectValue();
                        });
                        break;
                    case EffectCombineOperation.MULTIPLICATION:
                        value += 1;
                        this.Effects.ForEach((eff) =>
                        {
                            value *= eff.GetEffectValue();
                        });
                        break;
                    default:
                        break;
                }
                Value = value;
            }
            public void AddEffect(Effect effect)
            {
                if (!Effects.Contains(effect))
                {
                    Effects.Add(effect);
                    RecalcualteValue();
                }
            }
            public void RemoveEffect(Effect effect)
            {
                if (Effects.Contains(effect))
                {
                    Effects.Remove(effect);
                    RecalcualteValue();
                }
            }
        }
        private Dictionary<EffectType, EffectHolder> Effects = new Dictionary<EffectType, EffectHolder>(); 
        public EffectsList(params EffectsListInit[] possibleTypes)
        {
            List<EffectType> possible = new List<EffectType>();

            for(int i = 0; i < possibleTypes.Length; i++)
            {
                if (possible.Contains(possibleTypes[i].type))
                    continue;
                possible.Add(possibleTypes[i].type);
                Effects.Add(possibleTypes[i].type, new EffectHolder(possibleTypes[i].operation));
            }

            PossibleTypes = possible.ToArray();
        }

        public bool AddEffect(Effect effect)
        {
            if (!PossibleTypes.Contains(effect.GetEffectType()))
                return false;
            Effects[effect.GetEffectType()].AddEffect(effect);
            return true;
        }
        public bool RemoveEffect(Effect effect)
        {
            if (!PossibleTypes.Contains(effect.GetEffectType()))
                return false;
            Effects[effect.GetEffectType()].RemoveEffect(effect);
            return true;
        }

        public BigNum GetValue(EffectType effectType)
        {
            if (!PossibleTypes.Contains(effectType))
                return new BigNum();

            Logger.Log(Effects[effectType]);
            return Effects[effectType].Value;
        }

        public void UpdateValue()
        {
            foreach (EffectHolder i in Effects.Values)
            {
                i.RecalcualteValue();
            }
        }

        public void UpdateValue(EffectType type)
        {
            if (!PossibleTypes.Contains(type))
                return;
            Effects[type].RecalcualteValue();
        }
    }
}
