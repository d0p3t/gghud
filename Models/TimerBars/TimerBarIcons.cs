using CitizenFX.Core;

namespace gghud.Models.TimerBars
{
    public static class TimerBarIcons
    {
        // Dict 'MPRPSymbol'
        private static bool mpRPSymbolDictSet = false;
        private static TextureReference rpSymbolTexture;
        // Dict 'TimerBars'
        private static bool timerBarsDictSet = false;
        private static TextureReference rocketsTexture;
        private static TextureReference spikesTexture;
        private static TextureReference boostTexture;
        // Dict 'MpSpecialRace'
        private static bool mpSpecialRaceDictSet = false;
        private static TextureReference homingRocketTexture;
        private static TextureReference hopTexture;
        private static TextureReference machineGunTexture;
        private static TextureReference parachuteTexture;
        private static TextureReference rocketBoostTexture;
        // Dict 'CrossTheLine'
        private static bool crossTheLineDictSet = false;
        private static TextureReference largeTickTexture;
        private static TextureReference largeCrossTexture;
        // Dict 'TimerBar_Icons'
        private static bool timerBarIconsDictSet = false;
        private static TextureReference pickupBeastTexture;
        private static TextureReference pickupRandomTexture;
        private static TextureReference pickupSlowTimeTexture;
        private static TextureReference pickupSwapTexture;
        private static TextureReference pickupTestosteroneTexture;
        private static TextureReference pickupThermalTexture;
        private static TextureReference pickupWeedTexture;
        private static TextureReference pickupHiddenTexture;
        private static TextureReference pickupTimeTexture;

        private static void EnsureMpRPSymbolDictTextures()
        {
            if (!mpRPSymbolDictSet)
            {
                TextureDictionary dict = new TextureDictionary("MPRPSymbol");
                rpSymbolTexture = new TextureReference(dict, "rp", 20, 20);

                mpRPSymbolDictSet = true;
            }
        }

        private static void EnsureTimerBarsDictTextures()
        {
            if (!timerBarsDictSet)
            {
                TextureDictionary dict = new TextureDictionary("TimerBars");
                foreach (TextureReference t in dict.GetTextures())
                {
                    switch (t.Name.ToLower())
                    {
                        case "rockets": rocketsTexture = t; break;
                        case "spikes": spikesTexture = t; break;
                        case "boost": boostTexture = t; break;
                    }
                }
                timerBarsDictSet = true;
            }
        }

        private static void EnsureMpSpecialRaceDictTextures()
        {
            if (!mpSpecialRaceDictSet)
            {
                TextureDictionary dict = new TextureDictionary("MpSpecialRace");
                foreach (TextureReference t in dict.GetTextures())
                {
                    switch (t.Name.ToLower())
                    {
                        case "homing_rocket": homingRocketTexture = t; break;
                        case "hop": hopTexture = t; break;
                        case "machine_gun": machineGunTexture = t; break;
                        case "parachute": parachuteTexture = t; break;
                        case "rocket_boost": rocketBoostTexture = t; break;
                    }
                }
                mpSpecialRaceDictSet = true;
            }
        }

        private static void EnsureCrossTheLineDictTextures()
        {
            if (!crossTheLineDictSet)
            {
                TextureDictionary dict = new TextureDictionary("CrossTheLine");
                foreach (TextureReference t in dict.GetTextures())
                {
                    switch (t.Name.ToLower())
                    {
                        case "timer_largecross_32": largeCrossTexture = t; break;
                        case "timer_largetick_32": largeTickTexture = t; break;
                    }
                }
                crossTheLineDictSet = true;
            }
        }

        private static void EnsureTimerBarIconsDictTextures()
        {
            if (!timerBarIconsDictSet)
            {
                TextureDictionary dict = new TextureDictionary("TimerBar_Icons");

                pickupBeastTexture = new TextureReference(dict, "pickup_beast", 20, 20);
                pickupRandomTexture = new TextureReference(dict, "pickup_random", 20, 20);
                pickupSlowTimeTexture = new TextureReference(dict, "pickup_slow_time", 20, 20);
                pickupSwapTexture = new TextureReference(dict, "pickup_swap", 20, 20);
                pickupTestosteroneTexture = new TextureReference(dict, "pickup_beast", 20, 20);
                pickupThermalTexture = new TextureReference(dict, "pickup_thermal", 20, 20);
                pickupWeedTexture = new TextureReference(dict, "pickup_weed", 20, 20);
                pickupHiddenTexture = new TextureReference(dict, "pickup_hidden", 20, 20);
                pickupTimeTexture = new TextureReference(dict, "pickup_b_time", 20, 20);

                timerBarIconsDictSet = true;
            }
        }

