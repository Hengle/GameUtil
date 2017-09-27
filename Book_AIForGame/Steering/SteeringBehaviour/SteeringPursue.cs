using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    /// <summary>
    /// 追逐算法
    /// </summary>
    public class SteeringPursue : SteeringSeek
    {
        float MaxPredictionTime = 0.1f;

        /// <summary>
        /// 只要重写这个方法，将Target的预判Position写进去就OK了，完美！
        /// </summary>
        public override Vector3 targetPosition
        {
            get
            {
                Vector3 direction = target.position - character.position;
                float distance = direction.magnitude;

                float speed = character.velocity.magnitude;
                float real_prediction_time;

                //速度太小，预测直接取最大预测时间，否则取距离/速度。分析：离得越远，速度越小，预测的越远？
                if (speed < distance / MaxPredictionTime)
                {
                    real_prediction_time = MaxPredictionTime;
                }
                else
                {
                    real_prediction_time = distance / speed;
                }

                //简单的将当前敌人的速度方向*预测时间，算出预判移动位置。
                return target.position + target.velocity * real_prediction_time;
            }
        }
    }

}
