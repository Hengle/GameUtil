using UnityEngine;

namespace GameUtil.Trajectory
{
    /// <summary>
    /// 做成泛型，以后增加真实弹道的时候，也可以通用。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface iTrajectory<T> where T : ScriptableObject
    {
        void Init( T param, EPlayerStandingState state );
        //do not insert speed value， insert the current speed / standard max speed
        EPlayerStandingState player_pose { set; }
        float deviation { get; }
        bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit);
        void TickDeviation(float delta_time, float speed_rate );
    }
}

