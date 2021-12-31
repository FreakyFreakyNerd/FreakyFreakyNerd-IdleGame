using FreakyFreakyNerd.IdleLibrary.Currency;
using FreakyFreakyNerd.Language;
using FreakyFreakyNerd.IdleLibrary.BigMath;
using System.Collections;
using UnityEngine;
using FreakyFreakyNerd.IdleLibrary.Achievement;

namespace FreakyFreakyNerd.IdleLibrary.Currency
{
    public class GameCurrency : IHookable, ILocalizable, IContainer, IGeneratable, IGained
    {
        private string ID;
        public string DisplayName { private set; get; }
        private BigNum StartingAmount;

        public GameCurrency(string id)
        {
            ID = id;
            Amount = new BigNum();
            Gained = new BigNum();


        }
        public GameCurrency(string id, BigNum starting) : this(id)
        {
            StartingAmount = starting;
            Amount = new BigNum(starting);
            Gained = new BigNum(starting);
        }

        private BigNum Amount = new BigNum();
        private BigNum Gained = new BigNum();


        public void AddAmount(BigNum amount)
        {
            Amount += amount;
            Gained += amount;
        }

        public void AddGenerated(BigNum amount)
        {
            this.AddAmount(amount);
        }

        public BigNum GetAmount()
        {
            return Amount;
        }

        public BigNum GetGained()
        {
            return Gained;
        }

        public BigNum GetGenerated()
        {
            return GetAmount();
        }

        public bool HasAmount(BigNum amount)
        {
            return Amount >= amount;
        }

        public void OnLanguageLoad()
        {
            DisplayName = LanguageManager.GetLocalizedText(string.Format("currency.{0}.name", ID));
        }

        public override void SetupHooks()
        {
            LanguageManager.OnLanguageLoad += OnLanguageLoad;
        }

        public void RemoveAmount(BigNum amount)
        {
            Amount -= amount;
        }

        public string GetDisplayName()
        {
            return DisplayName;
        }
    }
}