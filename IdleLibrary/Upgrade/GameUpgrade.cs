using FreakyFreakyNerd.IdleLibrary.Achievement;
using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Cost;
using FreakyFreakyNerd.IdleLibrary.Currency;
using FreakyFreakyNerd.IdleLibrary.Prestige;
using FreakyFreakyNerd.IdleLibrary.Producer;
using FreakyFreakyNerd.Language;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Upgrade
{
    public class GameUpgrade : IHookable, IBuyable, IContainer, ILocalizable, IResetable
    {
        private string ID;
        public string DisplayName { private set; get; }

        private List<Effect> Effects = new List<Effect>();
        private string BuyKey;
        private BigNum Bought = new BigNum();
        private bool Applied = false;
        private CostContainer Costs;
        private ResetImmunity Immunity = new ResetImmunity();

        public GameUpgrade(string id, string buykey)
        {
            ID = id;
            BuyKey = buykey;
            Costs = new CostContainer(BuyKey);
        }

        private void Apply()
        {
            Effects.ForEach((eff) => {
                eff.Apply();
            });
            Applied = true;
        }
        private void UnApply()
        {
            Effects.ForEach((eff) => {
                eff.UnApply();
            });
            Applied = false;
        }

        public virtual void AddAmount(BigNum amount)
        {
            this.Bought += amount;
        }

        public void Buy()
        {
            if (!Applied)
                Apply();
        }

        public BigNum BuyAmount()
        {
            return new BigNum(1);
        }
        public void UpdateCost()
        {
            throw new System.NotImplementedException();
        }
        public void UpdateEffects()
        {
            Effects.ForEach((eff) =>
            {
                eff.UpdateValue(Bought);
            });
        }

        public bool CanBuy()
        {
            throw new System.NotImplementedException();
        }

        public virtual BigNum GetAmount()
        {
            return Bought;
        }

        public BigNum GetBought()
        {
            return Bought;
        }

        public virtual bool HasAmount(BigNum amount)
        {
            return Bought >= amount;
        }

        public void OnLanguageLoad()
        {
            DisplayName = LanguageManager.GetLocalizedText(string.Format("upgrade.{0}.name", ID));
        }


        public override void SetupHooks()
        {
            LanguageManager.OnLanguageLoad += OnLanguageLoad;
        }

        public string GetDisplayName()
        {
            return DisplayName;
        }

        public virtual void RemoveAmount(BigNum amount)
        {
            Bought -= amount;
        }

        public virtual void OnReset(Reset reset)
        {
            if (Immunity.Immune(reset))
                return;
            Bought = new BigNum();
            UpdateCost();
            UpdateEffects();
        }

        public GameUpgrade AddImmunity(Reset reset)
        {
            Immunity.AddImmunity(reset);
            return this;
        }

        public GameUpgrade AddImmunitys(params Reset[] resets)
        {
            Immunity.AddImmunitys(resets);
            return this;
        }

        public GameUpgrade AddImmunityArray(Reset[] resets)
        {
            Immunity.AddImmunitys(resets);
            return this;
        }

        public GameUpgrade RemoveImmunity(Reset reset)
        {
            Immunity.RemoveImmunity(reset);
            return this;
        }

        public GameUpgrade RemoveImmunitys(params Reset[] resets)
        {
            Immunity.RemoveImmunitys(resets);
            return this;
        }

        public GameUpgrade RemoveImmunityArray(Reset[] resets)
        {
            Immunity.RemoveImmunitys(resets);
            return this;
        }

        public GameUpgrade AddCost(ICost cost)
        {
            Costs.AddCost(cost);
            UpdateCost();
            return this;
        }
        public GameUpgrade AddCosts(params ICost[] prod)
        {
            AddCostArray(prod);
            return this;
        }
        public GameUpgrade AddCostArray(ICost[] prod)
        {
            Costs.AddCosts(prod);
            return this;
        }
        public GameUpgrade RemoveCost(ICost cost)
        {
            Costs.RemoveCost(cost);
            UpdateCost();
            return this;
        }
        public GameUpgrade RemoveCosts(params ICost[] prod)
        {
            RemoveCostArray(prod);
            return this;
        }
        public GameUpgrade RemoveCostArray(ICost[] prod)
        {
            Costs.RemoveCosts(prod);
            return this;
        }
    }
}
