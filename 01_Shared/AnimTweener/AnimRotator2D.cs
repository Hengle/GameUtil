using UnityEngine;
using System.Collections;

namespace GameUtil
{
    public class AnimRotator2D : AnimTweener
    {
        public bool isCCW = false;

        public float Value 
        {
            get 
            {
                return mTrans.localEulerAngles.z;
            }
            set 
            {
                Vector3 euler = mTrans.localEulerAngles;
                euler.z = value;
                mTrans.localEulerAngles = euler;
            }
        }

        protected override void OnTick(float percent)
        {
            Value = LerpAngle(isCCW, percent);
        }
    }

}

