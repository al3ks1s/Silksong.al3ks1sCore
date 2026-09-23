using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;

namespace al3ks1sCore.AssetManagers
{
    public class CatalogManager
    {

        private static CatalogManager m_instance;
        public static CatalogManager Instance 
        {
            get 
            { 
                if (m_instance == null)
                    m_instance = new CatalogManager();

                return m_instance;
            }        
        }

        Dictionary<string, IResourceLocator> Locators = new();

        public IResourceLocator LoadCatalog(string CorePath, string assetFolder, string catalogName)
        {
            string catalogPath = Path.Combine(CorePath, assetFolder, GetCatalogNameByPlatform(catalogName));

            if (Locators.TryGetValue(catalogPath, out IResourceLocator catalogLocator))
                return catalogLocator;

            catalogLocator = Addressables.LoadContentCatalogAsync(catalogPath).WaitForCompletion();
            Locators.Add(catalogPath, catalogLocator);

            return catalogLocator;
        }

        public static string GetAssetPath(PluginInfo Info, string assetPath)
        {
            string CorePath = Path.GetDirectoryName(Info.Location);

            if (!Directory.Exists(Path.Combine(CorePath, assetPath)))
                if (Directory.Exists(Path.Combine(CorePath, "plugins", assetPath)))
                    CorePath = Path.Combine(Path.GetDirectoryName(Info.Location), "plugins");

            return CorePath;
        }

        private static string GetCatalogNameByPlatform(string catalogName)
        {
            string catalog = Application.platform switch
            {
                RuntimePlatform.WindowsPlayer => $"{catalogName}-StandaloneWindows64.bin",
                RuntimePlatform.LinuxPlayer => $"{catalogName}-StandaloneLinux64.bin",
                RuntimePlatform.OSXPlayer => $"{catalogName}-StandaloneOSX.bin",
                RuntimePlatform.Switch => $"{catalogName}-Switch.bin",
                _ => throw new PlatformNotSupportedException($"Unsupported platform: {Application.platform}")
            };

            return catalog;
        }

    }
}
