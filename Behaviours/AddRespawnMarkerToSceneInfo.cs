using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace al3ks1sCore.Behaviours
{
    internal class AddRespawnMarkerToSceneInfo : MonoBehaviour
    {

        public void Start()
        {
            var teleportMap = SceneTeleportMap.GetTeleportMap();
            string sceneString = GameManager.instance.GetSceneNameString();

            var respawnMarker = GetComponent<RespawnMarker>();
            if (respawnMarker == null) return;

            string respawnMarkerName = respawnMarker.name;
            
            if (teleportMap.TryGetValue(sceneString, out var sceneInfo)) 
            {
                if (!sceneInfo.RespawnPoints.Contains(respawnMarkerName))
                    sceneInfo.RespawnPoints.Add(respawnMarkerName);
            }
        }
    }
}
