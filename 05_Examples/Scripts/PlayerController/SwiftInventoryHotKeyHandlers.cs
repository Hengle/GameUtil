using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil.UI;

namespace GameUtil.Examples 
{
    public interface ISwiftInventoryTakeOut
    {
        bool OnTakeOut(UGUIInventoryItemConfig item_config, LocalPlayer local_player);
    }
    
    public class TakeOut_BuildingObject : ISwiftInventoryTakeOut
    {
        public bool OnTakeOut(UGUIInventoryItemConfig item_config, LocalPlayer local_player)
        {
            if (item_config.GetType() == typeof(BuildingBlockConfig))
            {
                GameObject gobj = GameObject.Instantiate(item_config.prefab);

                BuildingBlockParams bbp = local_player.building_block_params;

                BuildingObject bo = gobj.GetComponent<BuildingObject>();
                BuildingBlockConfig bbi = item_config as BuildingBlockConfig;

                if( bo != null && bbi != null )
                {
                    bbp.placing_building_detector.AttachPendingDropBuilding(bo, bbi);
                    bbp.building_block_orb_distance = bbi.drop_orb_distance;
                }
            }
            return false;
        }
    }

    public class TakeOut_ConsumableItem : ISwiftInventoryTakeOut 
    {
        public bool OnTakeOut(UGUIInventoryItemConfig item_config, LocalPlayer local_player) 
        {
            return false;
        }
    }

    public class TakeOut_Weapon : ISwiftInventoryTakeOut 
    {
        public bool OnTakeOut(UGUIInventoryItemConfig item_config, LocalPlayer local_player)
        {
            return false;
        }
    }
}