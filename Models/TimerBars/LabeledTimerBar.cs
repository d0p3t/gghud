using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace gghud.Models.TimerBars
{
    public class LabeledTimerBar : TimerBar
    {
        // Constants from the game scripts
        private const float LabelInitialWrapEnd = ((((0.88f - 0.062f) + 0.026f) + 0.027f) + 0.03f) - 0.034f;
        private const float LabelScale = 0.202f;
        private const float LabelSize = 0.288f;

        public string Label { get; set; }
        public Color LabelColor { get; set; } = Color.FromArgb(255, 255, 255);

        public LabeledTimerBar(string label)
        {
            Label = label;
        }

        public async override Task Draw(Vector2 position)
        {
            if (!IsVisible)
                return;

            await base.Draw(position);

            float wrapEnd = LabelInitialWrapEnd;

            if (!GetIsWidescreen())
            {
                wrapEnd -= 0.02f;
            }

            wrapEnd = wrapEnd - (0.03f * WrapEndMultiplier);

            SetTextFont(0);
            SetTextWrap(0.0f, wrapEnd);
            SetTextScale(LabelScale, LabelSize);
            SetTextColour(LabelColor.R, LabelColor.G, LabelColor.B, LabelColor.A);
            SetTextJustification((int)Alignment.Right);

            BeginTextCommandDisplayText("STRING");
            AddTextComponentSubstringPlayerName(Label);
            EndTextCommandDisplayText(position.X, position.Y);

            await Task.FromResult(0);
        }

        private static float WrapEndMultiplier
        {
            get
            {
                float aspectRatio = GetAspectRatio(false);
                int screenWidth = 0;
                int screenHeight = 0;

                GetActiveScreenResolution(ref screenWidth, ref screenHeight);
                float screenRatio = (float)screenWidth / screenHeight;
                aspectRatio = Math.Min(aspectRatio, screenRatio);
                if (screenRatio > 3.5f && aspectRatio > 1.7f)
                {
                    return 0.4f;
                }
                else if (aspectRatio > 1.7f)
                {
                    return 0.0f;
                }
                else if (aspectRatio > 1.5f)
                {
                    return 0.2f;
                }
                else if (aspectRatio > 1.3f)
                {
                    return 0.3f;
                }
                else
                {
                    return 0.4f;
                }
            }
        }
    }
}