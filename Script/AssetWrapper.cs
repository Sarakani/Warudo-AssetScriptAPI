using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Warudo.Core.Scenes;

namespace Sarakani.Plugins.ScriptAPI
{
    public class AssetWrapper
    {
        private Asset asset;

        private List<ScriptWrapper> scripts;

        public AssetWrapper(Asset asset)
        {
            this.asset = asset;
            scripts = new List<ScriptWrapper>();
            LoadScripts();
        }

        public void LoadScripts()
        {
            scripts.Clear();
            scripts.AddRange(CollectAssets());
        }

        protected List<ScriptWrapper> CollectScripts(GameObject gameObject)
        {
            List<ScriptWrapper> list = new List<ScriptWrapper>();
            CheckForScript(list, gameObject);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                CheckForScript(list, child);
                list.AddRange(CollectScripts(child));
            }
            return list;
        }

        protected void CheckForScript(List<ScriptWrapper> list, GameObject gameObject)
        {
            //Debug.Log("Checking Object " + gameObject.name);
            if (gameObject.GetComponent(typeof(MonoBehaviour)))
            {
                Debug.Log("Found Script on " + gameObject.name);
                ScriptWrapper script = new ScriptWrapper(gameObject);
                list.Add(script);
            }
        }

        public ScriptWrapper getScriptWrapper(string gameobject)
        {
            foreach (ScriptWrapper script in scripts)
            {
                if (script.getName().Equals(gameobject))
                {
                    return script;
                }
            }
            return null;
        }

        public List<ScriptWrapper> GetScripts()
        {
            return scripts;
        }

        public Asset GetAsset()
        {
            return asset;
        }

        public bool HasScripts()
        {
            return ScriptCount() > 0;
        }

        public int ScriptCount()
        {
            if (scripts == null)
            {
                return 0;
            }
            return scripts.Count;
        }

        public override bool Equals(object obj)
        {
            if (obj is AssetWrapper)
            {
                return GetAsset().Equals(((AssetWrapper) obj).GetAsset());
            }
            if (obj is Asset)
            {
                return GetAsset().Equals((Asset)obj);
            }
            return false;
        }

        public virtual List<ScriptWrapper> CollectAssets() { return new List<ScriptWrapper>(); }
    }
}
