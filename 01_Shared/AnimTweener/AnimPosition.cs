using UnityEngine;
using System.Collections;

namespace GameUtil
{
    public class AnimPosition : AnimTweener
    {
        public Vector3 from;
        public Vector3 to;
        public bool isLocal = true;
        public bool useAdditivePosition = false;

        protected override void OnPostStart()
        {
            if (useAdditivePosition)
            {
                if (isLocal)
                {
                    from += mTrans.localPosition;
                    to += mTrans.localPosition;
                }
                else 
                {
                    from += mTrans.position;
                    to += mTrans.position;
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
