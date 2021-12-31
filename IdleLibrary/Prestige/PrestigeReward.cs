using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Prestige
{
    public abstract class IReward
    {
        internal IContainer BasedOn;
        internal IContainer RewardItem;
        internal BigNum Amount;
        
        public IReward(IContainer basedon, IContainer rewarditem)
        {
            BasedOn = basedon;
            RewardItem = rewarditem;
        }

        public abstract void UpdateValue();

        public virtual void ApplyReward()
        {
            RewardItem.AddAmount(Amount);
        }
    }

    public class ExponentialReward : IReward
    {
        private BigNum Exponent;

        public ExponentialReward(IContainer basedon, IContainer rewarditem, BigNum exponent) : base(basedon, rewarditem)
        {
            Exponent = exponent;
        }

        public override void UpdateValue()
        {
            Amount = BigNum.Pow(BasedOn.GetAmount(), Exponent);
        }
    }

    public class RewardHandler
    {

    }
}
