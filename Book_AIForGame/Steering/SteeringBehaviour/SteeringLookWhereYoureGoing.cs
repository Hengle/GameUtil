using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public class SteeringLookWhereYoureGoing : SteeringAlign
    {
        float last_orientation;
        protected override float CalculateOrientation()
        {
            if (character.velocity.magnitude < 0.0001f )
            {
                return last_orientation;
            }

            last_orientation = Mathf.Atan2(-character.velocity.x, character.velocity.z);
            return last_orientation;
        }
    }
}
