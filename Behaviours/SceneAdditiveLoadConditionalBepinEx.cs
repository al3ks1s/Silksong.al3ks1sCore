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
            if (bepinTests != null)
            {
                return bepinTests.IsFulfilled;
            }

            return false;
        }
    }

    [Serializable]
    [Tooltip("Test group on bepinex configurations")]
    public class BepinExTest
    {
        public enum NumTestType
        {
            Equal,
            NotEqual,
            LessThan,
            MoreThan
        }
        public enum StringTestType
        {
            Equal,
            NotEqual,
            Contains,
            NotContains
        }


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
        public class BepinTestInt : BepinTest<int>
        {
            public int ExpectedValue;
            public NumTestType testType;

            public override bool IsFulfilled
            {
                get
                {
                    var configValue = GetConfigValue();
                    switch (testType)
                    {
                        case NumTestType.Equal:
                            return configValue == ExpectedValue;
                        case NumTestType.NotEqual:
                            return configValue != ExpectedValue;
                        case NumTestType.LessThan:
                            return configValue < ExpectedValue;
                        case NumTestType.MoreThan:
                            return configValue > ExpectedValue;
                        default:
                            return false;
                    }
                }
            }
        }

        [Serializable]
        public class BepinTestFloat : BepinTest<float>
        {
            public float ExpectedValue;
            public NumTestType testType;

            public override bool IsFulfilled
            {
                get
                {
                    var configValue = GetConfigValue();
                    switch (testType)
                    {
                        case NumTestType.Equal:
                            return configValue == ExpectedValue;
                        case NumTestType.NotEqual:
                            return configValue != ExpectedValue;
                        case NumTestType.LessThan:
                            return configValue < ExpectedValue;
                        case NumTestType.MoreThan:
                            return configValue > ExpectedValue;
                        default:
                            return false;
                    }
                }
            }
        }
        [Serializable]
        public class BepinTestString : BepinTest<string>
        {
            public string ExpectedValue;
            public StringTestType testType;

            public override bool IsFulfilled
            {
                get
                {
                    var configValue = GetConfigValue();
                    switch (testType)
                    {
                        case StringTestType.Equal:
                            return configValue.Equals(ExpectedValue);
                        case StringTestType.NotEqual:
                            return !configValue.Equals(ExpectedValue);
                        case StringTestType.Contains:
                            return configValue.Contains(ExpectedValue);
                        case StringTestType.NotContains:
                            return !configValue.Contains(ExpectedValue);
                        default:
                            return false;
                    }
                }
            }
        }


        [Serializable]
        public struct BepinTestGroup
        {

            public BepinTestGroup()
            { }

            [SerializeReference]
            public BepinTest[] tests = Array.Empty<BepinTest>();

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
