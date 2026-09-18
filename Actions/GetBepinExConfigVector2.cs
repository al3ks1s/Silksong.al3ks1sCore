using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HutongGames.PlayMaker;
using UnityEngine;

namespace al3ks1sCore.Actions
{

    [UnityEngine.Tooltip("Int Bepin Ex configuration retrieval action")]
    internal class GetBepinExConfigVector2 : GetBepinExConfigValue
    {

        public new FsmVector2 storeVariable;

        public override void DoGetValue()
        {
            if (config.TryGetEntry<Vector2>(BepinExConfigSection, BepinExConfigKey, out ConfigEntry<Vector2> value))
                storeVariable.Value = value.Value;
        }

    }
}
