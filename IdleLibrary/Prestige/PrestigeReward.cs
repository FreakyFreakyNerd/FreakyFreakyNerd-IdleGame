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
        private List<IReward> Rewards = new List<IReward>();

        public void AddReward(IReward reward)
        {
            if (!Rewards.Contains(reward))
                Rewards.Add(reward);
        }
        public void AddRewards(IReward[] rewards)
        {
            for(int i = 0; i < rewards.Length; i++)
            {
                AddReward(rewards[i]);
            }
        }
        public void RemoveReward(IReward reward)
        {
            if (Rewards.Contains(reward))
                Rewards.Remove(reward);
        }
        public void RemoveRewards(IReward[] rewards)
        {
            for (int i = 0; i < rewards.Length; i++)
            {
                RemoveReward(rewards[i]);
            }
        }

        public void UpdateRewards()
        {
            Rewards.ForEach((rew) =>
            {
                rew.UpdateValue();
            });
        }
        public void ApplyRewards()
        {
            UpdateRewards();
            Rewards.ForEach((rew) =>
            {
                rew.ApplyReward();
            });
        }
    }
}
