using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil 
{

    public interface IResourceProvider
    {
        Object Load(string path);
    }

    public class ResourceProviderDefault : IResourceProvider
    {

        public Object Load(string path)
        {
            Object obj = Resources.Load(path);
            if (obj == null)
            {
                Debug.Log(string.Format("<color=blue> Resource not found at " + path + "</color>"));
            }
            return obj;
        }
    }

    public class ResourceManager : Singleton<ResourceManager>
    {
        private Dictionary<string, Object> object_cache = new Dictionary<string, Object>();
        private IResourceProvider provider;

        public override void OnAwake()
        {
            provider = new ResourceProviderDefault(); 
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public Object Load(string path)
        {
            if (object_cache.ContainsKey(path))
            {
                return object_cache[path];
            }
            else
            {
                Object obj = provider.Load(path);
                if (obj != null)
                {
                    object_cache.Add(path, obj);
                }
                return obj;
            }
        }

        public T CreateInstance<T>(string path, Transform parent) where T : Component
        {
            Object obj = Load(path);

            if (obj != null)
            {
                GameObject gobj = GameObject.Instantiate(obj) as GameObject;

                if (parent != null)
                {
                    gobj.transform.SetParent(parent);
                }

                T monoT = gobj.GetComponent<T>();
                if (monoT == null)
                {
                    monoT = gobj.AddComponent<T>();
                }

                return monoT;
            }

            return default(T);
        }
    }
}
