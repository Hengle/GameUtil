﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameUtil.AI.Steering
{
    public struct KinematicData
    {
        public Vector3 velocity;
        //注意，结构体里面的Rotation
        public Vector3 angularVelocity;
        public Vector3 position;
        public Vector3 orientation;

        public float orientataionY
        {
            get
            {
                return orientation.y;
            }
            set
            {
                orientation.y = value;
            }
        }

        /// <summary>
        /// ReadOnly
        /// </summary>
        public float orientationYinRad
        {
            get
            {
                return orientation.y * Mathf.Deg2Rad;
            }
        }

        public float angularVelocityY
        {
            get
            {
                return angularVelocity.y;
            }
            set
            {
                angularVelocity.y = value;
            }
        }
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

