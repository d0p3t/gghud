using CitizenFX.Core;
using System.Threading.Tasks;

namespace gghud
{
    public class Main : BaseScript
    {
        public static Main Instance;
        public static readonly bool IsDebug = true;

        public GainedEffects GainedEffects;

        private bool isLoaded = false;

        public Main() { }

        [Tick]
        public async Task LoadTick()
        {
            try
            {
                if (isLoaded) return;
                isLoaded = true;
                GainedEffects = new GainedEffects();
                RegisterScript(GainedEffects);
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