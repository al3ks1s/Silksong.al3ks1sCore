using BepInEx.Bootstrap;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace al3ks1sCore.Behaviours
{

    [Tooltip("Extends the Scene Additional Load script to support bepinEx configs")]
    internal class SceneAdditiveLoadConditionalBepinEx : SceneAdditiveLoadConditional
    {

        [SerializeField]
        private BepinExTest bepinTests;

        private new void OnEnable()
        {
            if (LoadInSequence && !sceneLoaded && TryTestLoad())
            {
                _additiveSceneLoads.Add(this);
            }
        }

        private new void Start()
        {
            if (LoadInSequence || sceneLoaded || !TryTestLoad() || !BepinExTests())
            {
                return;
            }
            _additiveSceneLoads.Add(this);
            ApplySettings();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == SceneNameToLoad)
                {
                    sceneLoaded = true;
                    return;
                }
            }
            StartCoroutine(LoadRoutine(callEvent: true, this));
        }

        private bool BepinExTests()
        {
            if (tests != null)
            {
                return tests.IsFulfilled;
            }

            return false;
        }
    }

    [Serializable]
    public class BepinExTest
    {

        [Serializable]
        public abstract class BepinTest
        {
            public virtual bool IsFulfilled
            {
                get { return false; }
            }
        }

        [Serializable]
        public class BepinTest<T> : BepinTest
        {
            public string KeyName;
            public string SectionName;
            public string BepinExPluginID;

            public T GetConfigValue()
            {
                if (Chainloader.PluginInfos.TryGetValue(BepinExPluginID, out var plugin))
                {
                    if (plugin.Instance.Config.TryGetEntry<object>(SectionName, KeyName, out ConfigEntry<object> value))
                        return (T)value.Value;
                }

                return default;
            }
        }

        [Serializable]
        public class BepinTestBool : BepinTest<bool>
        {
            public bool ExpectedValue;

            public override bool IsFulfilled
            {
                get { return GetConfigValue() == ExpectedValue; }
            }
        }


        [Serializable]
        public struct BepinTestGroup
        {

            public BepinTest[] tests;

            public bool IsFulfilled
            {
                get
                {
                    if (tests == null) return true;
                    if (tests.Length == 0) return true;

                    return tests.All(t => t.IsFulfilled);
                }
            }
        }


        [SerializeField]
        public BepinTestGroup[] tests;

        public bool IsFulfilled
        {
            get
            {
                if (tests == null) return true;
                if (tests.Length == 0) return true;

                return tests.Any(t => t.IsFulfilled);
            }
        }

        public BepinExTest()
        {
            tests = Array.Empty<BepinTestGroup>();
        }
    }
}
