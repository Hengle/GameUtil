using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil.OpenWorld;

namespace GameUtil.Examples
{
    public class ActionButtonHandler_Default :IPressStateHandler
    {  
        public void OnBeginPress() 
        {
            Debug.Log("Default Action / Fire ");
        }

        public void OnRelease() 
        {

        }

        public void OnPressing() 
        {

        }
    }

    public class ActionButtonHandler_CanDropBuilding : IPressStateHandler 
    {
        public void OnBeginPress()
        {
            Debug.Log("Drop Building Block");
            LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();

            WorldManager world_mgr = GameFacade.Instance.GetWorldManager();

            Transform parent = world_mgr.FindTransform("BuildingBlocks");
            if (parent != null) 
            {
                PlacingBuildingDetector pbd = lp.building_block_params.placing_building_detector;

                //先改变父节点
                BuildingObject bo = pbd.building_object;
                BuildingBlockConfig bbi = pbd.building_config;

                if (bo != null && bbi != null) 
                {
                    //修改物件的父节点
                    bo.transform.SetParent(parent);

                    //在取消联系。
                    lp.building_block_params.placing_building_detector.DetachDropedBuilding();

                    //改变Shader
                    bo.SwitchDisplayState(EPlaceableObjectState.Normal, bbi);

                    //消耗库存
                    lp.CostSwiftSlotItem();
                }               
            }

        }

        public void OnRelease()
        {

        }

        public void OnPressing()
        {

        }
    }
}
