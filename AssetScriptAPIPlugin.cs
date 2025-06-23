using Warudo.Core.Attributes;
using Warudo.Core.Plugins;
using UnityEngine;
using Warudo.Core.Scenes;
using Warudo.Core.Serializations;
using Warudo.Core.Events;
using Warudo.Core;
using Sarakani.Plugins.ScriptAPI.Nodes;

namespace Sarakani.Plugins.ScriptAPI
{
    [PluginType(
        Id = "Sarakani.AssetScriptAPI",
        Name = "ASSET_SCRIPT_API",
        Description = "ASSET_SCRIPT_API_DESCRIPTION",
        Author = "Sarakani",
        Version = "1.0.0",
        NodeTypes = new[] {
            typeof(ScriptAPICharacterNode),
            typeof(ScriptAPIPropNode),
            typeof(ScriptAPIEnvironmentNode),
            typeof(ScriptAPIReloadScriptsNode)
        })]
    public class AssetScriptAPIPlugin : Plugin
    {
        private ScriptManager scriptManager;

        protected override void OnCreate()
        {
            base.OnCreate();
            Debug.Log("Starting AssetScriptAPI");
            scriptManager = new ScriptManager();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnSceneLoaded(Scene scene, SerializedScene serializedScene)
        {
            if (scriptManager != null)
            {
                scriptManager.LoadAllScripts();
            }
        }

        public ScriptManager GetScriptManager()
        {
            return scriptManager;
        }

    }
    
}
