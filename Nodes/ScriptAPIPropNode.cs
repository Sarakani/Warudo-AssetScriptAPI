using UnityEngine;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Scenes;
using Warudo.Plugins.Core.Assets.Environment;
using System.Linq;
using Warudo.Plugins.Core.Assets.Prop;

namespace Sarakani.Plugins.ScriptAPI.Nodes
{
    [NodeType(Id = "15faf932-d90c-4a7e-8cd1-40093402b792", Title = "CALL_PROP_METHOD", Category = "ASSET_SCRIPT_API_CATEGORY")]
    public class ScriptAPIPropNode : ScriptAPINode
    {
        [DataInput]
        [Label("API_PROP_ASSET")]
        public PropAsset propAsset;

        public override Asset GetAsset()
        {
            return propAsset;
        }
    }
}