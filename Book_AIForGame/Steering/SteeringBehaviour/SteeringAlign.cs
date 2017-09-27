using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public class SteeringAlign : iOutputSteering<SteeringOutput>
    {
        public SteeringAgent target;
        public SteeringAgent character;

        public float maxAccelerationgAngular;
        public float maxSpeedAngular;

        public float satisfiedRadius;
        public float slowDownRadius;

        public float timeToTargetSpeed = 0.1f;

        virtual protected float CalculateOrientation()
        {
            return (target.orientation - character.orientation) * Mathf.Deg2Rad;
        }

        virtual public bool GetSteering(out SteeringOutput output)
        {
            //第一步计算朝向差值
            float rotation = CalculateOrientation();
            
            //映射到-Pi到Pi区间
            rotation = SteerUtils.MapToRange(rotation);
            float rotationSize = Mathf.Abs(rotation);

            if (rotationSize < satisfiedRadius)
            {
                output = default(SteeringOutput);
                return false;//不再需要Steering了
            }

            float targetRotationSpeed;

            //如果没到最大值。那么取得最大值
            if (rotationSize > slowDownRadius)
            {
                targetRotationSpeed = maxSpeedAngular;
            }
            else
            {
                targetRotationSpeed = maxSpeedAngular * rotationSize / slowDownRadius;
            }

            //确定方向
            targetRotationSpeed *= rotation / rotationSize;

            output.angularAccerlation = targetRotationSpeed - character.angular;
            output.angularAccerlation /= timeToTargetSpeed;

            //Clip
            if (output.angularAccerlation > maxAccelerationgAngular)
            {
                output.angularAccerlation /= Mathf.Abs(output.angularAccerlation);
                output.angularAccerlation *= maxAccelerationgAngular;
            }

            output.linearAccerlation = Vector3.zero;
            return true;
        }



    }
}


