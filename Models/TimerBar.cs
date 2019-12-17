using Font = CitizenFX.Core.UI.Font;
using CitizenFX.Core.UI;
using System.Drawing;

namespace gghud.Models
{
    public abstract class TimerBarBase
    {
        public string Label { get; set; }
        public bool IsVisible { get; set; }

        public TimerBarBase(string label)
        {
            Label = label;
            IsVisible = true;
        }

        public virtual void Draw(int interval, SizeF res, PointF safe)
        {
            new UIText(Label, new PointF(res.Width - safe.X - 180, (int)res.Height - safe.Y - (30 + (4 * interval))), 0.3f, Color.White, Font.ChaletLondon, Alignment.Right).Draw();
            new Sprite("timerbars", "all_black_bg", new PointF((int)res.Width - safe.X - 298, (int)res.Height - safe.Y - (40 + (4 * interval))), new SizeF(300, 37), 0f, Color.FromArgb(180, 255, 255, 255)).Draw();
        }
    }

    public class TextTimerBar : TimerBarBase
    {
        public string Text { get; set; }
        public TextTimerBar(string label, string text) : base(label)
        {
            Text = text;
        }

        public override void Draw(int interval, SizeF res, PointF safe)
        {
            //base.Draw(interval, res, safe);
            new UIText(Text, new PointF(res.Width - safe.X - 10, res.Height - safe.Y - (42 + (4 * interval))), 0.5f, Color.White, Font.ChaletLondon, Alignment.Right).Draw(SizeF.Empty);
        }
    }

    public class BarTimerBar : TimerBarBase
    {
        public float Percentage { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }

        public BarTimerBar(string label) : base(label)
        {
            BackgroundColor = Color.DarkRed;
            ForegroundColor = Color.Red;
        }

        public override void Draw(int interval, SizeF res, PointF safe)
        {
            base.Draw(interval, res, safe);

            var start = new PointF((int)res.Width - safe.X - 160, (int)res.Height - safe.Y - (28 + (4 * interval)));

            new UIRectangle(start, new SizeF(150, 15), BackgroundColor).Draw();
            new UIRectangle(start, new SizeF((int)(150 * Percentage), 15), ForegroundColor).Draw();
        }
    }
}