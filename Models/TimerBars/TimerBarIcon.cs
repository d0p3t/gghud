using CitizenFX.Core;
using System.Drawing;

namespace gghud.Models.TimerBars
{
    public sealed class TimerBarIcon
    {
        internal const float XOffset = 0.145f + 0.001f;
        internal const float YOffset = 0.016f * 0.5f;
        internal const float DefaultWidth = 0.016f + 0.003f;
        internal const float DefaultHeight = 0.032f + 0.004f;
        // internal const Color DefaultColor = Color.White;

        public TextureReference Texture { get; set; }
        public Vector2 Size { get; set; } = new Vector2(DefaultWidth, DefaultHeight);
        public Color Color { get; set; } = Color.FromArgb(255, 255, 255);
    }
}