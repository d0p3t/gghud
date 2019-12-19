using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gghud.Models.TimerBars
{
    public class ProgressTimerBar : LabeledTimerBar
    {
        private static readonly TextureDictionary MarkerTextureDictionary = new TextureDictionary("timerbar_lines");
        /// <summary>
        /// We use the texture with the marker at 50% because the game only has textures for every 10% interval,
        /// but here we support any percentage and the 50% texture is the easiest one to offset.
        /// </summary>
        private const string MarkerTextureName = "linemarker50_128";
        // Constants from the game scripts
        private const float BarXOffset = 0.118f; // == (((((0.919f - 0.081f) + 0.028f) + 0.05f) - 0.001f) - 0.002f) - TimerBarManager.InitialX
        private const float BarYOffset = ((((0.013f - 0.002f) + 0.001f) + 0.001f) - 0.001f);
        private const float BarWidth = 0.069f;
        private const float BarHeight = 0.011f;


        private float percentage;

        /// <summary>
        /// Gets or sets the percentage of the progress bar.
        /// The percentage is clamped to the range [0.0, 1.0].
        /// </summary>
        public float Percentage { get => percentage; set => percentage = Utils.Clamp(value, 0.0f, 1.0f); }
        public Color BackColor { get; set; } = Color.FromArgb(139, 0, 0);
        public Color ForeColor { get; set; } = Color.FromArgb(255, 0, 0);
        /// <summary>
        /// Gets the <see cref="List{T}"/> of <see cref="float"/> that contains the percentages at which a marker is drawn.
        /// The percentages are clamped to the range [0.0, 1.0].
        /// </summary>
        public List<float> Markers { get; } = new List<float>();
        public Color MarkersColor { get; set; } = Color.FromArgb(0, 0, 0);
        /// <inheritdoc/>
        protected internal override bool SmallHeight => true;

        public ProgressTimerBar(string label, Color backColor, Color foreColor) : base(label)
        {
            BackColor = backColor;
            ForeColor = foreColor;
        }

        public ProgressTimerBar(string label) : base(label)
        {
        }

        public async override Task Draw(Vector2 position)
        {
            if (!IsVisible)
                return;

            await base.Draw(position);

            position.X += BarXOffset;
            position.Y += BarYOffset;

            DrawRect(position.X, position.Y, BarWidth, BarHeight, BackColor.R, BackColor.G, BackColor.B, BackColor.A);

            float fillX = position.X - BarWidth * 0.5f + BarWidth * 0.5f * percentage;
            DrawRect(fillX, position.Y, BarWidth * percentage, BarHeight, ForeColor.R, ForeColor.G, ForeColor.B, ForeColor.A);

            if (Markers.Count > 0)
            {
                if (!MarkerTextureDictionary.IsLoaded) MarkerTextureDictionary.Load();

                float markerOrigX = position.X - BarWidth * 0.5f;
                foreach (float markerPercentage in Markers)
                {
                    float x = markerOrigX + BarWidth * Utils.Clamp(markerPercentage, 0.0f, 1.0f);
                    DrawSprite(MarkerTextureDictionary.Name, MarkerTextureName, x, position.Y, BarWidth, BarHeight * 2.0f, 0.0f, 0, 0, 0, 255);
                }
            }
        }
    }
}