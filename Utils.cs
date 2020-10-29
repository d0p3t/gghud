using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;
using System;
using System.Drawing;

namespace gghud
{
    public static class Utils
    {
        public static SizeF ResolutionMaintainRatio
        {
            get
            {
                int screenw = Screen.Resolution.Width;
                int screenh = Screen.Resolution.Height;

                float ratio = (float)screenw / screenh;
                float width = 1080f * ratio;

                return new SizeF(width, 1080f);
            }
        }

        public static Point SafezoneBounds
        {
            get
            {
                float t = GetSafeZoneSize();

                double g = Math.Round(Convert.ToDouble(t), 2);
                g = (g * 100) - 90;
                g = 10 - g;

                int screenw = Screen.Resolution.Width;
                int screenh = Screen.Resolution.Height;

                float ratio = (float)screenw / screenh;
                float wmp = ratio * 5.4f;

                return new Point((int)Math.Round(g * wmp), (int)Math.Round(g * 5.4f));
            }
        }

        public static float Clamp(float value, float inclusiveMinimum, float inlusiveMaximum)
        {
            if (value >= inclusiveMinimum)
            {
                if (value <= inlusiveMaximum)
                {
                    return value;
                }

                return inlusiveMaximum;
            }

            return inclusiveMinimum;
        }
    }
}