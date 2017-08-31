using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

namespace GameUtil.OpenWorld 
{

    public class WorldManager : MonoBehaviour
    {
        public Transform building_root;

        void OnEnable() 
        {
            building_root = transform.FindChild("BuildingBlocks");
            LoadWorld();
        }

        public Transform FindTransform(string trans_name) 
        {
            for (int i = 0; i < transform.childCount; i++) 
            {
                if (transform.GetChild(i).gameObject.name == trans_name) 
                {
                    return transform.GetChild(i);
                }
            }
            return null;
        }

        /// <summary>
        /// 寻找直接子节点，规定这些子节点不可以重名！
        /// </summary>
        Dictionary<string, Transform> FindDirectChildren() 
        {
            Dictionary<string, Transform> direct_children_map = new Dictionary<string, Transform>();
            Transform trans = transform;

            //把自己也加进去。
            direct_children_map[trans.name] = trans;

            for (int i = 0; i < trans.childCount; i++) 
            {
                Transform child = transform.GetChild(i);
                direct_children_map[child.name] = child;
            }

            return direct_children_map;
        }

        public void SaveWorld() 
        {
            WorldSaveObject[] wsos = GetComponentsInChildren<WorldSaveObject>();

            WorldProfile w_profile = new WorldProfile();
            w_profile.BeginSave();

            for (int i = 0; i < wsos.Length; i++) 
            {
                if (wsos[i] != null) 
                {
                    w_profile.Append(wsos[i].Save());
                }                
            }

            w_profile.EndSave();
        }

        public void ClearWorld() 
        {
            WorldSaveObject[] wsos = GetComponentsInChildren<WorldSaveObject>();
            for (int i = 0; i < wsos.Length; i++) 
            {
                if (wsos != null) 
                {
                    if (Application.isPlaying)
                    {
                        GameObject.Destroy(wsos[i].gameObject);
                    }
                    else 
                    {
                        GameObject.DestroyImmediate(wsos[i].gameObject);
                    }                   
                }
            }
        }

        public void LoadWorld() 
        {
            WorldProfile w_profile = WorldProfile.Load();

            if (w_profile == null) 
            {
                w_profile = new WorldProfile();
            }

            Dictionary<string,Transform> trans_index = FindDirectChildren();

            Debug.Log("w_profile is" + w_profile);

            for (int i = 0; i < w_profile.saved_object_list.Count; i++) 
            {
                WorldSaveObjectData wsod = w_profile.saved_object_list[i];

                if (wsod != null) 
                {
                    Transform parent = trans_index[wsod.parent_trans];

                    GameObject restore_object = wsod.ToGameObject(parent);

                    WorldSaveObject mono_wso = restore_object.GetComponent<WorldSaveObject>();
                    mono_wso.GUID = wsod.GUID;
                    mono_wso.Load(wsod);
                }
            }
        }
    }

}

