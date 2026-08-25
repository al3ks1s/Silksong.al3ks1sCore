using BepInEx;
using System.IO;

namespace al3ks1sCore
{
    // TODO - adjust the plugin guid as needed
    [BepInAutoPlugin(id: "io.github.al3ks1s.al3ks1score")]
    public partial class al3ks1sCorePlugin : BaseUnityPlugin
    {

        public static string CorePath = string.Empty;

        private void Awake()
        {

            CorePath = Path.GetDirectoryName(Info.Location);

            // Put your initialization logic here
            Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
        }
    }
}
