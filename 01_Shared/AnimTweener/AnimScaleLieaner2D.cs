using UnityEngine;
using System.Collections;

namespace GameUtil 
{

    public class AnimScaleLieaner2D : AnimTweener
    {
        public Vector2 from;
        public Vector2 to;

        protected override void OnTick(float percent)
        {
            mTrans.localScale = Vector2.Lerp(from, to, percent);
        }
    }
}
