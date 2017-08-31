using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameUtil;

namespace GameUtil.Examples 
{
    /// <summary>
    /// 表示一个可放置物体的当前状态。
    /// Normal表示普通状态，应该播放正常使用的Shader。
    /// Conflict表示冲突状态，不可放置，点左键也不会有反应。
    /// Dropable表示可放置状态。再次点击左键就放置物体。
    /// </summary>
    public enum EPlaceableObjectState
    {
        Normal,
        Conflict,
        Dropable,
    }

    public class PlacingBuildingDetector : MonoBehaviour
    {
        [HideInInspector]   public BuildingObject building_object;
        [HideInInspector]   public BuildingBlockConfig building_config;
        Dictionary<EBuildingPlacingType, System.Func<EPlaceableObjectState>> drop_building_handler = new Dictionary<EBuildingPlacingType, System.Func<EPlaceableObjectState>>();

        void OnEnable() 
        {
            drop_building_handler.Clear();
            drop_building_handler[EBuildingPlacingType.SlotOnly] = HandleDropBuilding_SnapOnly;
            drop_building_handler[EBuildingPlacingType.SnapToBuildingSurface] = HandleDropBuilding_SnapToBuilding;
            drop_building_handler[EBuildingPlacingType.TraceTerrain] = HandleDropBuilding_TracingTerrain;
        }

        public void AttachPendingDropBuilding(BuildingObject bo, BuildingBlockConfig bbi) 
        {
            building_object = bo;
            building_object.transform.SetParent(this.transform);
            building_object.transform.localPosition = Vector3.zero;
            building_object.transform.localEulerAngles = Vector3.zero;
            
            building_config = bbi;           
        }

        void FixedUpdate()
        {
            if (building_object != null && building_config != null) 
            {
                building_object.SwitchDisplayState(drop_building_handler[building_config.placing_type](), building_config);
            }
        }

        EPlaceableObjectState HandleDropBuilding_SnapOnly() 
        {
            if (building_object.SnapCount > 0) 
            {
                return EPlaceableObjectState.Dropable;
            }

            return EPlaceableObjectState.Conflict;
        }

        EPlaceableObjectState HandleDropBuilding_TracingTerrain()
        {

            //Trace Box toward obstacles
            string[] masked_obstacles = { "Default", "Buildings" };
            BoxCollider[] bounds = building_object.BoundingBoxes;

            if( bounds.Length > 0 )
            {
                for (int i = 0; i < bounds.Length; i++)
                {
                    if (Physics.CheckBox(bounds[i].transform.position + bounds[i].center, bounds[i].size / 2, bounds[i].transform.rotation, LayerMask.GetMask(masked_obstacles), QueryTriggerInteraction.Ignore))
                    {
                        Debug.Log("Hit Bound " + i);
                        return EPlaceableObjectState.Conflict;
                    }
                }
            }

            //Trace Ground
            string[] masked_layers = { "Terrain" };
            int trace_hit = 0;
            int trace_require_hit = building_config.trace_probs.Count;
            for (int i = 0; i < trace_require_hit; i++)
            {
                TracingProbe tp = building_config.trace_probs[i];
                if (Physics.Raycast(new Ray(transform.position + tp.relative_position, TracingProbe.GetTracingDirection(tp.direction)), tp.distance, LayerMask.GetMask(masked_layers)))
                {
                    trace_hit++;
                }
            }

            if (trace_hit == trace_require_hit) 
            {
                return EPlaceableObjectState.Dropable;
            }

            return EPlaceableObjectState.Conflict;
        }

        EPlaceableObjectState HandleDropBuilding_SnapToBuilding() 
        {
            return EPlaceableObjectState.Conflict;
        }

        public void DetachDropedBuilding() 
        {
            building_object = null;
            building_config = null;
        }

        public void Clear() 
        {
            if (building_object != null) 
            {
                Destroy(building_object.gameObject);
            }

            DetachDropedBuilding(); 
        }
    }

}
