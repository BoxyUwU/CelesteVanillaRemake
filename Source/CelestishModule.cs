using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.Celestish
{
    public class CelestishModule : EverestModule
    {
        public static CelestishModule Instance { get; private set; }

        public override Type SettingsType => typeof(CelestishModuleSettings);
        public static CelestishModuleSettings Settings => (CelestishModuleSettings)Instance._Settings;

        public override Type SessionType => typeof(CelestishModuleSession);
        public static CelestishModuleSession Session => (CelestishModuleSession)Instance._Session;

        public override Type SaveDataType => typeof(CelestishModuleSaveData);
        public static CelestishModuleSaveData SaveData => (CelestishModuleSaveData)Instance._SaveData;

        public CelestishModule()
        {
            Instance = this;
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(CelestishModule), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(CelestishModule), LogLevel.Info);
#endif
        }

        public override void Load()
        {
            // TODO: apply any hooks that should always be active
        }

        public override void Unload()
        {
            // TODO: unapply any hooks applied in Load()
        }
    }
}
