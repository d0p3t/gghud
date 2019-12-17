using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using System.Drawing;

namespace gghud
{
    public class Sprite
    {
        public PointF Position { get; }
        public SizeF Size { get; }
        public Color Color { get; }
        public bool Visible { get; }
        public float Heading { get; }

        public string TextureDict { get; }
        public string TextureName { get; }

        public Sprite(string textureDict, string textureName, PointF position, SizeF size, float heading, Color color)
        {
            TextureDict = textureDict;
            TextureName = textureName;
            Position = position;
            Size = size;
            Heading = heading;
            Color = color;

            Visible = true;
        }

        public void Draw()
        {
            if (!Visible) return;

            int screenw = Screen.Resolution.Width;
            int screenh = Screen.Resolution.Height;
            const float height = 1080f;
            float ratio = (float)screenw / screenh;
            var width = height * ratio;


            float w = (Size.Width / width);
            float h = (Size.Height / height);
            float x = (Position.X / width) + w * 0.5f;
            float y = (Position.Y / height) + h * 0.5f;

            DrawSprite(TextureDict, TextureName, x, y, w, h, Heading, Color.R, Color.G, Color.B, Color.A);
        }
    }
}