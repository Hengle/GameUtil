using UnityEngine;
using System.Collections;

namespace GameUtil 
{
    public enum TimerType
    {
        Forward,
        Reverse,
        Loop,
        PingPong,
    }

    public class NeoTimer
    {
        public static uint ServerTime
        {
            get
            {
                long time_t;
                System.DateTime dt1 = new System.DateTime(1970, 1, 1, 0, 0, 0);
                System.TimeSpan ts = System.DateTime.Now - dt1;
                time_t = ts.Ticks / 10000000 - 28800;
                uint u_time = (uint)time_t;
                return u_time;
            }
        }

        public static uint UtcServerTime
        {
            get
            {
                long time_t;
                System.DateTime dt1 = new System.DateTime(1970, 1, 1, 0, 0, 0);
                System.TimeSpan ts = System.DateTime.UtcNow - dt1;
                time_t = ts.Ticks / 10000000 - 28800;
                uint u_time = (uint)time_t;
                return u_time;
            }
        }

        public delegate void voidDelegate();
        public delegate void tickDelegate(float percent);

        public voidDelegate onFinish;
        public voidDelegate onLoopOneRound;
        public voidDelegate onPingPongOneRound;
        public tickDelegate onTick;

        bool ticking = false;                       
        bool isReverse = false;                    
        TimerType timertype = TimerType.Forward;
        float timer;                               
        float length;                                         

        protected float percentage
        {
            get
            {
                if (length <= 0.00001f)
                {
                    return 1;
                }

                if (isReverse)
                {
                    return 1 - Mathf.Min(1.0f, timer / length);
                }
                else
                {
                    return Mathf.Min(1.0f, timer / length);
                }
            }
        }
        public bool paused
        {
            get
            {
                return !ticking;
            }
            set
            {
                ticking = !value;
            }
        }

        public void Tick(float delta_time)
        {
            if (ticking)
            {
                timer += delta_time;
                if (onTick != null)
                {
                    if (timertype == TimerType.Reverse) 
                    {
                        isReverse = true;
                    }
                    onTick(percentage);
                }

                if (timer >= length)
                {
                    switch (timertype)
                    {
                        case TimerType.Forward:
                        case TimerType.Reverse:
                            ticking = false;
                            timer = 0;
                            length = 0;
                            if (onFinish != null)
                            {
                                onFinish();
                            }
                            break;
                        case TimerType.Loop:
                            timer -= length;
                            if (onLoopOneRound != null) 
                            {
                                onLoopOneRound();
                            }
                            break;
                        case TimerType.PingPong:
                            timer -= length;
                            isReverse = !isReverse;
                            if (onPingPongOneRound != null) 
                            {
                                onPingPongOneRound();
                            }
                            break;
                    }


                }
            }
        }

        public void BeginTimer(float duration, TimerType type )
        {
            timer = 0;
            length = duration;
            ticking = true;
            timertype = type;
        }
    }

}
