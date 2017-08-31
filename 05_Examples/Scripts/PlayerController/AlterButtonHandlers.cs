using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Examples 
{
    public class AlterButtonHandler_Default:IPressStateHandler 
    {
        public void OnBeginPress()
        {
            Debug.Log("Default Alter Action/ Alter Fire ");
        }

        public void OnRelease()
        {

        }

        public void OnPressing()
        {

        }
    }

    public class AlterButtonHandler_PlacingBuilding : IPressStateHandler 
    {
        public void OnBeginPress()
        {
            LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();
            BuildingBlockParams bbp = lp.building_block_params;
            BuildingObject bo = lp.building_block_params.placing_building_detector.building_object;

            if (bo.SnapCount > 0) 
            {
                bbp.Rotate(90);
            }
        }

        public void OnRelease()
        {

        }

        public void OnPressing()
        {
            LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();
            BuildingBlockParams bbp = lp.building_block_params;
            BuildingObject bo = lp.building_block_params.placing_building_detector.building_object;

            if (bo.SnapCount == 0) 
            {
                bbp.Rotate();
            }            
        }
    }
}
