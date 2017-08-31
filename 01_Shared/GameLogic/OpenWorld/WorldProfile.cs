using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

namespace GameUtil.OpenWorld
{
    [System.Serializable]
    public class WorldSaveObjectData 
    {
        public static string prefab_folder = "Prefabs3D/";
        public static Dictionary<string, Object> cached_object = new Dictionary<string, Object>();

        public float p_x;
        public float p_y;
        public float p_z;

        public float r_x;
        public float r_y;
        public float r_z;

        public float s_x;
        public float s_y;
        public float s_z;

        public string parent_trans;
        public string prefab_name;
        public ulong GUID;

        virtual public void ToWorldObject(GameObject gobj) 
        {
            Transform trans = gobj.transform;
            p_x = trans.position.x;
            p_y = trans.position.y;
            p_z = trans.position.z;

            r_x = trans.eulerAngles.x;
            r_y = trans.eulerAngles.y;
            r_z = trans.eulerAngles.z;

            s_x = trans.localScale.x;
            s_y = trans.localScale.y;
            s_z = trans.localScale.z;

            parent_trans = trans.parent.name;
            prefab_name = prefab_folder + gobj.name.Trim().Replace("(Clone)", "");
        }

        virtual public GameObject ToGameObject( Transform parent ) 
        {
            Object obj_to_init = null;
            
            //暂时采用100%Cache的方式，以后考虑采用多次使用到才Cache的内存优化策略
            cached_object.TryGetValue( prefab_name, out obj_to_init );
            if( obj_to_init == null )
            {
                obj_to_init = Resources.Load( prefab_name );
                cached_object[prefab_name] = obj_to_init;
            }

            if(obj_to_init != null)
            {
                GameObject gobj = GameObject.Instantiate(obj_to_init, parent) as GameObject ;
                gobj.transform.position = new Vector3(p_x, p_y, p_z);
                gobj.transform.eulerAngles = new Vector3(r_x, r_y, r_z);
                gobj.transform.localScale = new Vector3(s_x, s_y, s_z);
                return gobj;
            }
            else
            {
                Debug.LogError(string.Format("object to init with path {0} is not exist", prefab_name));
            }

            return null;
        }
    }


    [System.Serializable]
    public class WorldProfile
    {
        public List<WorldSaveObjectData> saved_object_list = new List<WorldSaveObjectData>();

        public static string GetProfilePath(bool file_only = false) 
        {
            if (file_only == false)
            {
                return Application.persistentDataPath + "/" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + ".wp";
            }
            else 
            {
                return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + ".wp";
            }            
        }

        public void Append(WorldSaveObjectData wsd) 
        {
            saved_object_list.Add(wsd);
        }

        public void BeginSave() 
        {
            saved_object_list.Clear();
        }

        public void EndSave() 
        {
            FileUtil.SaveClass(GetProfilePath(), this, true);
        }

        public static WorldProfile Load() 
        {
            return FileUtil.LoadClass<WorldProfile>(GetProfilePath(), true);
        }
    }

}