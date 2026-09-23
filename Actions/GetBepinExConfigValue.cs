using HutongGames.PlayMaker;

namespace al3ks1sCore.Actions
{

    [ActionCategory("Al3ks1s Core")]
    [Tooltip("Generic Bepin Ex configuration retrieval action, use specific typed actions for better stability")]
    public class GetBepinExConfigValue : FsmStateAction
    {

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The plugin ID as defined in the main class.")]
        public string BepinExPluginID;

        [RequiredField]
        public string BepinExConfigSection;
        
        [RequiredField]
        public string BepinExConfigKey;

        public NamedVariable storeVariable;

        public override void Awake()
        {

        }

        public override void OnEnter()
        {
            DoGetValue();
            base.Finish();
        }

        public virtual void DoGetValue()
        {

        }

    }
}
