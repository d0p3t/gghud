using CitizenFX.Core;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gghud
{
    public class HudComponents : BaseScript
    {
        private static readonly List<HudComponent> m_hiddenDefaults = new List<HudComponent>
        {
            HudComponent.AreaName,
            HudComponent.StreetName,
            HudComponent.VehicleName
        };

        private bool m_hideAll = false;

        internal HudComponents() { }

        [Tick]
        public async Task OnHudComponents()
        {
            try
            {
                Screen.Hud.IsVisible = !m_hideAll;

                if (m_hideAll) return;

                m_hiddenDefaults.ForEach(h => Screen.Hud.HideComponentThisFrame(h));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                await Delay(5000);
            }
            await Task.FromResult(0);
        }

        [EventHandler("gameStatus")]
        public void OnGameStatus(bool running)
        {
            m_hideAll = !running;
        }
    }
}