using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HutongGames.PlayMaker;

namespace al3ks1sCore.Actions
{

    [Tooltip("Generic Bepin Ex configuration retrieval action, use specific typed actions for better stability")]
    internal class GetBepinExConfigValue : FsmStateAction
    {

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The plugin ID as defined in the main class.")]
        public string BepinExPluginID;

        [RequiredField]
        public string BepinExConfigSection;
        
        [RequiredField]
        public string BepinExConfigKey;

        public NamedVariable storeVariable;

        protected ConfigFile config;

        public override void Awake()
        {
            if (!Chainloader.PluginInfos.TryGetValue(BepinExPluginID, out var plugin))
            {
                base.Finish();
                return;
            }

            config = plugin.Instance.Config;        
        }

        public override void OnEnter()
        {
            DoGetValue();
            base.Finish();
        }

        public virtual void DoGetValue()
        {
            if (config.TryGetEntry<object>(BepinExConfigSection, BepinExConfigKey, out ConfigEntry<object> value))
                storeVariable.RawValue = value.Value;
        }

    }
}
