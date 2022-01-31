using System;

namespace MathEngine.Functions
{
    internal static class MathHelper
    {
        public static double Reg2Rad(double deg)
        {
            return Math.PI / 180.0 * deg;
        }
    }
}