using UnityEngine;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Scenes;
using Warudo.Plugins.Core.Assets.Environment;
using System.Linq;

namespace Sarakani.Plugins.ScriptAPI.Nodes {
    [NodeType(Id = "93cf6dc1-c344-45a9-84a0-1c7e1f6a7559", Title = "CALL_ENVIRONMENT_METHOD", Category = "ASSET_SCRIPT_API_CATEGORY")]
    public class ScriptAPIEnvironmentNode : ScriptAPINode
    {
        public override Asset GetAsset()
        {
            return Context.OpenedScene.GetAssets<EnvironmentAsset>().FirstOrDefault();
        }
    }
}