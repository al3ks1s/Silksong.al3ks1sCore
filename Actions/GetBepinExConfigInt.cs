using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HutongGames.PlayMaker;

namespace al3ks1sCore.Actions
{

    [Tooltip("Int Bepin Ex configuration retrieval action")]
    internal class GetBepinExConfigInt : GetBepinExConfigValue
    {

        public new FsmInt storeVariable;

        public override void DoGetValue()
        {
            if (config.TryGetEntry<int>(BepinExConfigSection, BepinExConfigKey, out ConfigEntry<int> value))
                storeVariable.Value = value.Value;
        }

    }
}
