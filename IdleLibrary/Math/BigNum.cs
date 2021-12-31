using System;
using System.Collections;
using UnityEngine;

namespace FreakyFreakyNerd.IdleLibrary.BigMath
{
    public class BigNum
    {
        private static double PRECISION = 7;
        private static NumberFormatter ActiveFormatter = new TestingFormatter();

        internal double mantissa, exponent;

        public BigNum() { mantissa = exponent = 0; }
        public BigNum(double mantissa) { this.mantissa = mantissa; exponent = 0; AdjustMantissa(); }
        public BigNum(double mantissa, double exponent) { this.mantissa = mantissa; this.exponent = exponent; AdjustMantissa(); }
        public BigNum(BigNum num) { this.mantissa = num.mantissa; this.exponent = num.exponent; AdjustMantissa(); }

        public override string ToString()
        {
            return ActiveFormatter.Format(this);
        }

        private void AdjustMantissa()
        {
            if (mantissa < Math.Pow(10, -PRECISION) && mantissa > -Math.Pow(10, -PRECISION))
                return;
            if (WithinNotation(mantissa))
                return;
            double pow = Math.Log10(Math.Abs(mantissa));

            pow = Math.Floor(pow);

            mantissa = mantissa / Math.Pow(10, pow);
            exponent += pow;
        }

        private BigNum Negate()
        {
            this.mantissa = -mantissa;
            return this;
        }

        public double ToDouble()
        {
            return mantissa * Math.Pow(10, exponent);
        }

        internal static bool WithinNotation(double mant)
        {
            return (1 < mant && mant < 10) || (mant < -1 && mant > -10);
        }
        internal static bool WithinPrecision(double mant)
        {
            return mant < Math.Pow(10, -PRECISION) && mant > -Math.Pow(10, -PRECISION);
        }

        public static BigNum operator +(BigNum a, BigNum b)
        {
            return Add(a, b);
        }
        public static BigNum operator +(BigNum a, double b)
        {
            return Add(a, new BigNum(b));
        }
        public static BigNum operator +(double a, BigNum b)
        {
            return Add(new BigNum(a), b);
        }
        public static BigNum operator -(BigNum a, BigNum b)
        {
            return Subtract(a, b);
        }
        public static BigNum operator *(BigNum a, BigNum b)
        {
            return Multiply(a,b);
        }
        public static BigNum operator *(double a, BigNum b)
        {
            return Multiply(new BigNum(a), b);
        }
        public static BigNum operator *(BigNum a, double b)
        {
            return Multiply(a, new BigNum(b));
        }
        public static BigNum operator /(BigNum a, BigNum b)
        {
            return Divide(a, b);
        }
        public static BigNum operator /(double a, BigNum b)
        {
            return Divide(new BigNum(a), b);
        }
        public static BigNum operator /(BigNum a, double b)
        {
            return Divide(a, new BigNum(b));
        }
        public static BigNum operator ^(BigNum a, BigNum b)
        {
            return Pow(a, b);
        }
        public static BigNum operator ^(double a, BigNum b)
        {
            return Pow(new BigNum(a), b);
        }
        public static BigNum operator ^(BigNum a, double b)
        {
            return Pow(a, new BigNum(b));
        }

        public static bool operator ==(BigNum a, BigNum b)
        {
            return Equal(a, b);
        }
        public static bool operator !=(BigNum a, BigNum b)
        {
            return !Equal(a, b);
        }
        public static bool operator >=(BigNum a, BigNum b)
        {
            return GreaterThan(a, b) || Equal(a, b);
        }
        public static bool operator <=(BigNum a, BigNum b)
        {
            return LessThan(a, b) || Equal(a, b);
        }
        public static bool operator >(BigNum a, BigNum b)
        {
            return GreaterThan(a, b);
        }
        public static bool operator >(BigNum a, double b)
        {
            return GreaterThan(a, new BigNum(b));
        }
        public static bool operator <(BigNum a, BigNum b)
        {
            return LessThan(a, b);
        }
        public static bool operator <(BigNum a, double b)
        {
            return LessThan(a, new BigNum(b));
        }

        public static BigNum Add(BigNum a, BigNum b)
        {
            if (a.exponent - b.exponent > PRECISION)
                return a;
            if (b.exponent - a.exponent > PRECISION)
                return b;
            return new BigNum(a.mantissa * Math.Pow(10, a.exponent - b.exponent) + b.mantissa, b.exponent);
        }
        public static BigNum Subtract(BigNum a, BigNum b)
        {
            if (a.exponent - b.exponent > PRECISION)
                return a;
            if (b.exponent - a.exponent > PRECISION)
                return b.Negate();
            return new BigNum(a.mantissa * Math.Pow(10, a.exponent - b.exponent) - b.mantissa, b.exponent);
        }

        public static BigNum Multiply(BigNum a, BigNum b)
        {
            return new BigNum(a.mantissa * b.mantissa, a.exponent + b.exponent);
        }
        public static BigNum Divide(BigNum a, BigNum b)
        {
            return new BigNum(a.mantissa / b.mantissa, a.exponent - b.exponent);
        }
        public static BigNum Pow(BigNum a, BigNum b)
        {
            return new BigNum(Math.Pow(10, b.mantissa * Math.Log10(a.mantissa)), (a.exponent * b).ToDouble());
        }
        public static BigNum Log10(BigNum a)
        {
            return new BigNum(Math.Log10(a.mantissa) + a.exponent);
        }
        public static BigNum Log(BigNum a, BigNum b)
        {
            return Log10(a)/Log10(b);
        }
        public static bool GreaterThan(BigNum a, BigNum b)
        {
            return a.mantissa > b.mantissa && a.exponent >= b.exponent;
        }
        public static bool LessThan(BigNum a, BigNum b)
        {
            return a.mantissa < b.mantissa && a.exponent <= b.exponent;
        }
        public static bool Equal(BigNum a, BigNum b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return WithinPrecision(a.mantissa - b.mantissa) && WithinPrecision(a.exponent - b.exponent);
        }
    }
}