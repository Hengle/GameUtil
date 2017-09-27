using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    /// <summary>
    /// 这个照比Seek增加了两种控制。要学会通过这种机制去改变运动行为。
    /// 
    /// 【1】增加了制动距离，这个一般的Steering算法里面都有
    /// 【2】增加了速度衰间时间控制函数。
    /// 
    /// </summary>
    public class KinematicArrive:iOutputSteering<KinematicSteeringOutput>
    {
        public SteeringAgent character;
        public SteeringAgent target;
        public float max_speed;
        public float satisfaction_radius;//满意距离
        public const float C_TIME_TO_TARGET = 0.25f;

        public bool GetSteering(out KinematicSteeringOutput output)
        {
            //注意这里是不设上限的，但是，随着距离减小，速度会越来越慢，
            //当Velocity 小于 max_speed 之后，也就是实现Arrive的效果
            output.velocity = target.position - character.position;

            //待定：这里是不是要想Raycast一样，用bool做返回值呢？
            //接受上面的重构建议：这样使用方就能方便的知道，Steering是否终止。
            if (output.velocity.magnitude < satisfaction_radius)
            {
                output = default(KinematicSteeringOutput);
                return false;
            }

            //控制速度衰变开始的时机，如果C_TIME_TO_TARGET 大于一，则增大衰变开始
            //反之，则减少衰变开始的时间。
            output.velocity /= C_TIME_TO_TARGET;

            //这里会统一Clip一下，防止速度超标。
            if (output.velocity.magnitude > max_speed)
            {
                output.velocity = output.velocity.normalized;
                output.velocity *= max_speed;
            }

            character.orientation = SteerUtils.GetNewOrientation(character.orientation, output.velocity);
            output.rotation = 0;
           
            return true;
        }
    }

}
