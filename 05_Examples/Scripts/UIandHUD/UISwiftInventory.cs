using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;
using GameUtil.UI;

namespace GameUtil.Examples
{

    public class UISwiftInventory : UGUIInventory<UGUIInventoryItem>
    {
        LocalPlayer p_localplayer = null;
        LocalPlayer local_player 
        {
            get 
            {
                if (p_localplayer == null) 
                {
                    p_localplayer = GameFacade.Instance.GetLocalPlayer();
                }
                return p_localplayer;
            }
        }


        
        void OnPressShootCut(int short_cut_index)
        {
            if (local_player.OnSwiftInventoryUpdate == null) 
            {
                local_player.OnSwiftInventoryUpdate = LoadInventory;
            }

            InventoryItemData item_data = inventory_data[short_cut_index];

            if (item_data != null && item_data.count > 0)
            {
                UGUIInventoryItemConfig item_config = GameSettings.GetInventoryItemConfig(item_data.item_id);
                local_player.TakeOut(item_config as BuildingBlockConfig,short_cut_index);
            }
            else 
            {
                local_player.TakeOut(null, short_cut_index);
            }
        }

	    // Update is called once per frame
        // When open inventory, you can not use hotkey to switch building block.
	    void Update () 
        {
            if (local_player.operation_state != EOperationState.Managing_Inventory) 
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) OnPressShootCut(0);
                if (Input.GetKeyDown(KeyCode.Alpha2)) OnPressShootCut(1);
                if (Input.GetKeyDown(KeyCode.Alpha3)) OnPressShootCut(2);
                if (Input.GetKeyDown(KeyCode.Alpha4)) OnPressShootCut(3);
                if (Input.GetKeyDown(KeyCode.Alpha5)) OnPressShootCut(4);
                if (Input.GetKeyDown(KeyCode.Alpha6)) OnPressShootCut(5);
                if (Input.GetKeyDown(KeyCode.Alpha7)) OnPressShootCut(6);
                if (Input.GetKeyDown(KeyCode.Alpha8)) OnPressShootCut(7);
                if (Input.GetKeyDown(KeyCode.Alpha9)) OnPressShootCut(8);
            }
	    }
    }
}