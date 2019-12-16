using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace gghud
{
    public class Main : BaseScript
    {
        private static readonly SizeF size = new SizeF(18f, 18f);
  
        private Size m_screenResolution;
        private Ped m_spawnedPed;
        private List<GainedEffect> m_gainedEffects;
        private bool m_firstTick = true;
        
        public Main()
        {
            m_gainedEffects = new List<GainedEffect>(); 
        }

        [Tick]
        public async Task OnDrawTick()
        {
            try
            {
                await Delay(0);

                if(m_firstTick) 
                {
                    m_firstTick = false;

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
        }

        [Command("create")]
        public async void CreatePedCommand()
        {
            var pos = Game.PlayerPed.Position;
            var forward = Game.PlayerPed.ForwardVector;

            m_spawnedPed = await World.CreatePed(new Model(PedHash.Brad), pos * forward);
        }

        [Command("hc")]
        public void AddGainedEffect()
        {
            if(m_spawnedPed == null) return;

            var gainedEffect = new GainedEffect(m_spawnedPed.Position, "$", "mp_anim_cash", 1.0f, 25, Game.FPS);

            m_gainedEffects.Add(gainedEffect);
        }

        private void DrawGainedEffect(GainedEffect gainedEffect, Size screenRes)
        {
            var screenY = gainedEffect.Fade;

            SetDrawOrigin(gainedEffect.VictimPosition.X, gainedEffect.VictimPosition.Y, gainedEffect.VictimPosition.Z, 0);
            SetTextFont(4);
            SetTextColour(255, 255, 255, 255);
			SetTextScale(0.45f, 0.45f);
			SetTextDropShadow();
			SetTextOutline();
			SetTextEntry("STRING");
            AddTextComponentString(gainedEffect.Text);
            DrawText(8 / screenRes.Width, (-10f - screenY) / screenRes.Height);
            DrawSprite("MPHud", gainedEffect.Type, 0f, -screenY / screenRes.Height, size.Width / screenRes.Width, size.Height / screenRes.Height, 0f, 255, 255, 255, 255);
            ClearDrawOrigin();

            if(gainedEffect.FadeAfter <= 0)
            {
                gainedEffect.Fade = gainedEffect.Fade * 1.15f;
            }

            gainedEffect.Ticks--;
            gainedEffect.FadeAfter--;
        }

        public class GainedEffect {
            public GainedEffect(Vector3 victimPosition, string text, string type, float fade, int fadeAfter, float ticks)
            {
                VictimPosition = victimPosition;
                Text = text;
                Type = type;
                Fade = fade;
                FadeAfter = fadeAfter;
                Ticks = ticks;
            }

            public Vector3 VictimPosition {get; set;}
            public string Text {get; set;}
            public string Type {get; set;}
            public float Fade {get; set;}
            public int FadeAfter {get; set;}
            public float Ticks {get; set;}
        }
    }
}
