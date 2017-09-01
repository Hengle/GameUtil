using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Trajectory 
{
    /// <summary>
    /// 为射击游戏设计的 基于球体的 弹道函数
    /// 只提供
    /// </summary>
    public class SphereDeviationTrajectory : MonoBehaviour,iTrajectory
    {
        SimpleDeviationParams param;

        const float C_MOVE_SPEED_THRESHOLD = 0.1f;
        float current_deviation;
        float current_deviation_accerlation;
        float target_deviation;        
        float speed_rate;

        public SimpleDeviationParams deivation_params
        {
            set 
            {
                param = value; 
            }
        }

        public float move_speed_rate
        {
            set 
            {
                speed_rate = value;
            }
        }

        public bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit)
        {
            target_deviation += param.impluse_deviation_delta;
            ClampTargetDeviation();

            current_deviation_accerlation = param.impluse_deviation_acceration;   
         
            return true;
        }


        public void TickDeviation(float delta_time)
        {
            ClampTargetDeviation();

            current_deviation = Mathf.Lerp(current_deviation, target_deviation, current_deviation_accerlation);

            current_deviation_accerlation = Mathf.Lerp(current_deviation_accerlation, param.deviation_acceration, param.return_from_impluse);
        }

        private void ClampTargetDeviation() 
        {
            float rate = 1.0f + speed_rate;
            target_deviation = Mathf.Clamp(target_deviation, param.best_deviation * rate, param.worst_deviation * rate);
        }


        public float deviation
        {
            get 
            {
                return current_deviation;
            }
        }
    }
}
