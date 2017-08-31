using UnityEngine;
using System.Collections;

namespace GameUtil
{
    public class AnimPosition2D : AnimTweener
    {
        public Vector2 from;
        public Vector2 to;
        public bool isLocal = true;
        public bool useAdditivePosition = true;

        protected override void OnPostStart()
        {
            if (useAdditivePosition)
            {

                if (isLocal)
                {
                    to = to + (Vector2)mTrans.localPosition;
                    from = from + (Vector2)mTrans.localPosition;
                }
                else 
                {
                    to = to + (Vector2)mTrans.position;
                    from = from + (Vector2)mTrans.position;
                }
            }
        }
         
        protected override void OnTick(float percent)
        {
            Vector2 pos = Vector2.Lerp(from, to, percent);
            if (isLocal)
            {
                mTrans.localPosition = (Vector3)pos;
            }
            else 
            {
                mTrans.position = (Vector3)pos;
            }
        }
    }
}
