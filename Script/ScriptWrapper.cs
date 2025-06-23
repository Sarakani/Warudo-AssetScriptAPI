using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sarakani.Plugins.ScriptAPI
{
    public class ScriptWrapper
    {
        private GameObject gameObject;

        public ScriptWrapper(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void sendMethod(string method, object arg)
        {
            gameObject.SendMessage(method, arg);
        }

        public void sendMethod(string method)
        {
            gameObject.SendMessage(method);
        }

        public string getName()
        {
            return gameObject.name;
        }
    }
}
