using System;

namespace SuggestionApi.Domain.Extensions
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="val">The value to convert to radians</param>
        /// <returns>The value in radians</returns>
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }

        public static bool IsLatitudeValid(this double val)
        {
            return !(val < -90) && !(val > 90);
        }
        
        public static bool IsLongitudeValid(this double val)
        {
            return !(val < -180) && !(val > 180);
        }
    }
}