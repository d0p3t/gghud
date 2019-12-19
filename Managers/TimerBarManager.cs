using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using gghud.Models.TimerBars;

namespace gghud.Managers
{
    internal static class TimerBarManager
    {
        private const float m_initialX = 0.795f;
        private const float m_initialY = 0.925f - 0.002f;
        private const float m_loadingPromptYOffset = 0.036f;

        private static readonly List<TimerBar> m_timerBars = new List<TimerBar>();
        private static bool orderPriorityChanged;

        public static void AddTimerBar(TimerBar timerBar)
        {
            m_timerBars.Add(timerBar);
        }

        public static void RemoveTimerBar(TimerBar timerBar)
        {
            m_timerBars.Remove(timerBar);
        }

        public static void NotifyOrderPriorityChanged()
        {
            orderPriorityChanged = true;
        }

        public static async Task OnDrawTick()
        {
            try
            {
                if (m_timerBars.Count <= 0) return;

                if (orderPriorityChanged)
                {
                    TimerBar[] tmp = m_timerBars.OrderBy(t => t.OrderPriority).ToArray();
                    m_timerBars.Clear();
                    m_timerBars.AddRange(tmp);
                    orderPriorityChanged = false;
                }

                bool isAnyVisible = false;
                Vector2 pos = new Vector2(m_initialX, m_initialY - (IsLoadingPromptBeingDisplayed() ? m_loadingPromptYOffset : 0.0f));
                for (int i = 0; i < m_timerBars.Count; i++)
                {
                    TimerBar b = m_timerBars[i];

                    if (b != null && b.IsVisible)
                    {
                        if (!isAnyVisible)
                        {
                            isAnyVisible = true;
                            SetScriptGfxAlign('R', 'B');
                            SetScriptGfxAlignParams(0.0f, 0.0f, 0.952f, 0.949f);
                        }

                        await b.Draw(pos);
                        pos.Y -= b.SmallHeight ? TimerBar.SmallHeightWithGap : TimerBar.DefaultHeightWithGap;
                    }
                }

                if (isAnyVisible)
                {
                    ResetScriptGfxAlign();
                    HideHudComponentThisFrame(6);
                    HideHudComponentThisFrame(7);
                    HideHudComponentThisFrame(8);
                    HideHudComponentThisFrame(9);
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
                await BaseScript.Delay(5000);
            }
        }
    }
}