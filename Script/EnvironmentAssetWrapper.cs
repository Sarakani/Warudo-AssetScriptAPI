using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warudo.Core.Scenes;

namespace Sarakani.Plugins.ScriptAPI
{
    public class EnvironmentAssetWrapper : AssetWrapper
    {
        public EnvironmentAssetWrapper(Asset asset) : base(asset) {}

        public override List<ScriptWrapper> CollectAssets()
        {
            List<ScriptWrapper> list = new List<ScriptWrapper>();
            GameObject[] gameObjects = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Environment").GetRootGameObjects();
            foreach (GameObject rootObject in gameObjects)
            {
                list.AddRange(CollectScripts(rootObject));
            }
            return list;
        }
    }
}
