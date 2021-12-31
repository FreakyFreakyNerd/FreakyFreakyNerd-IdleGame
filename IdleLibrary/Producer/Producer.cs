using FreakyFreakyNerd.IdleLibrary.Achievement;
using FreakyFreakyNerd.IdleLibrary.BigMath;
using FreakyFreakyNerd.IdleLibrary.Cost;
using FreakyFreakyNerd.IdleLibrary.Currency;
using FreakyFreakyNerd.IdleLibrary.Upgrade;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.Producer
{
    public class GameProducer : IHookable, ITickable, IBuyable, IContainer, IEffectable
    {
        private string ID;

        private List<Production> Productions = new List<Production>();
        private string BuyKey;
        private BigNum Bought = new BigNum();
        private List<Effect> AppliedEffects = new List<Effect>();
        private CostContainer Costs;

        public Production GetProduction(int v)
        {
            if(v < Productions.Count)
                return Productions[v];
            return null;
        }

        public CostContainer GetCosts()
        {
            return Costs;
        }

        public GameProducer(string id, string buyKey)
        {
            id = ID;
            BuyKey = buyKey;
            Costs = new CostContainer(BuyKey);
        }
        public GameProducer(string id, string buyKey, Production[] prods, ICost[] costs) : this(id, buyKey)
        {
            AddProductionArray(prods);
            AddCostArray(costs);
        }

        public GameProducer AddProduction(Production prod)
        {
            if (!Productions.Contains(prod))
                Productions.Add(prod);
            return this;
        }
        public GameProducer AddProductions(params Production[] prod)
        {
            AddProductionArray(prod);
            return this;
        }
        public GameProducer AddProductionArray(Production[] prod)
        {
            for(int i = 0; i < prod.Length; i++)
            {
                AddProduction(prod[i]);
            }
            return this;
        }
        public GameProducer AddCost(ICost cost)
        {
            Costs.AddCost(cost);
            UpdateCost();
            return this;
        }
        public GameProducer AddCosts(params ICost[] prod)
        {
            AddCostArray(prod);
            return this;
        }
        public GameProducer AddCostArray(ICost[] prod)
        {
            Costs.AddCosts(prod);
            return this;
        }
        public override void SetupHooks()
        {
            GameManager.Tick += OnTick;
        }

        public void OnTick(float deltaT)
        {
            Logger.Log("Producer Produce");
            this.Produce(deltaT);
        }

        private void Produce(float deltaT)
        {
            this.Productions.ForEach((prod) =>
            {
                prod.Produce(deltaT);
            });
        }

        private void UpdateProduction()
        {
            this.Productions.ForEach((prod) =>
            {
                prod.UpdateProduction(GetAmount());
            });
        }

        public void Buy()
        {
            if (CanBuy())
            {
                Bought += BuyAmount();
                Costs.Buy();
                UpdateCost();
                UpdateProduction();
            }
        }

        public void UpdateCost()
        {
            Costs.UpdateCosts(GetBought());
        }

        public BigNum GetBought()
        {
            return Bought;
        }

        public bool CanBuy()
        {
            return Costs.CanBuy();
        }

        public BigNum BuyAmount()
        {
            return Costs.GetBuyAmount();
        }

        public virtual BigNum GetAmount()
        {
            return GetBought();
        }

        public virtual void AddAmount(BigNum amount)
        {
            Bought += amount;
        }

        public bool HasAmount(BigNum amount)
        {
            return Bought >= amount;
        }

        public void ApplyEffect(Effect effect)
        {
            if (AppliedEffects.Contains(effect))
                return;
            AppliedEffects.Add(effect);
            Productions.ForEach((prod) =>
            {
                prod.ApplyEffect(effect);
            });
        }

        public void RemoveEffect(Effect effect)
        {
            if (!AppliedEffects.Contains(effect))
                return;
            AppliedEffects.Remove(effect);
            Productions.ForEach((prod) =>
            {
                prod.RemoveEffect(effect);
            });
        }

        public virtual void RemoveAmount(BigNum amount)
        {
            Bought -= amount;
        }
    }
}