using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtil;

namespace GameUtil.UI
{
    /// <summary>
    /// UGUI 页面配置文件
    /// </summary>
    [System.Serializable]
    public class UGUIPageData
    {
        public string name_index;
        public string type_index;
        public string prefab_path;
    }

    public class UGUIConfigFile : GameConfigTemplate<string, UGUIPageData> 
    {
        public override void RebuildIndex()
        {
            foreach (var val in data) 
            {
                data_index[val.name_index] = val;
            }
        }
    }
}
