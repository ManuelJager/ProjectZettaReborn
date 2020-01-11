namespace Zetta.Math
{
    public static class Geometryf
    {
        /// <summary>
        /// Normalizes the values between the given parameters
        /// </summary>
        /// <param name="min">The lowest value</param>
        /// <param name="max">The highest value</param>
        /// <param name="value">The value</param>
        /// <returns>The normalized value</returns>
        public static float NormalizeValue(float min, float max, float value)
        {
            return ((value - min) / (max - min)) * 2 - 1;
        }

        /// <summary>
        /// Maxizes and minimizes the given values
        /// </summary>
        /// <param name="max">The max value</param>
        /// <param name="val">The current value</param>
        /// <returns>The maximized value</returns>
        public static float MaxValue(float max, float val)
        {
            val = val > max ? max : val;
            val = val < -max ? -max : val;
            return val;
        }
    }
}