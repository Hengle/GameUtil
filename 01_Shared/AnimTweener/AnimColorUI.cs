using UnityEngine;
using System.Collections;

namespace GameUtil
{
    [RequireComponent(typeof(UnityEngine.UI.Graphic))]
    public class AnimColorUI : AnimTweener
    {
        public Color from;
        public Color to;

        protected override void OnTick(float percent)
        {
            uiGraphic.color = Color.Lerp(from, to, percent);
        }
    }
}


