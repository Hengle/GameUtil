using UnityEngine;
using System.Collections;

namespace GameUtil
{
    [RequireComponent(typeof(MeshRenderer))]
    public class AnimColorMaterial : AnimTweener
    {
        public Color from;
        public Color to;

        protected override void OnTick(float percent)
        {
            renderer3D.material.color = Color.Lerp(from, to, percent);
        }
    }

}
