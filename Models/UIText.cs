using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;
using System;
using System.Drawing;

namespace gghud.Models
{
    public class UIText : Text
    {
        public float Wrap { get; set; } = 0;
        public Alignment TextAlignment { get; set; }

        public UIText(string caption, PointF position, float scale) : base(caption, position, scale)
        {
            TextAlignment = Alignment.Left;
        }

        public UIText(string caption, PointF position, float scale, Color color)
            : base(caption, position, scale, color)
        {
            TextAlignment = Alignment.Left;
        }

        public UIText(string caption, PointF position, float scale, Color color, Font font, Alignment justify)
            : base(caption, position, scale, color, font, CitizenFX.Core.UI.Alignment.Left)
        {
            TextAlignment = justify;
        }

        public override void Draw(SizeF offset)
        {
            int screenw = Screen.Resolution.Width;
            int screenh = Screen.Resolution.Height;
            const float height = 1080f;
            float ratio = (float)screenw / screenh;
            var width = height * ratio;

            float x = (Position.X) / width;
            float y = (Position.Y) / height;

            API.SetTextFont((int)Font);
            API.SetTextScale(1.0f, Scale);
            API.SetTextColour(Color.R, Color.G, Color.B, Color.A);
            if (Shadow)
                API.SetTextDropShadow();
            if (Outline)
                API.SetTextOutline();
            switch (TextAlignment)
            {
                case Alignment.Center:
                    API.SetTextCentre(true);
                    break;
                case Alignment.Right:
                    API.SetTextRightJustify(true);
                    API.SetTextWrap(0, x);
                    break;
            }

            if (Wrap != 0)
            {
                float xsize = (Position.X + Wrap) / width;
                API.SetTextWrap(x, xsize);
            }

            API.SetTextEntry("jamyfafi");
            AddLongString(Caption);

            API.DrawText(x, y);
        }

        public static void AddLongString(string str)
        {
            var utf8ByteCount = System.Text.Encoding.UTF8.GetByteCount(str);

            if (utf8ByteCount == str.Length)
            {
                AddLongStringForAscii(str);
            }
            else
            {
                AddLongStringForUtf8(str);
            }
        }

        private static void AddLongStringForAscii(string input)
        {
            const int maxByteLengthPerString = 99;

            for (int i = 0; i < input.Length; i += maxByteLengthPerString)
            {
                string substr = (input.Substring(i, Math.Min(maxByteLengthPerString, input.Length - i)));
                API.AddTextComponentString(substr);
            }
        }

        internal static void AddLongStringForUtf8(string input)
        {
            const int maxByteLengthPerString = 99;

            if (maxByteLengthPerString < 0)
            {
                throw new ArgumentOutOfRangeException("maxLengthPerString");
            }
            if (string.IsNullOrEmpty(input) || maxByteLengthPerString == 0)
            {
                return;
            }

            var enc = System.Text.Encoding.UTF8;

            var utf8ByteCount = enc.GetByteCount(input);
            if (utf8ByteCount < maxByteLengthPerString)
            {
                API.AddTextComponentString(input);
                return;
            }

            var startIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var length = i - startIndex;
                if (enc.GetByteCount(input.Substring(startIndex, length)) > maxByteLengthPerString)
                {
                    string substr = (input.Substring(startIndex, length - 1));
                    API.AddTextComponentString(substr);

                    i -= 1;
                    startIndex = (startIndex + length - 1);
                }
            }
            API.AddTextComponentString(input.Substring(startIndex, input.Length - startIndex));
        }
    }

    public class UIRectangle : CitizenFX.Core.UI.Rectangle
    {
        public UIRectangle(PointF pos, SizeF size, Color color) : base(pos, size, color) { }
    }
}