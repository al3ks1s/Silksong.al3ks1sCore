using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HutongGames.PlayMaker;

namespace al3ks1sCore.Actions
{

    [Tooltip("Int Bepin Ex configuration retrieval action")]
    internal class GetBepinExConfigFloat : GetBepinExConfigValue
    {

        public new FsmFloat storeVariable;

        public override void DoGetValue()
        {
            if (config.TryGetEntry<float>(BepinExConfigSection, BepinExConfigKey, out ConfigEntry<float> value))
                storeVariable.Value = value.Value;
        }

    }
}
