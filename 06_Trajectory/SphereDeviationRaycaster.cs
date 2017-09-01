using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDeviationRaycaster : MonoBehaviour
{
    [SerializeField]
    float max_distance = 100;
    [SerializeField][Range(0.001f,0.999f)]
    float deviation_per_meter;   //uu is short for unity unit.equal to meter
    virtual public float DeviationPerUnit
    {
        get { return deviation_per_meter; }
        set { deviation_per_meter = value; }
    }

    virtual public float MaxTraceDistance 
    {
        get { return max_distance;  }
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

        Physics.Raycast(start_position, normal_aim_direction, out hit, MaxTraceDistance);

        if (hit.collider == null)
        {
            //实际上不可能，除非是向天上乱开枪，那样只要没有弹线，也无所谓啦，理论可以返回Zero，但是实际上不可能返回Zero的。
            //这种情况，直接把原来目标返回去拉倒
            return start_position + normal_aim_direction * MaxTraceDistance;
        }
        else
        {
            Vector3 position = hit.point;

            float distance = (hit.point - start_position).magnitude;
            float accuracy = distance * DeviationPerUnit;
            return position + GetRandomPointInSphere(accuracy);
        }
    }

    protected Vector3 GetFinalTracePositionByTargetPosition(Vector3 start_position, Vector3 target_position)
    {
        return GetFinalTracePositionByAimDirection(start_position, target_position - start_position);
    }

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
    public  static Vector3 GetRandomPointInSphere( float r ) 
    {
        Vector3 shift = Vector3.zero;

        float theta = Random.Range( 0, Mathf.PI * 2 );
        shift.x = r * Mathf.Cos(theta) * Random.Range(-1.0f,1.0f);
        shift.y = r * Mathf.Sin(theta) * Random.Range(-1.0f,1.0f);

        float z_max = Mathf.Sqrt(r * r - new Vector2(shift.x, shift.y).sqrMagnitude);
        shift.z = r * Random.Range(-z_max, z_max);

        return shift;
    }

    /// <summary>
    /// Double Raycast know the hit point center, but calculate deviation by test raycast hitpoint.
    /// 
    /// This is used for tatical game. not shoot game
    /// 
    /// </summary>
    /// <param name="start_position"></param>
    /// <param name="target_position"></param>
    /// <returns></returns>
    public RaycastHit DeviationRaycastOnHitPoint(Vector3 start_position, Vector3 target_position) 
    {
        Vector3 real_hit_position = GetFinalTracePositionByTargetPosition(start_position, target_position);
        RaycastHit hit;
        Physics.Raycast(start_position, (real_hit_position - start_position).normalized, out hit, MaxTraceDistance);

        return hit;
    }

    /// <summary>
    /// Double Raycast Only know the cannon direciton is enough.
    /// 
    /// This is use for SHOOT game.
    /// 
    /// </summary>
    /// <param name="start_position"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public RaycastHit DeviationRaycastOnAimHit(Vector3 start_position, Vector3 direction) 
    {
        Vector3 real_hit_position = GetFinalTracePositionByAimDirection(start_position, direction);
        RaycastHit hit;
        Physics.Raycast(start_position, (real_hit_position - start_position).normalized, out hit, MaxTraceDistance);

        return hit;
    }

    /// <summary>
    /// Single RayCast Need Know Where the absoluate hit point is.
    /// 
    /// This is used for tatical game. not shoot game
    /// 
    /// </summary>
    /// <param name="start_position"></param>
    /// <param name="target_position"></param>
    /// <returns></returns>
    public RaycastHit DeviationRaycastOnTargetCenter(Vector3 start_position, Vector3 target_position) 
    {
        Vector3 real_hit_position = GetRandomPointInSphere(DeviationPerUnit * (target_position - start_position).magnitude);
        RaycastHit hit;
        Physics.Raycast(start_position, (real_hit_position - start_position).normalized, out hit, MaxTraceDistance);
       
        return hit;
    }
}
