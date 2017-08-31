using UnityEngine;
using System.Collections;
namespace GameUtil
{

    public class AnimMovePathUI : AnimMovePath
    {
        protected override void OnTick(float percent)
        {
            uiGraphic.rectTransform.anchoredPosition = Vector2.Lerp(from, to, percent);
        }
    }
}