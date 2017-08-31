using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Examples 
{
    public enum EBuildingSnapSlotType 
    {
        Horizon_Full,
        Vertical_Full,
        Stair_Top,
        Stair_Bottom,
        Pillar_Top,
        Pillar_Middle,
        Pillar_Bottom,
        Wall_Top,
        Wall_Middle,
        Wall_Bottom,
        Wall_Support,
        InnerStairBottom,
        Floor,
    }

    public enum EBuildingSnapLimitType 
    {
        Unlimit,
        Unique_Slot_Check,
    }

    [System.Serializable]
    public class BuildingSnapLimitCondition
    {
        public string desc;
        public EBuildingSnapSlotType slot_type;
        public EBuildingSnapLimitType limit_type = EBuildingSnapLimitType.Unlimit;        
        public Vector3 check_position_shift;
        public Vector3 check_direction;
        public float check_distance;
    }

    public class BuildingSnapSlot : MonoBehaviour
    {
        [HideInInspector]
        public BuildingObject building_owner;

        //向外发送的Slot类型。
        public EBuildingSnapSlotType output_type = EBuildingSnapSlotType.Horizon_Full;
        public List<BuildingSnapLimitCondition> limit_conditions = new List<BuildingSnapLimitCondition>();

        /// <summary>
        /// 当前Slot到中心的位移
        /// </summary>
        public Vector3 SlotToCenterShift 
        {
            get 
            {
                return this.transform.position - building_owner.transform.position;
            }
        }

        public static string[] buildings_mask = { "Buildings" };

        public bool Accept(BuildingSnapSlot target) 
        {
            BuildingSnapLimitCondition bsc = limit_conditions.Find(x => x.slot_type == target.output_type);
            if (bsc != null) 
            {
                switch (bsc.limit_type)
                {
                    case EBuildingSnapLimitType.Unlimit:
                        return true;
                    case EBuildingSnapLimitType.Unique_Slot_Check:
                        Debug.Log("BeginSlotCheck : Unique_Slot_Check pos " + (transform.position + bsc.check_position_shift) + " dir " + bsc.check_direction);


                        LineRenderer lr = GetComponent<LineRenderer>();
                        if (lr != null) 
                        {
                            lr.SetPosition(0, transform.position + bsc.check_position_shift);
                            lr.SetPosition(1, transform.position + bsc.check_position_shift + bsc.check_direction * bsc.check_distance);
                        }

                        RaycastHit result;

                        Physics.Raycast(
                            transform.position + bsc.check_position_shift, 
                            bsc.check_direction,
                            out result, 
                            bsc.check_distance,
                            LayerMask.GetMask(buildings_mask),
                            QueryTriggerInteraction.Ignore);
                                                
                        Debug.Log("snap test " + result.collider);

                        if (result.collider == null) 
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        void OnTriggerEnter(Collider other)
        {
            ////冲突状态，不要进行吸附，放置两个物块重叠！
            //LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();
            //if (lp.operation_state == EOperationState.Building_Confict) 
            //{
            //    return;
            //}

            //已经放好的，就不要在吸附了，被吸附就可以了
            if (building_owner.BuildingBlockState == EPlaceableObjectState.Normal) 
            {
                return;
            }

            if (other.gameObject.tag == "Slot" ) 
            {
                BuildingSnapSlot temp_target = other.gameObject.GetComponent<BuildingSnapSlot>();

                //不接受的部件，不要进行吸附
                if( temp_target.Accept( this ) )
                {
                    this.building_owner.SnapToSlot(this, temp_target);
                }
            }
        }
    }

}

