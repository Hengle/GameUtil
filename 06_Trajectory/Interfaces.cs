using UnityEngine;

namespace GameUtil.Trajectory
{
    /// <summary>
    /// 适用于非精瞄状态下的弹道，精瞄状态下有瞄具的真实位置决定弹道射线。
    /// 做一个简化模型，非真实弹道。
    /// </summary>
    public class SimpleDeviationParams:ScriptableObject
    {
        [Tooltip("射线有效Layer")]
        public string[] consider_layers;
        [Tooltip("是否考虑Trigger,默认False")]
        public bool consider_trigger = false;

        [Tooltip("静止时候的最高精度")]
        public float best_deviation = 0.001f;
        [Tooltip("最大移动误差，连续开枪也不会超过这个值")]
        public float worst_deviation = 0.04f;

        [Tooltip("一次开枪带来的瞬间Deviation增长值,注意这个值不会乘以DeltaTime")]
        public float impluse_deviation_delta = 0.01f;
        
        public float deviation_acceration = 1.0f;
        public float impluse_deviation_acceration = 10.0f;
        public float return_from_impluse = 20.0f;
    }

    public interface iTrajectory
    {
        SimpleDeviationParams deivation_params { set; }
        //do not insert speed value， insert the current speed / standard max speed
        float move_speed_rate { set; }
        float deviation { get; }
        bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit);
        void TickDeviation(float delta_time);
    }
}

