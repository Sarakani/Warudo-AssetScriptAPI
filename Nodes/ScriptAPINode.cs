using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Warudo.Core.Attributes;
using Warudo.Core.Data;
using Warudo.Core.Graphs;
using Warudo.Core.Scenes;
using Warudo.Core;
using Warudo.Plugins.Core.Assets.Environment;
using Warudo.Plugins.Core.Assets.Character;
using Warudo.Plugins.Core.Assets.Prop;

namespace Sarakani.Plugins.ScriptAPI.Nodes
{

    public class ScriptAPINode : Node
    {
        [FlowInput]
        public Continuation Enter()
        {
            CallMethod();
            return Exit;
        }

        [FlowOutput]
        public Continuation Exit;

        [DataInput]
        [Label("API_SCRIPT")]
        [AutoComplete(nameof(AutoCompleteScript), forceSelection: true)]
        public string scriptObject;

        [DataInput]
        [Label("API_METHOD")]
        public string methodToCall;

        [DataInput]
        [Label("API_PARAMETER")]
        public object parameter;
        
        public async UniTask<AutoCompleteList> AutoCompleteScript()
        {
            ScriptManager scriptManager = Context.PluginManager.GetPlugin<AssetScriptAPIPlugin>().GetScriptManager();
            HashSet<string> types = new HashSet<string>();
            if (scriptManager.CheckValidity())
            {
                Asset usedAsset = GetAsset();
                List<ScriptWrapper> scripts = scriptManager.GetScriptsForAsset(usedAsset);
                foreach (ScriptWrapper scriptWrapper in scripts)
                {
                    types.Add(scriptWrapper.getName());
                }
            } else
            {
                types.Add("Not Loaded");
            }
            return types.Select(it => new AutoCompleteEntry
            {
                label = it,
                value = it
            }).ToAutoCompleteList();
        }

        public void CallMethod()
        {
            ScriptManager scriptManager = Context.PluginManager.GetPlugin<AssetScriptAPIPlugin>().GetScriptManager();
            if (!scriptManager.CheckValidity())
            {
                return;
            }
            Asset asset = GetAsset();
            if (asset == null)
            {
                return;
            }
            ScriptWrapper script = scriptManager.GetScriptByName(asset, scriptObject);
            if (script == null)
            {
                return;
            }
            if (parameter == null)
            {
                script.sendMethod(methodToCall);
                return;
            }
            script.sendMethod(methodToCall, parameter);
        }

        public virtual Asset GetAsset()
        {
            return null;
        }

    }
}
