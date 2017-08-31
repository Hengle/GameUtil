using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

namespace GameUtil.OpenWorld 
{
    [ExecuteInEditMode]
    public abstract class WorldSaveObject : MonoBehaviour
    {
        public ulong GUID = 0;

        virtual protected void Awake() 
        {
            if (GUID == 0) 
            {
                GUID = FileUtil.NextGUID;
                //Debug.Log("<color=green>GUID is </color>" + GUID);
            }            
        }

        public void SaveBasicInfo( WorldSaveObjectData data ) 
        {
            data.ToWorldObject(this.gameObject);
            data.GUID = GUID;
        }

        abstract public WorldSaveObjectData Save();
        abstract public void Load(WorldSaveObjectData data);
    }

}
