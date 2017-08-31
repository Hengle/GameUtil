using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil.UI;

namespace GameUtil.Examples
{
    public enum EBuildingTracingDirection 
    {
        UP,     //Y
        Down,   //-Y
        Front,  //Z
        Back,   //-Z
        Left,   //-X
        Right,  //X
    }

    /// <summary>
    /// Exclude with other collider except terrain, is always a required condition.
    /// </summary>
    public enum EBuildingPlacingType
    {
        SlotOnly,                   //Only Slot Hit Will return true
        SnapToBuildingSurface,      //Tracing Building hit, Snap to building surface, Slot hit is a bonus option. need exclude other buildings.
        TraceTerrain,               //Tracing Terrain hit, Slot hit is a bonus option. need exclude other buildings.
    }

    [System.Serializable]
    public class TracingProbe 
    {
        public EBuildingTracingDirection direction = EBuildingTracingDirection.Down;
        public Vector3 relative_position;//Position relative to center of object;
        public float distance;

        public static Vector3 GetTracingDirection( EBuildingTracingDirection dir ) 
        {
            switch (dir) 
            {
                case EBuildingTracingDirection.UP:      return Vector3.up;
                case EBuildingTracingDirection.Down:    return Vector3.down;
                case EBuildingTracingDirection.Front:   return Vector3.forward;
                case EBuildingTracingDirection.Back:    return Vector3.back;
                case EBuildingTracingDirection.Left:    return Vector3.left;
                case EBuildingTracingDirection.Right:   return Vector3.right;
            }

            //默认向下Trace
            return Vector3.down;
        }
    }

    /*
    public enum EBuildingBlockType 
    {
        Fundation_X4_Legs,  //四个脚的地基。
        Stair_X4_Legs,      //是个角的2格高度的楼梯。
    }*/

    [System.Serializable]
    public class BuildingBlockConfig : UGUIInventoryItemConfig
    {
        public EBuildingPlacingType placing_type = EBuildingPlacingType.TraceTerrain;     
        public List<TracingProbe> trace_probs = new List<TracingProbe>();
        public float drop_orb_distance = 4;
        public bool need_absorb = true;
        public Material standard;
        public Material confict;
        public Material enable;
    }

}
