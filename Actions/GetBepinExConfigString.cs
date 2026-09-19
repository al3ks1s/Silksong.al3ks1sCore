using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HutongGames.PlayMaker;

namespace al3ks1sCore.Actions
{

    [ActionCategory("Al3ks1s Core")]
    [Tooltip("Int Bepin Ex configuration retrieval action")]
    internal class GetBepinExConfigString : GetBepinExConfigValue
    {

        public new FsmString storeVariable;

        public override void DoGetValue()
        {
            if (config.TryGetEntry<string>(BepinExConfigSection, BepinExConfigKey, out ConfigEntry<string> value))
                storeVariable.Value = value.Value;
        }

    }
}
