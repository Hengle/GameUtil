using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Trajectory
{
    /// <summary>
    /// 适用于非精瞄状态下的弹道，精瞄状态下有瞄具的真实位置决定弹道射线。
    /// 做一个简化模型，非真实弹道。
    /// </summary>
    public class UnrealisticDeviationParams : ScriptableObject
    {
        public string[] layers;
        public bool trace_trigger = false;
        public float max_distance = 400.0f;

        public float best_deviation = 0.001f;
        public float worst_deviation = 0.05f;

        [System.Serializable]
        public struct PoseModifier
        {
            public EPlayerStandingState body_state;
            public float modify_rate;
        }

        //根据不同的身体姿势，确定不同的最大最小缩圈比率。
        public PoseModifier[] gesture_modifiers;

        public float impluse_deviation_delta = 0.01f;
    }
}