using UnityEngine;
using System.Collections;

namespace GameUtil
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimAlphaSprite2D : AnimTweener
    {
        [Range(0,1)]
        public float from;
        [Range(0, 1)]
        public float to;
        
        public float Value 
        {
            get 
            {
                return spriteRenderer.color.a;
            }
            set 
            {
                Color color = spriteRenderer.color;
                color.a = value;
                spriteRenderer.color = color;
            }
        }
        
        protected override void OnTick(float percent)
        {
            Value = LerpFloat(from, to, percent);
        }
    }
}


