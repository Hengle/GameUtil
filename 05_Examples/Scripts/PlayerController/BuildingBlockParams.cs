using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Examples 
{
    /// <summary>
    /// 用来存放，和放置建筑相关的临时变量和参数。
    /// 
    /// LocalPlayer(1)-->(1)BuildingBlockParams
    /// 
    /// </summary>
    [System.Serializable]
    public class BuildingBlockParams
    {
        public float building_block_max_distance = 10;
        public float building_block_min_distance = 3;
        public float rotate_factor = 120;
        public float zoom_speed = 30;
        public PlacingBuildingDetector placing_building_detector;


        private Transform p_trans_cache;
        Transform building_block_refernece_transform 
        {
            get 
            {
                if( p_trans_cache == null )
                {
                    p_trans_cache = placing_building_detector.transform;
                }
                return p_trans_cache;
            }
        }

       
        public float building_block_orb_distance
        {
            get
            {
                return building_block_refernece_transform.localPosition.z;
            }
            set
            {
                Vector3 pos = building_block_refernece_transform.localPosition;
                pos.z = value;
                pos.z = Mathf.Clamp(pos.z, building_block_min_distance, building_block_max_distance);
                building_block_refernece_transform.localPosition = pos;
            }
        }

        

        public void Rotate( float angle = 99999999 ) 
        {
            if (angle > 99999998)
            {
                float rotate_speed = Time.deltaTime * rotate_factor;
                placing_building_detector.building_object.Rotate(rotate_speed);
            }
            else 
            {
                placing_building_detector.building_object.Rotate(angle); 
            }            
        }
    }
}