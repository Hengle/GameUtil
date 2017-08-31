using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GameUtil
{  

    public class AnimMovePath : AnimTweener
    {
        public voidDelegate onFinishAnim;

        public List<Vector3> wayPoints = new List<Vector3>();
        public bool isLocal = true;
        public bool useAdditivePosition = true;

        private List<float> time_interivals = new List<float>();
        private int patrol_index = 0;

        protected Vector3 from 
        {
            get 
            {
                return wayPoints[patrol_index];
            }
        }
        protected Vector3 to
        {
            get
            {
                return wayPoints[patrol_index+1];
            }
        }

        /// <summary>
        /// 暂时不支持循环寻路，只能一次走到黑！
        /// </summary>
        protected override void OnFinishDelay()
        {
            
            if (loopType == TimerType.Reverse || loopType == TimerType.PingPong) 
            {
                Debug.LogError("路径运动不支持反向和乒乓运动！");
                return;
            }

            if (wayPoints.Count >= 2)
            {
                if (useAdditivePosition)
                {
                    for (int i = 0; i < wayPoints.Count; i++)
                    {
                        wayPoints[i] = wayPoints[i] + (isLocal ? mTrans.localPosition : mTrans.position);
                    }
                }

                time_interivals.Clear();
                float sum_distance = 0;

                for (int i = 0; i < wayPoints.Count - 1; i++)
                {
                    float dist = Vector3.Distance(wayPoints[i], wayPoints[i + 1]);
                    time_interivals.Add(dist);
                    sum_distance += dist;
                }

                for (int i = 0; i < time_interivals.Count; i++)
                {
                    time_interivals[i] = length * time_interivals[i] / sum_distance;
                }
                
                timer.onTick = OnTick;
                timer.onFinish = OnFinish;
                patrol_index = 0;
                timer.BeginTimer(time_interivals[patrol_index], TimerType.Forward);
            }
            else 
            {
                Debug.LogError("错误：路点的数量小于2");
            }
        }

        protected override void OnFinish()
        {
            if (patrol_index + 1 < time_interivals.Count)
            {
                patrol_index++;
                timer.BeginTimer(time_interivals[patrol_index], TimerType.Forward);
            }
            else 
            {
                if (loopType == TimerType.Loop)
                {
                    patrol_index = 0;
                    timer.BeginTimer(time_interivals[patrol_index], TimerType.Forward);
                }
                else 
                {
                    if (onFinishAnim != null) 
                    {
                        onFinishAnim();
                    }
                }
            }
        }

        protected override void OnTick(float percent)
        {
            if (isLocal)
            {
                mTrans.localPosition = Vector3.Lerp(from, to, percent);
            }
            else 
            {
                mTrans.position = Vector3.Lerp(from, to, percent);
            }
        }
    }

}
