using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public class KinematicArrive
    {
        public SteeringAgent character;
        public SteeringAgent target;
        public float max_speed;
        public float satisfaction_radius;//满意距离
        public const float C_TIME_TO_TARGET = 0.25f;
    }

}
