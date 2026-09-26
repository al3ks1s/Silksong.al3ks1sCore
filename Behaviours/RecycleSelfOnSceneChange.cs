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
            SceneManager.sceneLoaded += DoRecycle;
        }

        public void DoRecycle(Scene scene, LoadSceneMode mode)
        {

            if (KeepWhenScenePattern)
                if ( !(ScenePattern.Equals(string.Empty)) && scene.name.Contains(ScenePattern))
                    return;

            if (mode == LoadSceneMode.Additive)
                return;

            Destroy(gameObject);

        }

        public void OnDestroy()
        {
            SceneManager.sceneLoaded -= DoRecycle;
        }
    }
}
