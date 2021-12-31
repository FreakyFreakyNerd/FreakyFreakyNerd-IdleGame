using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.BigMath
{
    public interface NumberFormatter
    {
        public abstract string Format(BigNum num);
    }

    public class TestingFormatter : NumberFormatter
    {
        private double ExponentCutOff = 6;
        public string Format(BigNum num)
        {
            if (num.exponent > ExponentCutOff)
                return string.Format("{0}e{1}", num.mantissa.ToString("0.00"), num.exponent.ToString("0"));
            else
                return string.Format("{0}", num.ToDouble().ToString("0.00"));
        }
    }
}