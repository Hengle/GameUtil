using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public class SteeringArrive : iOutputSteering<SteeringOutput>
    {
        public SteeringAgent character;
        public SteeringAgent target;
        public float maxAcceration;
        public float targetRadius;
        public float slowDownRadius;
        //Hold the time over which to achieve target speed;
        public float timerToTarget = 0.1f;


        private float targetSpeed = 0;
        private Vector3 targetVelocity;

        /// <summary>
        /// Quest：疑问点，targetVelocity到底是什么东西？
        /// Answer:这玩意是为了让速度从max平滑降到0的东西。
        /// 通过他计算一个反向加速度，用以减速刹车
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool GetSteering(out SteeringOutput output)
        {
            Vector3 direction = target.position - character.position;
            float distance = direction.magnitude;

            //中止条件：到达满意半径
            if (distance < targetRadius)
            {
                output = default(SteeringOutput);
                return false;
            }

            if (distance > slowDownRadius)
            {
                targetSpeed = character.max_speed;
            }
            else
            {
                //计算衰减的速度
                //注意 distance 》= targetRadius 且 《= slowDownRadius
                //相当于从1 变化到 targetRadius/slowDownRadius
                targetSpeed = character.max_speed * distance / slowDownRadius;
            }

            //目标速度结合了速度和方向
            targetVelocity = direction.normalized * targetSpeed;

            //timerToTarget是期望到达目标速度的时间
            output.linearAccerlation = targetVelocity - character.velocity;
            output.linearAccerlation /= timerToTarget;

            //检查加速度是否过快
            if (output.linearAccerlation.magnitude > maxAcceration)
            {
                output.linearAccerlation = output.linearAccerlation.normalized * maxAcceration;
            }

            //设置角加速度
            output.angularAccerlation = 0;

            return true;
        }
    }
}