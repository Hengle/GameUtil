using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameUtil.AI.Steering
{
    public interface iSteeringUpdate
    {
        void Update(SteeringAgent agent, SteeringOutput steering, float delta_time);
    }

    public interface iOutputSteering<T>where T: struct
    {
        bool GetSteering(out T output);
    }
}