using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    public class TimeBasedAction
    {
        private float next_action_time = 0;

        public void InitAction(float next_interval, System.Action on_time_up, System.Func<bool> tick_condition = null)
        {
            RefreshNextActionTime(next_interval);
            OnTimeUp = on_time_up;
            TickCondition = tick_condition;
        }

        public void Tick(float delta_time, float next_interval)
        {
            if (TickCondition == null || (TickCondition != null && TickCondition() == true))
            {
                if (Time.timeSinceLevelLoad >= next_action_time)
                {
                    RefreshNextActionTime(next_interval);
                    OnTimeUp();
                }
            }
        }

        public void RefreshNextActionTime(float next_interval) 
        {
            next_action_time = Time.timeSinceLevelLoad + next_interval;
        }

        public System.Action OnTimeUp;
        public System.Func<bool> TickCondition;
    }

}

