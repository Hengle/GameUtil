using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Examples 
{
    public class ScrollWheelHandlers 
    {
        public static void Scroll_Default(float val) 
        {
            Debug.Log("Scroll delta value " + val);
        }

        public static void Scroll_ZoomBuildingObject(float val) 
        {
            LocalPlayer local_player = GameFacade.Instance.GetLocalPlayer();
            BuildingBlockParams bbp = local_player.building_block_params;
            bbp.building_block_orb_distance += val;
        }
    }
}
