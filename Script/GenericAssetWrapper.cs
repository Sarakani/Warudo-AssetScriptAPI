using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warudo.Core.Scenes;
using Warudo.Plugins.Core.Assets;

namespace Sarakani.Plugins.ScriptAPI
{
    public class GenericAssetWrapper : AssetWrapper
    {
        public GenericAssetWrapper(Asset asset) : base(asset) { }

        public override List<ScriptWrapper> CollectAssets()
        {
            List<ScriptWrapper> list = new List<ScriptWrapper>();
            if (GetAsset() is GameObjectAsset)
            {
                list.AddRange(CollectScripts(((GameObjectAsset) GetAsset()).GameObject));
            }
            return list;
        }
    }
}