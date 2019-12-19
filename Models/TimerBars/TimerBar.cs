using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Drawing;
using System.Threading.Tasks;
using gghud.Managers;

namespace gghud.Models.TimerBars
{
    public class TimerBar : IDisposable
    {
        // Constants from the game scripts
        private static readonly TextureDictionary BgTextureDictionary = new TextureDictionary("timerbars");
        private const string BgTextureName = "all_black_bg";
        private const string BgHighlightTextureName = "all_white_bg";
        private const float BgXOffset = 0.079f;
        private const float BgDefaultYOffset = 0.008f;
        private const float BgSmallYOffset = 0.012f;
        private const float BgWidth = 0.157f;
        private const float BgDefaultHeight = 0.036f;
        private const float BgSmallHeight = 0.028f;
        internal const float DefaultHeightWithGap = ((0.025f + 0.006f) + 0.0009f) + 0.008f;
        internal const float SmallHeightWithGap = ((0.025f + 0.006f) + 0.0009f);

        public const uint DefaultOrderPriority = uint.MaxValue;

        private uint orderPriority = DefaultOrderPriority;

        private bool isDisposed = false;
        public bool IsDisposed
        {
            get => isDisposed;
            private set
            {
                if (value != isDisposed)
                {
                    isDisposed = value;
                }
            }
        }

        public bool IsVisible { get; set; } = true;
        public Color? HighlightColor { get; set; }

        /// <summary>
        /// Gets whether the height of this <see cref="TimerBar"/> is small or default.
        /// </summary>
        protected internal virtual bool SmallHeight => false;

        public TimerBar()
        {
            TimerBarManager.AddTimerBar(this);
        }

        ~TimerBar()
        {
            Dispose(false);
        }

        public async virtual Task Draw(Vector2 position)
        {
            if (!IsVisible)
                return;

            // Probably don't need
            // if (!HasStreamedTextureDictLoaded(BgTextureDictionary))
            // {
            //     RequestStreamedTextureDict(BgTextureDictionary, false);
            //     while (!HasStreamedTextureDictLoaded(BgTextureDictionary)) await BaseScript.Delay(1);
            // }

            Vector2 bgPos = position;
            bgPos.X += BgXOffset;
            bgPos.Y += SmallHeight ? BgSmallYOffset : BgDefaultYOffset;
            float height = SmallHeight ? BgSmallHeight : BgDefaultHeight;

            if (HighlightColor.HasValue)
            {
                DrawSprite(BgTextureDictionary.Name, BgHighlightTextureName, bgPos.X, bgPos.Y, BgWidth, height, 0.0f, HighlightColor.Value.R, HighlightColor.Value.G, HighlightColor.Value.B, 140);
            }
            DrawSprite(BgTextureDictionary.Name, BgTextureName, bgPos.X, bgPos.Y, BgWidth, height, 0.0f, 255, 255, 255, 140);

            await Task.FromResult(0);
        }

        public uint OrderPriority
        {
            get => orderPriority;
            set
            {
                if (value != orderPriority)
                {
                    orderPriority = value;
                    TimerBarManager.NotifyOrderPriorityChanged();
                }
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    TimerBarManager.RemoveTimerBar(this);
                }

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}