using FreakyFreakyNerd.IdleLibrary.Achievement;
using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Currency;
using FreakyFreakyNerd.IdleLibrary.Producer;
using FreakyFreakyNerd.Language;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Upgrade
{
    public class GameUpgrade : IHookable, IBuyable, IContainer, ILocalizable
    {
        private string ID;
        public string DisplayName { private set; get; }

        private List<Effect> Effects = new List<Effect>();
        private string BuyKey;
        private BigNum Bought = new BigNum();
        private bool Applied = false;

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
    }
}
