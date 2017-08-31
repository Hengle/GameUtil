using UnityEngine;
using System.Collections;
using GameUtil.UI;
using GameUtil.OpenWorld;

namespace GameUtil.Examples
{

    [System.Serializable]
    public class PickableObjectData : WorldSaveObjectData 
    {
        public InventoryItemData inventory_data;
        public PickableObjectData(PickableObject po) 
        {
            po.SaveBasicInfo(this);
            this.inventory_data = po.inventory_data;
        }
    }

    public class PickableObject : InteractObject 
    {
        const string C_DEFAULT_PICKUP_HINT = "PICKUP";

        public InventoryItemData inventory_data;

        public override void OnInteracted(LocalPlayer toucher)
        {
            int item_id = inventory_data.item_id;
            UGUIInventoryItemConfig info = GameSettings.GetInventoryItemConfig(item_id);
            int max_stack = info.max_stack;

            EInventoryOperateResult result = toucher.inventory.TryAddItem(item_id, ref inventory_data.count, max_stack);
            Debug.Log("Pick up result " + result);

            if (result == EInventoryOperateResult.Success) 
            {
                GameObject.Destroy(this.gameObject);
            }

        }

        public override string HintString
        {
            get
            {
                return GameSettings.GetString(C_DEFAULT_PICKUP_HINT)+"("+GetInstanceID()+")";
            }
        }

        public override WorldSaveObjectData Save()
        {
            return new PickableObjectData( this );
        }

        /// <summary>
        /// 读取GameObject的特殊部分，前面的部分可以统一处理掉。
        /// </summary>
        /// <param name="wod"></param>
        public override void Load(WorldSaveObjectData wod)
        {
            PickableObjectData pod = wod as PickableObjectData;
            this.inventory_data = pod.inventory_data;
        }
    }

}
