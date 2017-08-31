using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil.UI;
using GameUtil.OpenWorld;

namespace GameUtil.Examples 
{
    [System.Serializable]
    public class StorageObjectData : BuildingObjectData 
    {
        public Inventory inventory_data;

        public StorageObjectData(StorageObject so)
            : base(so) 
        {
            this.inventory_data = so.StorageInventory;
        }
    }

    public class StorageObject : BuildingObject
    {
        const string C_STORAGE_DEFAULT = "CHEST";

        void OnEnable() 
        {
            interact_type = EInteractType.Storage;
        }

        /// <summary>
        /// 这个不应该存放到这里，而应该通过一个ID，去WorldManager去检索。也不适合记录在Player身上
        /// 万一以后做联网版，Player没在线，Chest的数据就没有了
        /// 
        /// 当然，并不需要考虑MMO那种有专用服务器的情况，能处理宿主主机即可。
        /// 
        /// TODO：25以后改成参数。当然
        /// 需要打开的页面，也作为参数！当然，也可以，只有一种，Storage物件，哈哈哈哈
        /// </summary>
        Inventory storage_inventory = new Inventory(25);

        public Inventory StorageInventory 
        {
            get 
            {
                return storage_inventory;
            }
        }


        public override void OnInteracted(LocalPlayer toucher)
        {
            base.OnInteracted(toucher);
            UIInventory chest_25_slot = UGUIManager.Instance.OpenDialog<UIInventory>("Chest25Slot");
            UIInventory player_inventory = UGUIManager.Instance.OpenDialog<UIInventory>("Player50Slot");

            chest_25_slot.LoadInventory(storage_inventory);
            chest_25_slot.AppendCascadeClosing(player_inventory);
            chest_25_slot.widget_position = new Vector2(-410, 0);
            
            player_inventory.LoadInventory(toucher.inventory);
            player_inventory.AppendCascadeClosing(chest_25_slot);
            player_inventory.widget_position = new Vector2(210, 0);
        }

        public override string HintString
        {
            get
            {
                return GameSettings.GetString(C_STORAGE_DEFAULT) + "("+GetInstanceID()+")";
            }
        }

        /// <summary>
        /// 这里必须要调用父类的Load方法，因为存储物件，首先是一个建筑物件。
        /// </summary>
        /// <param name="data"></param>
        public override void Load(WorldSaveObjectData data)
        {
            base.Load(data);
            StorageObjectData sod = data as StorageObjectData;
            this.storage_inventory = sod.inventory_data;
        }

        public override WorldSaveObjectData Save()
        {
            return new StorageObjectData(this);
        }
    }

}
