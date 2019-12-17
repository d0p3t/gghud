using CitizenFX.Core;
using System.Threading.Tasks;

namespace gghud
{
    public class Main : BaseScript
    {
        public static Main Instance;
        public static readonly bool IsDebug = false;

        public GainedEffects GainedEffects;
        public HudComponents HudComponents;
        public LoadingPrompt LoadingPrompt;
        public TimerBars TimerBars;

        public Main()
        {

            if (Instance == null)
                Instance = this;

            GainedEffects = new GainedEffects();
            HudComponents = new HudComponents();
            LoadingPrompt = new LoadingPrompt();
            TimerBars = new TimerBars();

            RegisterScript(GainedEffects);
            RegisterScript(HudComponents);
            RegisterScript(LoadingPrompt);
            RegisterScript(TimerBars);
        }

        [Tick]
        public async Task LoadTick()
        {
            try
            {
                Debug.WriteLine("[HUD] Registering scripts...");

                HudComponents = new HudComponents();
                LoadingPrompt = new LoadingPrompt();
                TimerBars = new TimerBars();

                RegisterScript(HudComponents);
                RegisterScript(LoadingPrompt);
                RegisterScript(TimerBars);

                Debug.WriteLine("[HUD] Registering scripts completed.");

                Tick -= LoadTick;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
                await Delay(5000);
            }

            await Task.FromResult(0);
        }
    }
}