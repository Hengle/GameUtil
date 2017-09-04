using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Trajectory 
{
    /// <summary>
    /// 为射击游戏设计的 基于球体的 弹道函数
    /// 只提供
    /// </summary>
    public class SphereDeviationTrajectory : MonoBehaviour,iTrajectory<UnrealisticDeviationParams>
    {
        #region Static Methods

        /// <summary>
        /// 静态方法，获得已知半径r的球体内的随机一个点，作为被弹偏移点。
        /// 这样做就不用考虑，被弹平面与弹道垂直等烂七八糟的问题了。
        /// 
        /// 如果将Deviation球体固定于摄影机正前方，这可以更快的计算弹道偏移量。
        /// 2017年4月12日16:18:33 当前的算法还没有考虑，射击之后，准星归位的问题，
        /// FPS在准星归位前再度射击，精度会进一步下降。
        /// 但是对于策略坦克射击游戏，则不会有这个问题。默认每次射击都是稳定后再开炮的。
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 GetRandomPointInSphere(float r)
        {
            Vector3 shift = Vector3.zero;

            float theta = Random.Range(0, Mathf.PI * 2);
            shift.x = r * Mathf.Cos(theta) * Random.Range(-1.0f, 1.0f);
            shift.y = r * Mathf.Sin(theta) * Random.Range(-1.0f, 1.0f);

            float z_max = Mathf.Sqrt(r * r - new Vector2(shift.x, shift.y).sqrMagnitude);
            shift.z = r * Random.Range(-z_max, z_max);

            return shift;
        }


        /// <summary>
        /// 注意参数必须都是世界坐标，Trace传局部坐标几个意思？
        /// 先按照绝对精准计算一下目标位置
        /// 然后按照偏移球体计算随机偏移量
        /// 最后算出实际的命中点。
        /// 根据这个点可以方便的算出实际弹道
        /// </summary>
        /// <param name="start_position"></param>
        /// <param name="aim_direction"></param>
        /// <returns></returns>
        protected Vector3 GetFinalTracePositionByAimDirection(Vector3 start_position, Vector3 aim_direction)
        {
            RaycastHit hit;
            Vector3 normal_aim_direction = aim_direction.normalized;

            if (param.layers != null && param.layers.Length > 0) 
            {
                Physics.Raycast(start_position, normal_aim_direction, out hit, param.max_distance, LayerMask.GetMask( param.layers ));
            }
            else 
            {
                Physics.Raycast(start_position, normal_aim_direction, out hit, param.max_distance);
            }
            

            if (hit.collider == null)
            {
                //实际上不可能，除非是向天上乱开枪，那样只要没有弹线，也无所谓啦，理论可以返回Zero，但是实际上不可能返回Zero的。
                //这种情况，直接把原来目标返回去拉倒
                return start_position + normal_aim_direction * param.max_distance;
            }
            else
            {
                Vector3 position = hit.point;

                float distance = (hit.point - start_position).magnitude;
                float accuracy = distance * current_deviation;
                return position + GetRandomPointInSphere(accuracy);
            }
        }

        #endregion

        UnrealisticDeviationParams param;    

        const float C_MOVE_SPEED_THRESHOLD = 0.1f;
        const float C_MIN_DEVIATION_CHANGE_SPEED = 0.1f;

        float current_deviation;
        float target_deviation;
        float current_max;
        float current_min;

        Dictionary<EPlayerStandingState, float> gesture_rate_mapping = new Dictionary<EPlayerStandingState, float>();

        public void Init(UnrealisticDeviationParams param, EPlayerStandingState state)
        {
            this.param = param;
            gesture_rate_mapping.Clear();
            foreach (var item in param.gesture_modifiers)
            {
                gesture_rate_mapping[item.body_state] = item.modify_rate;
            }
                        
            player_pose = state;
            target_deviation = current_max;
        }

        public EPlayerStandingState player_pose
        {
            set 
            {
                current_min = param.best_deviation * gesture_rate_mapping[value];
                current_max = param.worst_deviation * gesture_rate_mapping[value];
                target_deviation = Mathf.Clamp(target_deviation, current_min, current_max);
            }
        }        
        
        public bool Fire(Vector3 start_pos, Vector3 direction, out RaycastHit hit)
        {
            current_deviation += param.impluse_deviation_delta;
            current_deviation = Mathf.Clamp(current_deviation, current_min, current_max);

            //计算逻辑
            Vector3 real_hit_position = GetFinalTracePositionByAimDirection(start_pos, direction);
            bool result = false;
            if (param.layers != null && param.layers.Length > 0)
            {
                result = Physics.Raycast(start_pos, (real_hit_position - start_pos).normalized, out hit, param.max_distance, LayerMask.GetMask(param.layers));
            }
            else 
            {
                result = Physics.Raycast(start_pos, (real_hit_position - start_pos).normalized, out hit, param.max_distance );
            }

            return result;
        }

        public float deviation
        {
            get 
            {
                return current_deviation;
            }
        }
        
        public void TickDeviation(float delta_time, float speed_rate)
        {
            //简化！
            if (speed_rate > C_MOVE_SPEED_THRESHOLD)
            {
                target_deviation = current_max;
            }
            else 
            {
                target_deviation = current_max;
            }

            float change_speed = Mathf.Max( C_MIN_DEVIATION_CHANGE_SPEED, Mathf.Abs( target_deviation - current_deviation)  );

            current_deviation = Mathf.Lerp(current_deviation, target_deviation, change_speed);
        }
    }
}
