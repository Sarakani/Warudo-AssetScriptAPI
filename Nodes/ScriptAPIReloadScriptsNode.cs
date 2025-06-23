using UnityEngine;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Scenes;
using Warudo.Plugins.Core.Assets.Environment;
using System.Linq;
using Warudo.Plugins.Core.Assets.Prop;
using Warudo.Core.Graphs;

namespace Sarakani.Plugins.ScriptAPI.Nodes
{
    [NodeType(Id = "068bb202-304e-47fd-bec7-e5150a695475", Title = "RELOAD_SCRIPTS", Category = "ASSET_SCRIPT_API_CATEGORY")]
    public class ScriptAPIReloadScriptsNode : Node
    {

        [FlowInput]
        public Continuation Enter()
        {
            ReloadScripts();
            return Exit;
        }

        [FlowOutput]
        public Continuation Exit;

        public void ReloadScripts()
        {
            ScriptManager scriptManager = Context.PluginManager.GetPlugin<AssetScriptAPIPlugin>().GetScriptManager();
            scriptManager.LoadAllScripts();
        }
    }
}
