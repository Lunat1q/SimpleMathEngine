using System;
using System.Globalization;

namespace MathEngine.Helpers
{
    public static class MathExtensions
    {
        private const double DecimalMaxValue = (double) decimal.MaxValue;
        private const double DecimalMinValue = (double) decimal.MinValue;

        internal static bool DoubleEquals(this double d1, double d2)
        {
            return Math.Abs(d1 - d2) < 0.00000000000001;
        }

        public static decimal Normalize(this double value)
        {
            return (decimal)value / 1.000000000000000000000000000000000m;
        }

        public static string AsString(this double value)
        {
            return value < DecimalMaxValue && value > DecimalMinValue ? value.Normalize().ToString(CultureInfo.InvariantCulture) : value.ToString(CultureInfo.InvariantCulture);
        }
    }
}