using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace al3ks1sCore.Behaviours
{
    [Tooltip("Add this behaviour to a GameObject with a Tk2dTileMap component to force a rebuild")]
    internal class ForceRefreshTilemap : MonoBehaviour
    {

        public void Start()
        {

            if (gameObject.GetComponent<tk2dTileMap>() == null)
                return;

            var tilemap = gameObject.GetComponent<tk2dTileMap>();
            tilemap.ForceBuild();

        }

    }
}
