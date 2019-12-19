using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using System.Drawing;
using System.Threading.Tasks;

namespace gghud.Models.TimerBars
{
    public class TextTimerBar : LabeledTimerBar
    {
        // Constants from the game scripts
        private const float TextYOffset = ((((-0.01f - 0.005f) + 0.004f) - 0.001f) + 0.001f);
        private const float TextWrapEnd = (((((0.95f - 0.047f) + 0.001f) + 0.047f) - 0.002f) + 0.001f);
        private const float TextScale = 0.332f;
        private const float TextSize = ((((((0.469f + 0.096f) - 0.017f) + 0.022f) - 0.062f) - 0.001f) - 0.013f);

        public string Text { get; set; }
        public Color TextColor { get; set; } = Color.FromArgb(255, 255, 255);
        public TimerBarIcon Icon { get; set; }

        public TextTimerBar(string label, string text) : base(label)
        {
            Text = text;
        }

        public async override Task Draw(Vector2 position)
        {

            if (!IsVisible)
                return;

            await base.Draw(position);

            float wrapEnd = TextWrapEnd;

            if (Icon != null)
            {
                if (!Icon.Texture.Dictionary.IsLoaded)
                {
                    await Icon.Texture.Dictionary.LoadAndWait();
                }

                Vector2 iconPos = position + new Vector2(TimerBarIcon.XOffset, TimerBarIcon.YOffset);
                Color c = Icon.Color;
                DrawSprite(Icon.Texture.Dictionary.Name, Icon.Texture.Name, iconPos.X, iconPos.Y, Icon.Size.X, Icon.Size.Y, 0.0f, c.R, c.G, c.B, c.A);
                wrapEnd -= Icon.Size.X;
            }

            position.Y += TextYOffset;

            SetTextFont(0);
            SetTextWrap(0.0f, wrapEnd);
            SetTextScale(TextScale, TextSize);
            SetTextColour(TextColor.R, TextColor.G, TextColor.B, TextColor.A);
            SetTextJustification((int)Alignment.Right);

            BeginTextCommandDisplayText("STRING");
            AddTextComponentSubstringPlayerName(Text);
            EndTextCommandDisplayText(position.X, position.Y);

            await Task.FromResult(0);
        }
    }
}