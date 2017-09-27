using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    /// <summary>
    /// 对AI4Game这本书里面提到的Kinematic结构体，针对Unity的GameObject进行包装。
    /// </summary>
    public class SteeringAgent : MonoEx
    {
        #region FieldWarpper

        public Vector3 position
        {
            get
            {
                return cachedTransform.position;
            }
            set
            {
                cachedTransform.position = value;
            }
        }

        public float orientation
        {
            get
            {
                return cachedTransform.eulerAngles.y;
            }
            set
            {
                cachedTransform.rotation = Quaternion.Euler(0, value, 0);
            }
        }

        public Vector3 velocity
        {
            get
            {
                return cachedRigidbody.velocity;
            }
            set
            {
                cachedRigidbody.velocity = value;
            }
        }

        Vector3 angular_speed_holder;
        public float angular
        {
            get
            {
                return cachedRigidbody.angularVelocity.y;
            }
            set
            {
                angular_speed_holder.y = value;
                cachedRigidbody.angularVelocity = angular_speed_holder;
            }
        }
                
        public float max_speed = 4.0f;
        #endregion

        public SteeringOutput steering;
        public iSteeringUpdate steering_updater;
        public string updater_class = "SteeringUpdater_StandardKniematic";
        protected string updateClass
        {
            get
            {
                return "GameUtil.AI." + updater_class;
            }
        }

        void OnEnable()
        {
            steering_updater = (iSteeringUpdate)System.Activator.CreateInstance(System.Type.GetType(updateClass));
            if (steering_updater == null)
            {
                Debug.Log("steering updader class not found "+updateClass);
            }
        }

        public void ClipSpeed()
        {
            if (velocity.magnitude > max_speed)
            {
                velocity = velocity.normalized * max_speed;
            }
        }

        void Update()
        {
            steering_updater.Update(this, steering, Time.deltaTime);
        }

    }
}

