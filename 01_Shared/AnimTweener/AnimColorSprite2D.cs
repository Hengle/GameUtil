using UnityEngine;
using System.Collections;

namespace GameUtil
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimColorSprite2D :AnimTweener
    {
        public Color from;
        public Color to;
        
        protected override void OnTick(float percent)
        {
            spriteRenderer.color = Color.Lerp(from, to, percent);
        }        
    }
}


