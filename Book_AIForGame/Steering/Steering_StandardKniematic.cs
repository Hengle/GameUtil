using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{

    public class SteeringUpdater_StandardKniematic : iSteeringUpdate
    {
        public void Update(SteeringAgent agent, SteeringOutput steering, float delta_time)
        {
            agent.position += agent.velocity * delta_time + 0.5f * steering.linearAccerlation * delta_time * delta_time;
            agent.angular += agent.angular * delta_time + 0.5f * steering.angularAccerlation * delta_time * delta_time;
            agent.velocity += steering.linearAccerlation * delta_time;
            agent.angular += steering.angularAccerlation * delta_time;
        }
    }

}
