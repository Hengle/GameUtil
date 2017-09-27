using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{


    public class KinematicWander : iOutputSteering<KinematicSteeringOutput>
    {
        public SteeringAgent character;
        public float max_speed;
        public float max_rotation;

        public bool GetSteering(out KinematicSteeringOutput output)
        {
            //get Velocity form orientation
            output.velocity = max_speed * SteerUtils.OrientationToVector3_XZ(character.orientation);

            //Q:每帧都变吗，不会有问题吗？GetSteering不是应该每帧都调用的吗啊？还是可以隔一段时间调用过一次？
            output.rotation = SteerUtils.RandomBinomial() * max_rotation;

            return true;
        }
    }
}