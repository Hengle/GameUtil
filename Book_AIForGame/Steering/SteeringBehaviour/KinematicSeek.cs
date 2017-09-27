using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{

    public class KinematicSeek :iOutputSteering<KinematicSteeringOutput>
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
        


        //在这个最简单的算法里面，速度是恒定的。
        public bool GetSteering(out KinematicSteeringOutput steering)
        {
            steering.velocity = (target.position - owner.position) * flee;
            steering.velocity = steering.velocity.normalized * max_speed;

            //这样合适嘛，你直接就在这里把朝向给改了？作者你也太不负责任了。
            owner.orientation = SteerUtils.GetNewOrientation(owner.orientation, steering.velocity);

            steering.rotation = 0;
            return true;
        }
    }
}
