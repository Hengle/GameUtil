using UnityEngine;

namespace GameUtil.Trajectory
{


    /// <summary>
    /// 适用于飞精瞄状态下的弹道，精瞄状态下有瞄具的真实位置决定弹道射线。
    /// </summary>
    public class TrajectoryParams:ScriptableObject
    {
        [Tooltip("有效Layer，不填就没用，不填默认ConsiderTrigger无效")]
        public int valid_layers;
        [Tooltip("是否考虑Trigger,默认False")]
        public bool consider_trigger = false;

        [Tooltip("每米误差，如果精度不够，考虑改成，每十米误差。待定")]
        public float min_deviation;
        [Tooltip("最大误差，连续开枪也不会超过这个值")]
        public float max_deviation;
        [Tooltip("偏移放大速度")]
        public float deviation_speed;
        [Tooltip("缩圈速度，应该比放大速度略小")]
        public float shrink_speed;

        [Tooltip("以下值影响放大，最大系数，不影响缩圈系数，暂定缩圈系数是恒定的这是为了简化模型。")]
        public float stand_fire_rate = 1.0f;
        public float stand_move_rate = 1.0f;
        public float couch_fire_rate = 0.75f;
        public float couch_move_rate = 0.75f;
    }

    public interface iTrajectory
    {
        TrajectoryParams trajectory_params { get; set; }        
        void OnMove(bool is_begin);
        void OnAir(bool is_begin);
        bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit);
        void TickShrinkAim(float delta_time);
    }
}

