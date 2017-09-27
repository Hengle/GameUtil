using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public class SteeringFaceTarget : SteeringAlign
    {
        protected override float CalculateOrientation()
        {
            Vector3 direction = target.position - character.position;
            float ideal_orientation = Mathf.Atan2(-direction.x, direction.z);
            return ideal_orientation;
        }
    }

}
