using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace gghud
{
    public class GainedEffects : BaseScript
    {
        private static readonly SizeF size = new SizeF(24f, 24f);

        private Size m_screenResolution;
        private List<GainedEffect> m_gainedEffects;
        private bool m_firstTick = true;

        internal GainedEffects()
        {
            m_gainedEffects = new List<GainedEffect>();
        }

        [Tick]
        public async Task OnDrawTick()
        {
            try
            {
                if (m_firstTick)
                {
                    m_firstTick = false;

                    AddTextEntry("GAIN_FX", "~a~");

                    RequestStreamedTextureDict("MPHud", false);
                    m_screenResolution = Screen.Resolution;
                }

                if (m_gainedEffects.Count == 0)
                {
                    await Delay(0);
                    return;
                }

                var remainingEffects = new List<GainedEffect>();

                foreach (var effect in m_gainedEffects)
                {
                    DrawGainedEffect(effect, m_screenResolution);
                    if (effect.Ticks > 0) remainingEffects.Add(effect);
                }

                m_gainedEffects = remainingEffects;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                await Delay(5000);
            }
            await Task.FromResult(0);
        }

        [EventHandler("gameEventTriggered")]
        public void OnDeath(string eventName, List<Object> data)
        {
            if (eventName != "CEventNetworkEntityDamage") return;

            Entity attacker = Entity.FromHandle(int.Parse(data[1].ToString()));
            if (attacker == null || attacker.Handle != Game.PlayerPed.Handle) return;

            bool victimDied = int.Parse(data[3].ToString()) == 1;
            if (!victimDied) return;

            Entity victim = Entity.FromHandle(int.Parse(data[0].ToString()));
            if (victim == null || !(victim is Ped v) || !v.IsPlayer) return;

            var rp = "+20";

            if (v.Bones.LastDamaged == Bone.SKEL_Head)
            {
                rp = "+100";
            }
            var gainedEffect = new GainedEffect(victim.Position, rp, "mp_anim_rp", 1.0f, 25, Game.FPS);
            m_gainedEffects.Add(gainedEffect);
        }

        private void DrawGainedEffect(GainedEffect gainedEffect, Size screenRes)
        {
            var screenY = gainedEffect.Fade;

            SetDrawOrigin(gainedEffect.VictimPosition.X, gainedEffect.VictimPosition.Y, gainedEffect.VictimPosition.Z + 0.95f, 0);
            SetTextFont(4);
            SetTextColour(255, 255, 255, 255);
            SetTextScale(0.45f, 0.45f);
            SetTextDropShadow();
            SetTextOutline();
            BeginTextCommandDisplayText("GAIN_FX");
            AddTextComponentSubstringPlayerName(gainedEffect.Text);
            EndTextCommandDisplayText(25f / screenRes.Width, (-17f - screenY) / screenRes.Height);
            DrawSprite("MPHud", gainedEffect.Type, 0f, -screenY / screenRes.Height, size.Width / screenRes.Width, size.Height / screenRes.Height, 0f, 255, 255, 255, 255);
            ClearDrawOrigin();

            if (gainedEffect.FadeAfter <= 0)
            {
                gainedEffect.Fade = gainedEffect.Fade * 1.15f;
            }

            gainedEffect.Ticks--;
            gainedEffect.FadeAfter--;
        }

        sealed class GainedEffect
        {
            public GainedEffect(Vector3 victimPosition, string text, string type, float fade, int fadeAfter, float ticks)
            {
                VictimPosition = victimPosition;
                Text = text;
                Type = type;
                Fade = fade;
                FadeAfter = fadeAfter;
                Ticks = ticks;
            }

            public Vector3 VictimPosition { get; set; }
            public string Text { get; set; }
            public string Type { get; set; }
            public float Fade { get; set; }
            public int FadeAfter { get; set; }
            public float Ticks { get; set; }
        }
    }
}