        private static TextureReference RPSymbolTexture
        {
            get
            {
                EnsureMpRPSymbolDictTextures();
                return rpSymbolTexture;
            }
        }

        private static TextureReference RocketsTexture
        {
            get
            {
                EnsureTimerBarsDictTextures();
                return rocketsTexture;
            }
        }

        private static TextureReference SpikesTexture
        {
            get
            {
                EnsureTimerBarsDictTextures();
                return spikesTexture;
            }
        }

        private static TextureReference BoostTexture
        {
            get
            {
                EnsureTimerBarsDictTextures();
                return boostTexture;
            }
        }

        private static TextureReference HomingRocketTexture
        {
            get
            {
                EnsureMpSpecialRaceDictTextures();
                return homingRocketTexture;
            }
        }

        private static TextureReference HopTexture
        {
            get
            {
                EnsureMpSpecialRaceDictTextures();
                return hopTexture;
            }
        }

        private static TextureReference MachineGunTexture
        {
            get
            {
                EnsureMpSpecialRaceDictTextures();
                return machineGunTexture;
            }
        }

        private static TextureReference ParachuteTexture
        {
            get
            {
                EnsureMpSpecialRaceDictTextures();
                return parachuteTexture;
            }
        }

        private static TextureReference RocketBoostTexture
        {
            get
            {
                EnsureMpSpecialRaceDictTextures();
                return rocketBoostTexture;
            }
        }

        private static TextureReference LargeTickTexture
        {
            get
            {
                EnsureCrossTheLineDictTextures();
                return largeTickTexture;
            }
        }

        private static TextureReference LargeCrossTexture
        {
            get
            {
                EnsureCrossTheLineDictTextures();
                return largeCrossTexture;
            }
        }

        private static TextureReference PickupBeastTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupBeastTexture;
            }
        }

        private static TextureReference PickupRandomTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupRandomTexture;
            }
        }

        private static TextureReference PickupSlowTimeTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupSlowTimeTexture;
            }
        }

        private static TextureReference PickupSwapTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupSwapTexture;
            }
        }

        private static TextureReference PickupTestosteroneTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupTestosteroneTexture;
            }
        }

        private static TextureReference PickupThermalTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupThermalTexture;
            }
        }

        private static TextureReference PickupWeedTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupWeedTexture;
            }
        }

        private static TextureReference PickupHiddenTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupHiddenTexture;
            }
        }

        private static TextureReference PickupTimeTexture
        {
            get
            {
                EnsureTimerBarIconsDictTextures();
                return pickupTimeTexture;
            }
        }


        public static TimerBarIcon RP => new TimerBarIcon
        {
            Texture = RPSymbolTexture,
            Size = new Vector2(TimerBarIcon.DefaultWidth - 0.002f, TimerBarIcon.DefaultHeight - 0.009f),
        };
        public static TimerBarIcon Rockets => new TimerBarIcon() { Texture = RocketsTexture };
        public static TimerBarIcon Spikes => new TimerBarIcon { Texture = SpikesTexture };
        public static TimerBarIcon Boost => new TimerBarIcon { Texture = BoostTexture };
        public static TimerBarIcon HomingRocket = new TimerBarIcon { Texture = HomingRocketTexture };
        public static TimerBarIcon Hop => new TimerBarIcon { Texture = HopTexture };
        public static TimerBarIcon MachineGun => new TimerBarIcon { Texture = MachineGunTexture };
        public static TimerBarIcon Parachute => new TimerBarIcon { Texture = ParachuteTexture };
        public static TimerBarIcon RocketBoost => new TimerBarIcon { Texture = RocketBoostTexture };
        public static TimerBarIcon Tick => new TimerBarIcon { Texture = LargeTickTexture };
        public static TimerBarIcon Cross => new TimerBarIcon { Texture = LargeCrossTexture };
        public static TimerBarIcon Beast => new TimerBarIcon { Texture = PickupBeastTexture };
        public static TimerBarIcon Random => new TimerBarIcon { Texture = PickupRandomTexture };
        public static TimerBarIcon SlowTime => new TimerBarIcon { Texture = PickupSlowTimeTexture };
        public static TimerBarIcon Swap => new TimerBarIcon { Texture = PickupSwapTexture };
        public static TimerBarIcon Testosterone => new TimerBarIcon { Texture = PickupTestosteroneTexture };
        public static TimerBarIcon Thermal => new TimerBarIcon { Texture = PickupThermalTexture };
        public static TimerBarIcon Weed => new TimerBarIcon { Texture = PickupWeedTexture };
        public static TimerBarIcon Hidden => new TimerBarIcon { Texture = PickupHiddenTexture };
        public static TimerBarIcon Time => new TimerBarIcon { Texture = PickupTimeTexture };
    }
}