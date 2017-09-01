using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameUtil
{

    
    public abstract class AnimTweener : MonoBehaviour
    {
        protected Renderer renderer3D;
        protected Graphic uiGraphic;
        protected SpriteRenderer spriteRenderer;
        protected Transform mTrans;
        protected RectTransform rectTrans;
        
        
        public TimerType loopType;              
        public bool runAtStart = false;
        public bool autoDestroyAtFinish = false;
        public float length;                  
        public float delay;

        public delegate void voidDelegate();
        public voidDelegate onAnimFinished;
        
        abstract protected void OnTick(float percent);
        virtual protected void OnFinish() 
        {
            if (autoDestroyAtFinish) 
            {
                Destroy(this.gameObject);
            }

            if (onAnimFinished != null)
            {
                onAnimFinished();
            }
        }
        virtual protected void OnPostStart() { }

        void OnEnable()
        {
            renderer3D = GetComponent<Renderer>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            uiGraphic = GetComponent<Graphic>();
            mTrans = transform;
            rectTrans = transform as RectTransform;
        }

        /// <summary>
        /// 计时器，标配
        /// </summary>
        protected NeoTimer timer = new NeoTimer();

        public bool paused
        {
            get
            {
                return timer.paused;
            }
            set 
            {
                timer.paused = value;
            }
        }

        void Start() 
        {
            if ( runAtStart ) 
            {
                InitAndPlay();
            }
        }

        public void InitAndPlay() 
        {
            //不使用Invoke，直接用一个计时器实现Delay
            if (delay > 0.000001f)
            {
                timer.onFinish = OnFinishDelay;
                timer.onTick = null;
                timer.BeginTimer(delay, TimerType.Forward);
            }
            else
            {
                OnFinishDelay();
            }
            OnPostStart();
        }

        virtual protected void OnFinishDelay() 
        {
            timer.onTick = OnTick;
            timer.onFinish = OnFinish;
            timer.BeginTimer(length, loopType);
        }

        void Update() 
        {
            timer.Tick( Time.deltaTime );
        }

        public float LerpFloat(float from, float to, float lerp)
        {
            return from + (to - from) * lerp;
        }

        public float LerpAngle(bool ccw, float lerp) 
        {
            return 360 * lerp * (ccw ? 1 : -1);
        }

        virtual public void OnQueueBumped() 
        {

        }
    }
}


