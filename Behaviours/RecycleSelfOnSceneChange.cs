using UnityEngine;
using UnityEngine.SceneManagement;

namespace al3ks1sCore.Behaviours
{
    internal class RecycleSelfOnSceneChange : MonoBehaviour
    {

        public bool KeepWhenScenePattern;
        public string ScenePattern;

        public void Start() 
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += DoRecycle;
        }

        public void DoRecycle(Scene originScene, Scene targetScene)
        {
            if (KeepWhenScenePattern)
                if ( !(ScenePattern.Equals(string.Empty)) && targetScene.name.Contains(ScenePattern))
                    return;

            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            SceneManager.activeSceneChanged -= DoRecycle;
        }
    }
}
