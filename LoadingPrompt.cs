using CitizenFX.Core;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gghud
{
    public class LoadingPrompt : BaseScript
    {
        private int m_timeRemaining = 0;

        internal LoadingPrompt()
        {
        }

        public async Task OnAutoHideTick()
        {
            if (m_timeRemaining <= 0)
            {
                Screen.LoadingPrompt.Hide();
                Tick -= OnAutoHideTick;
                return;
            }

            m_timeRemaining--;
            await Task.FromResult(0);
        }

        [EventHandler("gghud_showLoadingPrompt")]
        public void ShowLoadingPrompt(string text, int spinnerType, int ms)
        {
            Screen.LoadingPrompt.Show(text, (LoadingSpinnerType)spinnerType);

            if (ms != default(int) && ms > 0)
            {
                m_timeRemaining = ms;
                Tick += OnAutoHideTick;
            }
        }

        [EventHandler("gghud_hideLoadingPrompt")]
        public void HideLoadingPrompt()
        {
            Screen.LoadingPrompt.Hide();
            m_timeRemaining = 0;
        }
    }
}