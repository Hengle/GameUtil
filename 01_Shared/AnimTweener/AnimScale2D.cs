using UnityEngine;
using System.Collections;

namespace GameUtil
{
    public class AnimScale2D : AnimTweener
    {
        public AnimationCurve scaleCurve;

        public Vector2 Value
        {
            get 
            {
                return (Vector2)mTrans.localScale;
            }
            set 
            {
                mTrans.localScale = (Vector3)value;
            }
        }

        protected override void OnTick(float percent)
        {
            float val = scaleCurve.Evaluate(percent);
            Value = new Vector2(val, val);
        }
    }
}

