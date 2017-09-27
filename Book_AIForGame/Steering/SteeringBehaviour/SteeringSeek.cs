using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public class SteeringSeek:iOutputSteering<SteeringOutput>
    {
        public SteeringAgent character;
        public SteeringAgent target;
        public float MaxAcceleration;
        public bool flee;

        virtual public Vector3 targetPosition
        {
            get
            {
                return target.position;
            }
        }
             

        public bool GetSteering(out SteeringOutput output)
        {
            //get the direction to the target.
            output.linearAccerlation = targetPosition - character.position;

            //不在单独处理Flee了，省时间
            if (flee)
                output.linearAccerlation *= -1;

            //give full acceleration along this direction
            output.linearAccerlation.Normalize();
            output.linearAccerlation *= MaxAcceleration;

            output.angularAccerlation = 0;

            return true;
        }
    }
}