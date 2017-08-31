using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;


namespace GameUtil.UI
{
    /// <summary>
    /// UGUI 背包道具配置文件。
    /// </summary>
    [System.Serializable]
    public class UGUIInventoryItemConfig : ScriptableObject
    {
        public string[] item_names;
        public string[] item_descs;
        [HideInInspector]
        public int item_group;
        public int item_id;
        public Sprite item_image;
        [Tooltip("This field can be null! if null means this object will not drop on ground.")]
        public GameObject pickup_prefab;
        public GameObject prefab;
        public bool discardable = true;
        public int max_stack = 50;

        public string item_name
        {
            get
            {
                return item_names[UGUIManager.Language];
            }
        }

        public string item_desc
        {
            get
            {
                return item_descs[UGUIManager.Language];
            }
        }
    }

    [System.Serializable]
    public class UGUIInventoryDatabase : GameConfigTemplate<int,UGUIInventoryItemConfig>
    {
        public override void AddEmpty()
        {
            data.Add(null);
        }

        public override void RebuildIndex()
        {
            data_index.Clear();
            foreach (var inv_item in data)
            {
                data_index[inv_item.item_id] = inv_item;
            }
        }
    }
}

