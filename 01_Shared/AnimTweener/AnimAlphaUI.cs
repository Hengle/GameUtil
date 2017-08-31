using UnityEngine;
using System.Collections;

namespace GameUtil
{
    [RequireComponent(typeof(UnityEngine.UI.Graphic))]
    public class AnimAlphaUIText : AnimTweener
    {
        [Range(0, 1)]
        public float from;
        [Range(0, 1)]
        public float to;

        public float Value 
        {
            get 
            {
                return  uiGraphic.color.a;
            }
            set 
            {
                Color cc = uiGraphic.color;
                cc.a = value;
                uiGraphic.color = cc;
            }
        }

        protected override void OnTick(float percent)
        {
            Value = LerpFloat(from, to, percent);
        }        
    }
}


