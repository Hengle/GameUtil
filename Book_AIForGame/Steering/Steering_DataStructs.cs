using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameUtil.AI.Steering
{
    public struct KinematicData
    {
        public Vector3 position;
        public float orientation;

        public Vector3 velocity;
        public float rotation;
    }

    /// <summary>
    /// Steering的每帧输出值。
    /// 线加速度和角加速度两个值。
    /// 另一个结构体，直接用Unity自身的Transform和RigidBody搞定。
    /// </summary>
    public struct SteeringOutput
    {
        public Vector3 linearAccerlation;
        public float angularAccerlation;
    }

    /// <summary>
    /// 这个是比较简单粗暴的方式，没有过度，直接扭过去。
    /// </summary>
    public struct KinematicSteeringOutput
    {
        public Vector3 velocity;
        public float rotation;
    }
    
}

