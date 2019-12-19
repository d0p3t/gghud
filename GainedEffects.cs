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

                    RequestStreamedTextureDict("MPHud", false);
                    m_screenResolution = Screen.Resolution;
                }
                m_gainedEffects.ForEach(x => DrawGainedEffect(x, m_screenResolution));

                m_gainedEffects = m_gainedEffects.FindAll(x => x.Ticks > 0);
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

            Entity victim = Entity.FromHandle(int.Parse(data[0].ToString()));
            Entity attacker = Entity.FromHandle(int.Parse(data[1].ToString()));
            bool victimDied = int.Parse(data[3].ToString()) == 1;

            if (victim == null || attacker == null || !victimDied) return;

            if (victim is Ped v)
            {
                if (attacker is Ped a)
                {
                    if (a.Handle == Game.PlayerPed.Handle)
                    {
                        var last = v.Bones.LastDamaged;
                        var rp = "+50";

                        if (last == Bone.SKEL_Head || last == Bone.SKEL_Neck_1)
                        {
                            rp = "+100";
                        }
                        var gainedEffect = new GainedEffect(victim.Position, rp, "mp_anim_rp", 1.0f, 25, Game.FPS);
                        m_gainedEffects.Add(gainedEffect);
                    }
                }
            }
            uint weaponHash = (uint)int.Parse(data[4].ToString());
            bool isMeleeDamage = int.Parse(data[9].ToString()) != 0;
            int vehicleDamageTypeFlag = int.Parse(data[10].ToString());
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
            SetTextEntry("STRING");
            AddTextComponentString(gainedEffect.Text);
            DrawText(25f / screenRes.Width, (-17f - screenY) / screenRes.Height);
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
