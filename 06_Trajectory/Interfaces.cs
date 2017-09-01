using UnityEngine;

namespace GameUtil.Trajectory
{
    public class TrajectoryParams
    {
        [Tooltip("有效Layer，不填就没用，不填默认ConsiderTrigger无效")]
        public int valid_layers;
        [Tooltip("是否考虑Trigger,默认False")]
        public bool consider_trigger = false;
        [Tooltip("每米误差，如果精度不够，考虑改成，每十米误差。待定")]
        public float min_deviation;
        [Tooltip("最大误差，连续开枪也不会超过这个值")]
        public float max_deviation;
        [Tooltip("连续射击的时候，造成的放大误差")]
        public float shoot_deviation;
        [Tooltip("移动造成的精度偏移值")]
        public float move_deviation;
        [Tooltip("被击时的精度偏移值")]
        public float hit_deviation;
        [Tooltip("跳跃在空中时的精度偏移值")]
        public float air_deviation;
        [Tooltip("连续射击或移动的时候，回归最小误差点的速度,相当于缩圈速度")]
        public float shrink_aim_speed;
    }

    public interface iTrajectory
    {
        /// <summary>
        /// 根据不同的姿势，可以有不同的参数，可以在更换参数的时候，同时切换参数模板。
        /// </summary>
        void ReplaceTrajectorParam(TrajectoryParams param);
        void OnMove(bool is_begin);
        void OnHit();
        void OnAir(bool is_begin);
        bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit);
        void TickShrinkAim(float delta_time);
    }
}

