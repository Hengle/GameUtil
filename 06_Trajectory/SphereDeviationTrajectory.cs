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
        TrajectoryParams t_param;
        float current_deviation;
        float target_deviation;
        

        public void ReplaceTrajectorParam(TrajectoryParams param)
        {
            this.t_param = param;
            if(this.current_deviation < t_param.min_deviation)
            {
                this.current_deviation = t_param.min_deviation;
            }
            this.target_deviation = this.current_deviation;
        }
        
        public void OnMove(bool is_begin)
        {
            throw new System.NotImplementedException();
        }

        public void OnHit()
        {
            throw new System.NotImplementedException();
        }

        public void OnAir(bool is_begin)
        {
            throw new System.NotImplementedException();
        }

        public bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit)
        {
            throw new System.NotImplementedException();
        }

        public void TickShrinkAim(float delta_time)
        {
            throw new System.NotImplementedException();
        }
    }
}
