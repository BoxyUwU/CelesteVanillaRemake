using System;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.MyExampleMod
{
    public class MyExampleModModule : EverestModule
    {
        public static MyExampleModModule Instance { get; private set; }

        public override Type SettingsType => typeof(MyExampleModModuleSettings);
        public static MyExampleModModuleSettings Settings => (MyExampleModModuleSettings)Instance._Settings;

        public override Type SessionType => typeof(MyExampleModModuleSession);
        public static MyExampleModModuleSession Session => (MyExampleModModuleSession)Instance._Session;

        public override Type SaveDataType => typeof(MyExampleModModuleSaveData);
        public static MyExampleModModuleSaveData SaveData => (MyExampleModModuleSaveData)Instance._SaveData;

        public MyExampleModModule()
        {
            Instance = this;
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(MyExampleModModule), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(MyExampleModModule), LogLevel.Info);
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
