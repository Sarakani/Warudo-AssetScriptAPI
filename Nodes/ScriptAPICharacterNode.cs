using UnityEngine;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Scenes;
using Warudo.Plugins.Core.Assets.Environment;
using System.Linq;
using Warudo.Plugins.Core.Assets.Character;

namespace Sarakani.Plugins.ScriptAPI.Nodes
{
    [NodeType(Id = "0dfad028-c9a7-4d89-9a3c-c7ae35c43e75", Title = "CALL_CHARACTER_METHOD", Category = "ASSET_SCRIPT_API_CATEGORY")]
    public class ScriptAPICharacterNode : ScriptAPINode
    {
        [DataInput]
        [Label("API_CHARACTER_ASSET")]
        public CharacterAsset characterAsset;

        public override Asset GetAsset()
        {
            return characterAsset;
        }
    }
}
