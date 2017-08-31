using UnityEngine;
using System.Collections;

namespace GameUtil 
{
    public class AnimPositionUIWithBumper : AnimPositionUI
    {
        public Vector2 bumpDistance;

        protected override void OnFinish()
        {
            EffectManager.Instance.RemoveFromBumpQueue(this);
        }

        protected override void OnPostStart()
        {
            EffectManager.Instance.AddToBumpQueue(this);
        }

        public override void OnQueueBumped()
        {
            saved_pos += bumpDistance;
        }
    }
}


