using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gghud.Models.TimerBars
{
    public class IconsTimerBar : LabeledTimerBar
    {
        public IList<TimerBarIcon> Icons { get; } = new List<TimerBarIcon>();
        public float Spacing { get; set; } = TimerBarIcon.DefaultWidth * 0.75f;

        public IconsTimerBar(string label) : base(label)
        {
        }

        public override async Task Draw(Vector2 position)
        {

            if (!IsVisible)
                return;

            await base.Draw(position);

            if (Icons.Count > 0)
            {
                position.X += TimerBarIcon.XOffset;
                position.Y += TimerBarIcon.YOffset;

                for (int i = 0; i < Icons.Count; i++)
                {
                    TimerBarIcon icon = Icons[i];

                    if (!icon.Texture.Dictionary.IsLoaded) icon.Texture.Dictionary.Load();

                    Color c = icon.Color;
                    DrawSprite(icon.Texture.Dictionary.Name, icon.Texture.Name, position.X, position.Y, icon.Size.X, icon.Size.Y, 0.0f, c.R, c.G, c.B, c.A);
                    position.X -= Spacing;
                }
            }
        }
    }
}