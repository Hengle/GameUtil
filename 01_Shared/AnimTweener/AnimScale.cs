using UnityEngine;
using System.Collections;

namespace GameUtil 
{
    public class AnimScale : AnimTweener
    {
        public AnimationCurve scaleCurve;

        public float Value 
        {
            get 
            {
                return mTrans.localScale.x;
            }
            set 
            {
                mTrans.localScale = new Vector3(value, value, value);
            }
        }

        protected override void OnTick(float percent)
        {
            Value = scaleCurve.Evaluate(percent);
        }        
    }

}
