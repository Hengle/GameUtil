using UnityEngine;
using System.Collections;

namespace GameUtil
{
    [RequireComponent(typeof(MeshRenderer))]
    public class AnimAlphaMaterial : AnimTweener
    {
        [Range(0, 1)]
        public float from;
        [Range(0, 1)]
        public float to;

        protected float Value 
        {
            get 
            {
                return renderer3D.material.color.a;
            }
            set 
            {
                Color cc = renderer3D.material.color;
                cc.a = value;
                renderer3D.material.color = cc;
            }
        }

        protected override void OnTick(float percent)
        {
            Value = LerpFloat(from, to, percent);
        }
    }
}
