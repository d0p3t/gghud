using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using gghud.Managers;
using gghud.Models.TimerBars;
using gghud.Models;

namespace gghud
{
    public class TimerBars : BaseScript
    {
        private bool m_firstTick = true;
        private bool m_toggle = false;

        internal TimerBars()
        {
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
                        TextTimerBar timerBar = new TextTimerBar("TIME LEFT", "00:00:00");
                        TextTimerBar timerBarTwo = new TextTimerBar("POSITION", "1/8");

                        LabeledTimerBar labelBar = new LabeledTimerBar("LABEL") { LabelColor = Color.FromArgb(255, 0, 0) };

                        ProgressTimerBar progressBar = new ProgressTimerBar("PROGRESS", Color.FromArgb(0, 0, 0), Color.FromArgb(0, 255, 0)) { Percentage = 0.5f };

                        progressBar.Markers.Add(0.3333f);
                        progressBar.Markers.Add(0.6666f);

                        CheckpointsTimerBar checkpointsBar = new CheckpointsTimerBar("CHECKPOINTS", 4) { LabelColor = Color.FromArgb(240, 200, 80) };

                        for (int i = 0; i < checkpointsBar.Checkpoints.Length; i++)
                        {
                            checkpointsBar.Checkpoints[i].IsCompleted = false;
                            checkpointsBar.Checkpoints[i].CompletedColor = Color.FromArgb(240, 200, 80);
                            checkpointsBar.Checkpoints[i].HasCross = false;
                            checkpointsBar.Checkpoints[i].CrossColor = Color.FromArgb(0, 0, 0);
                        }
                        checkpointsBar.Checkpoints[1].IsCompleted = true;
                        checkpointsBar.Checkpoints[2].HasCross = true;
                        checkpointsBar.Checkpoints[3].IsCompleted = true;
                        checkpointsBar.Checkpoints[3].HasCross = true;
                        checkpointsBar.Checkpoints[6].IsCompleted = true;

                        IconsTimerBar iconsBar = new IconsTimerBar("ICONS");
                        var texRef = new TextureReference("TimerBar_Icons", "pickup_beast", 20, 20);
                        iconsBar.Icons.Add(new TimerBarIcon { Texture = texRef });

                        labelBar.OrderPriority = 0;
                        progressBar.OrderPriority = 1;
                    }

                    return;
                }
                if (Game.IsControlJustPressed(0, Control.Context))
                {
                    m_toggle = !m_toggle;
                }

                if (m_toggle)
                    await TimerBarManager.OnDrawTick();
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