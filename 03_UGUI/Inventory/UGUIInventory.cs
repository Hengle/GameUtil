using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil.UI
{
    /// <summary>
    /// 背包类的泛型类，方便用户扩展这个类。
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class UGUIInventory <TItem>: UGUIWidget where TItem : UGUIInventoryItem
    {
        [SerializeField]
        protected List<TItem> items = new List<TItem>();

        protected Inventory inventory_data;

        //暂定：capacity就返回格子的数量，暂时不考虑解锁格子的问题！
        public int capacity 
        {
            get 
            {
                return items.Count;
            }
        }

        protected TItem this[int index] 
        {
            get 
            {
                index = Mathf.Clamp(index, 0, items.Count - 1);
                return items[index];
            }
        }

        /// <summary>
        /// 初始化背包图案，并装载一个Inventory的数据。
        /// </summary>
        /// <param name="inventory"></param>
        virtual public void LoadInventory(Inventory inventory)
        {
            if (capacity == 0) 
            {
                items.Clear();
                items.AddRange(gameObject.GetComponentsInChildren<TItem>());
            }

            if (capacity != inventory.Capacity)
            {
                Debug.LogWarning("Warning inventory capacity do not match package inventory capacity ");
            }

            //在这里记录，inventory的数据。
            inventory_data = inventory;

            //初始化所有的格子，包括没数据的。
            for (int i = 0; i<items.Count; i++ )
            {
                items[i].InitInventoryItem(inventory,i);                
            }

            //为有数据的格子，载入数据。
            List<InventoryItemData> valid_data = inventory.InventoryDataSet;
            foreach (var inventory_item_data in valid_data)
            {
                if (inventory_item_data.count > 0 && inventory_item_data.slot_position < capacity)
                {
                    items[inventory_item_data.slot_position].SetItem(inventory_item_data);
                }
            }
        }
    }
}


