using BepInEx.Bootstrap;
using BepInEx.Configuration;
using HutongGames.PlayMaker;
using UnityEngine;

namespace al3ks1sCore.Actions
{

    [UnityEngine.Tooltip("Int Bepin Ex configuration retrieval action")]
    internal class GetBepinExConfigVector3 : GetBepinExConfigValue
    {

        public new FsmVector3 storeVariable;

        public override void DoGetValue()
        {
            if (config.TryGetEntry<Vector3>(BepinExConfigSection, BepinExConfigKey, out ConfigEntry<Vector3> value))
                storeVariable.Value = value.Value;
        }

    }
}
