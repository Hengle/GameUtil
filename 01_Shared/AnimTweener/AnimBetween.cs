using UnityEngine;
using System.Collections;

namespace GameUtil
{
    /// <summary>
    /// 两点间巡逻，如果起始点不是0，0那么先过渡到第一个点。
    /// </summary>
    public class AnimBetween : AnimMovePath
    {
        public Vector3 MinBound;
        public Vector3 MaxBound;

        protected override void OnFinishDelay()
        {
            loopType = TimerType.Loop;

            wayPoints.Clear();
            wayPoints.Add(Vector3.zero);
            wayPoints.Add(MaxBound);
            wayPoints.Add(MinBound);
            wayPoints.Add(Vector3.zero);

            base.OnFinishDelay();
        }
    }

}
