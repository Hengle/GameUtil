using UnityEngine;
using System.Collections;

namespace GameUtil
{
    public class AnimRotator : AnimTweener
    {
        public bool isCCW = false;
        public EAxis rotateAxis = EAxis.Z;

        public float Value 
        {
            get 
            {
                switch (rotateAxis) 
                {
                    case EAxis.X:
                        return mTrans.eulerAngles.x;
                    case EAxis.Y:
                        return mTrans.eulerAngles.y;
                    case EAxis.Z:
                    default:
                        return mTrans.eulerAngles.z;
                }
            }
            set 
            {
                Vector3 euler_angle = mTrans.eulerAngles;
                switch (rotateAxis) 
                {
                    case EAxis.X:
                        euler_angle.x = value;
                        break;
                    case EAxis.Y:
                        euler_angle.y = value;
                        break;
                    case EAxis.Z:
                        euler_angle.z = value;
                        break;
                }

                mTrans.eulerAngles = euler_angle;
            }
        }


        protected override void OnTick(float percent)
        {
            Value = LerpAngle(isCCW, percent);
        }
    }
}

