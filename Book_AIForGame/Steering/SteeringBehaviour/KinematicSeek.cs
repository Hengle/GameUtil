using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{

    public class KinematicSeek 
    {
        public SteeringAgent owner;
        public SteeringAgent target;
        public float max_speed;

        virtual protected int flee
        {
            get
            {
                return 1;
            }
        }
        
        //为了方便处理，这里我把弧度转换成角度了。因为最后Rotation接受的是角度
        //这个函数以后会提取出去，目前还比较懵逼，不知道这应该归为哪一类……
        //看明白了，这个应该属于KinematicSeek
        public float GetNewOrientation(float current_orientation, Vector3 velocity)
        {
            if (velocity.magnitude > 0)
            {
                return Mathf.Atan2(-velocity.x, velocity.z) * Mathf.Rad2Deg;
            }
            return current_orientation;
        }

        public KinimaticSteeringOutput GetSteering()
        {
            KinimaticSteeringOutput steering;
            steering.velocity = (target.position - owner.position) * flee;

            if (steering.velocity.magnitude > max_speed)
            {
                steering.velocity = steering.velocity.normalized * max_speed;
            }

            //这样合适嘛，你直接就在这里把朝向给改了？作者你也太不负责任了。
            owner.orientation = GetNewOrientation(owner.orientation, steering.velocity);

            steering.rotation = 0;
            return steering;
        }

    }
}
