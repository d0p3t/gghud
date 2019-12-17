using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using gghud.Models;

namespace gghud
{
    public class TimerBars : BaseScript
    {
        private static List<TimerBarBase> m_timerBars;

        private bool m_firstTick = true;
        private SizeF m_resolution;
        private PointF m_safeBounds;

        internal TimerBars()
        {
            m_timerBars = new List<TimerBarBase>();
        }

        [Tick]
        public async Task OnDrawTick()
        {
            try
            {
                if (m_firstTick)
                {
                    m_firstTick = false;

                    if (!HasStreamedTextureDictLoaded("timerbars"))
                    {
                        RequestStreamedTextureDict("timerbars", false);

                        while (!HasStreamedTextureDictLoaded("timerbars"))
                        {
                            await Delay(0);
                        }
                    }

                    // Just for debug purposes as UIText is somehow erroring out atm
                    // Might just use @citizenfx/NativeUI instead. Need a menu anyway sometime
                    if (Main.IsDebug)
                    {
                        var bar_one = new TextTimerBar("TEXT ONE", "00:10:00");
                        var bar_two = new TextTimerBar("TEXT TWO", "00:10:00");
                        var bar_three = new BarTimerBar("BAR");

                        bar_three.Percentage = 0.5f;

                        m_timerBars.Add(bar_one);
                        m_timerBars.Add(bar_two);
                        m_timerBars.Add(bar_three);
                    }

                    return;
                }

                for (int i = 0; i < m_timerBars.Count; i++)
                {
                    m_timerBars[i].Draw(i * 10, m_resolution, m_safeBounds);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                await Delay(5000);
            }
            await Task.FromResult(0);
        }

        [Tick]
        public async Task OnSlowUpdateTick()
        {
            try
            {
                m_resolution = Utils.ResolutionMaintainRatio;
                m_safeBounds = Utils.SafezoneBounds;

                await Delay(5000);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                await Delay(5000);
            }
            await Task.FromResult(0);
        }
    }
}