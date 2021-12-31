using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Currency;
using FreakyFreakyNerd.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Cost
{
    public abstract class ICost
    {
        internal BigNum Cost;
        internal IContainer CostObject;

        public ICost(IContainer costObject)
        {
            CostObject = costObject;
            Cost = new BigNum();
        }

        public virtual bool HasCost()
        {
            return CostObject.HasAmount(Cost);
        }

        public virtual BigNum GetCost()
        {
            return Cost;
        }

        public virtual void Buy()
        {
            CostObject.RemoveAmount(Cost);
        }

        public virtual string GetFormattedText()
        {
            return LanguageManager.GetLocalizedText("cost.formatcost", Cost, (CostObject as ILocalizable).GetDisplayName());
        }

        public abstract void UpdateCost(BigNum bought, BigNum buyAmount);
        public abstract BigNum GetMaxBuyable();
    }

    public class LinearCost : ICost
    {
        BigNum Starting, Increase;
        public LinearCost(IContainer costObject, BigNum starting, BigNum increase) : base(costObject)
        {
            Starting = starting;
            Increase = increase;
        }

        public override BigNum GetMaxBuyable()
        {
            throw new NotImplementedException();
        }

        public override void UpdateCost(BigNum bought, BigNum buyAmount)
        {
            if (buyAmount == new BigNum(1))
                Cost = Starting + Increase * bought;
            else
                Cost = Starting * buyAmount; // + function of increase and buyamount;
        }
    }
}
