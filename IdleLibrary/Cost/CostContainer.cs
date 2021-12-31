using FreakyFreakyNerd.IdleLibrary.BigMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Cost
{
    public class CostContainer
    {
        private List<ICost> Costs = new List<ICost>();
        private string BuyKey;

        public CostContainer(string buyKey, params ICost[] costs)
        {
            BuyKey = buyKey;
            AddCosts(costs);
        }

        public void AddCosts(ICost[] costs)
        {
            for(int i = 0; i < costs.Length; i++)
            {
                AddCost(costs[i]);
            }
        }
        public void AddCost(ICost cost)
        {
            if (!Costs.Contains(cost))
                Costs.Add(cost);
        }
        public void RemoveCosts(ICost[] costs)
        {
            for (int i = 0; i < costs.Length; i++)
            {
                RemoveCost(costs[i]);
            }
        }

        public ICost GetCost(int ind)
        {
            if (ind < Costs.Count)
                return Costs[ind];
            return null;
        }

        public void RemoveCost(ICost cost)
        {
            if (Costs.Contains(cost))
                Costs.Remove(cost);
        }

        public bool CanBuy()
        {
            foreach(ICost c in Costs)
            {
                if (!c.HasCost())
                    return false;
            }
            return true;
        }

        public BigNum GetMaxBuyable()
        {
            BigNum max = null;
            foreach(ICost c in Costs)
            {
                if (max == null || c.GetMaxBuyable() < max)
                    max = c.GetMaxBuyable();
            }
            if (max == null)
                return new BigNum();
            return max;
        }

        public BigNum GetBuyAmount()
        {
            BigNum buy = BuyAmountHandler.GetBuyAmount(BuyKey);
            if (buy > 0)
                return buy;
            return GetMaxBuyable();
        }

        public void Buy()
        {
            Costs.ForEach((cost) =>
            {
                cost.Buy();
            });
        }

        public void UpdateCosts(BigNum bought)
        {
            Costs.ForEach((cost) =>
            {
                cost.UpdateCost(bought, GetBuyAmount());
            });
        }
    }
}
