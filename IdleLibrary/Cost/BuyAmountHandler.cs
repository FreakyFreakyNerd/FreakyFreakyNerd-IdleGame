using FreakyFreakyNerd.IdleLibrary.BigMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakyFreakyNerd.IdleLibrary.Cost
{
    public static class BuyAmountHandler
    {
        private static Dictionary<string, BigNum> BuyAmounts = new Dictionary<string, BigNum>();
        public static void RegisterBuyAmount(string key)
        {
            if (!BuyAmounts.ContainsKey(key))
                BuyAmounts.Add(key, new BigNum(1));
        }

        public static BigNum GetBuyAmount(string key)
        {
            if (BuyAmounts.ContainsKey(key))
                return BuyAmounts[key];
            return new BigNum(1);
        }
    }
}
