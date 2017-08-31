using UnityEngine;
using System.Collections;

namespace GameUtil 
{
    public class AnimPositionUI : AnimTweener
    {
        public bool use_abs_axis = false;
       
        public Vector2 from;
        public Vector2 to;
        protected Vector2 saved_pos;
        protected override void OnPostStart()
        {
            saved_pos = rectTrans.anchoredPosition;
        }

        protected override void OnTick(float percent)
        {
            if (use_abs_axis == false)
            {
                Vector2 pos = Vector2.Lerp(from, to, percent);
                rectTrans.anchoredPosition = saved_pos + pos;
            }
            else 
            {
                rectTrans.anchoredPosition = Vector2.Lerp(from, to, percent);
            }
            
        }
    }

}

