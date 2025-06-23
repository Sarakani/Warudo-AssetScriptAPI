using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Warudo.Core;
using Warudo.Core.Graphs;
using Warudo.Core.Scenes;
using Warudo.Plugins.Core.Assets;
using Warudo.Plugins.Core.Assets.Character;
using Warudo.Plugins.Core.Assets.Environment;
using Warudo.Plugins.Core.Assets.Prop;

namespace Sarakani.Plugins.ScriptAPI
{
    public class ScriptManager
    {
        public enum ScriptType
        {
            CHARACTER,
            PROP
        }
        
        private AssetWrapper environmentScripts;
        
        private Dictionary<ScriptType, List<AssetWrapper>> scripts;

        private Scene scene;

        private bool firstSetup;

        public ScriptManager() {}

        public bool CheckValidity()
        {
            if (!firstSetup)
            {
                LoadAllScripts();
            }
            return firstSetup;
        }

        public void LoadAllScripts()
        {
            this.scene = Context.OpenedScene;
            if (this.scene == null)
            {
                return;
            }
            EnvironmentAsset envAsset = scene.GetAssets<EnvironmentAsset>().FirstOrDefault();
            if (envAsset != null)
            {
                environmentScripts = new EnvironmentAssetWrapper(envAsset);
            }

            scripts = new Dictionary<ScriptType, List<AssetWrapper>>();
            foreach (ScriptType type in Enum.GetValues(typeof(ScriptType)))
            {
                if (!scripts.ContainsKey(type))
                {
                    scripts.Add(type, new List<AssetWrapper>());
                }
                scene.GetAssets<CharacterAsset>().ToList().ForEach(asset => LoadScriptsForAsset(asset));
                scene.GetAssets<PropAsset>().ToList().ForEach(asset => LoadScriptsForAsset(asset));
            }
            firstSetup = true;
        }

        public AssetWrapper LoadScriptsForAsset(Asset asset)
        {
            List<AssetWrapper> wrappers = GetWrapperList(asset);
            if (wrappers == null)
            {
                return null;
            }
            if (WrapperListContains(wrappers, asset))
            {
                AssetWrapper wrapper = GetWrapper(wrappers, asset);
                wrapper.LoadScripts();
                return wrapper;
            }
            AssetWrapper newWrapper = new GenericAssetWrapper(asset);
            wrappers.Add(newWrapper);
            return newWrapper;
        }

        public List<ScriptWrapper> GetScriptsForAsset(Asset asset)
        {
            if (asset is EnvironmentAsset)
            {
                if (environmentScripts == null || !environmentScripts.HasScripts())
                {
                    EnvironmentAsset envAsset = scene.GetAssets<EnvironmentAsset>().FirstOrDefault();
                    if (envAsset != null)
                    {
                        environmentScripts = new EnvironmentAssetWrapper(envAsset);
                    }
                }
                return GetEnvironmentScripts();
            }
            AssetWrapper wrapper = GetAssetWrapper(asset);
            if (wrapper == null)
            {
                wrapper = LoadScriptsForAsset(asset);
            }
            if (wrapper == null)
            {
                return new List<ScriptWrapper>();
            }
            return wrapper.GetScripts();
        }

        public List<ScriptWrapper> GetEnvironmentScripts()
        {
            if (environmentScripts == null)
            {
                return new List<ScriptWrapper>();
            }
            return environmentScripts.GetScripts();
        }

        public List<ScriptWrapper> GetCharacterScripts(CharacterAsset asset)
        {
            AssetWrapper wrapper = GetWrapper(ScriptType.CHARACTER, asset);
            if (wrapper != null)
            {
                return wrapper.GetScripts();
            }
            return new List<ScriptWrapper>();
        }

        public List<ScriptWrapper> GetPropScripts(PropAsset asset)
        {
            AssetWrapper wrapper = GetWrapper(ScriptType.PROP, asset);
            if (wrapper != null)
            {
                return wrapper.GetScripts();
            }
            return new List<ScriptWrapper>();
        }

        public ScriptWrapper GetScriptByName(Asset asset, string gameobject)
        {
            AssetWrapper wrapper = GetAssetWrapper(asset);
            return wrapper.getScriptWrapper(gameobject);
        }

        private AssetWrapper GetAssetWrapper(Asset asset)
        {
            if (asset is EnvironmentAsset)
            {
                return environmentScripts;
            }
            List<AssetWrapper> wrappers = GetWrapperList(asset);
            if (wrappers == null)
            {
                return null;
            }
            if (WrapperListContains(wrappers, asset))
            {
                return GetWrapper(wrappers, asset);
            }
            return null;
        }

        private List<AssetWrapper> GetWrapperList(ScriptType type)
        {
            if (scripts.TryGetValue(type, out List<AssetWrapper> wrappers))
            {
                return wrappers;
            }
            return null;
        }

        private List<AssetWrapper> GetWrapperList(Asset asset)
        {
            ScriptType type = ScriptType.CHARACTER;
            if (asset is CharacterAsset)
            {
                type = ScriptType.CHARACTER;
            }
            else if (asset is PropAsset)
            {
                type = ScriptType.PROP;
            }
            return GetWrapperList(type);
        }

        private bool WrapperListContains(List<AssetWrapper> wrappers, Asset asset)
        {
            foreach (AssetWrapper wrapper in wrappers)
            {
                if (wrapper.Equals(asset))
                {
                    return true;
                }
            }
            return false;
        }

        private AssetWrapper GetWrapper(List<AssetWrapper> wrappers, Asset asset)
        {
            foreach (AssetWrapper wrapper in wrappers)
            {
                if (wrapper.Equals(asset))
                {
                    return wrapper;
                }
            }
            return null;
        }

        private AssetWrapper GetWrapper(ScriptType type, Asset asset)
        {
            if (scripts.TryGetValue(type, out List<AssetWrapper> wrappers))
            {
                foreach (AssetWrapper wrapper in wrappers)
                {
                    if (wrapper.Equals(asset))
                    {
                        return wrapper;
                    }
                }
            }
            return null;
        }

        ~ScriptManager()
        {
            environmentScripts = null;
            scripts.Clear();
        }
    }
}