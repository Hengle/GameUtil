using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

namespace GameUtil.UI 
{
    [System.Serializable]
    public class UGUIStringItem 
    {
        public string key;
        public string value;
    }
    
    public class UGUIStringTable : GameConfigTemplate<string,UGUIStringItem>
    {
        public override void RebuildIndex()
        {
            data_index.Clear();
            foreach (var st in data) 
            {
                data_index[st.key] = st;
            }
        }
    }

}
