using UnityEngine;
using System.Collections.Generic;
using CFramework.Core.Patterns;

namespace Snipe
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        private Dictionary<string, Object> objectLookup;

        private List<string> paths;

        public ResourceManager()
        {
            objectLookup = new Dictionary<string, Object>();

            paths = new List<string>();
        }

        public void AddPath(string path)
        {
            paths.Add(path);
        }
        
        public void LoadAll()
        {
            foreach (string path in paths)
            {
                Object obj = Resources.Load(path);
                
                if (obj != null)
                {
                    objectLookup[path] = obj;
                }
                else
                {
                    throw new System.Exception("Couldn't load resource at path: " + path);
                }
            }
        }

        public Object GetObject(string path)
        {
            return objectLookup[path];
        }
    }
}