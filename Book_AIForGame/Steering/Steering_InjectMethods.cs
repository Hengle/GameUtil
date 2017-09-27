using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    public static class Steering_InjectMethods 
    {
        public static void CreateKinematicData(this MonoEx monoex,out KinematicData data)
        {            
            data.velocity = monoex.cachedRigidbody.velocity;
            data.angularVelocity = monoex.cachedRigidbody.angularVelocity;

            data.position = monoex.cachedTransform.position;
            data.orientation = monoex.cachedTransform.eulerAngles;
        }

        public static void ApplyKinematicVelocity(this MonoEx monoex, ref KinematicData kinimatic_data)
        {
            monoex.cachedRigidbody.velocity = kinimatic_data.velocity;
            monoex.cachedRigidbody.angularVelocity = kinimatic_data.angularVelocity;
        }

        public static void ApplyPositionAndRotation(this MonoEx monoex, ref KinematicData kinematic_data)
        {
            monoex.cachedTransform.position = kinematic_data.position;
            monoex.cachedTransform.rotation = Quaternion.Euler(kinematic_data.orientation);
        }

        /// <summary>
        /// this will has very low performance，batter to use MonoEx extension
        /// </summary>
        /// <param name="gobj"></param>
        /// <param name="data"></param>
        public static void CreateKinematicData(this GameObject gobj, out KinematicData data)
        {
            Rigidbody rb = gobj.GetComponent<Rigidbody>();
            Transform trans = gobj.transform;

            data.velocity = rb.velocity;
            data.angularVelocity = rb.angularVelocity;

            data.position = trans.position;
            data.orientation = trans.eulerAngles;
        }

        /// <summary>
        /// this will has very low performance，batter to use MonoEx extension
        /// </summary>
        /// <param name="gobj"></param>
        /// <param name="data"></param>
        public static void ApplyKinematicVelocity(this GameObject gobj, ref KinematicData kinimatic_data)
        {
            Rigidbody rb = gobj.GetComponent<Rigidbody>();
            rb.velocity = kinimatic_data.velocity;
            rb.angularVelocity = kinimatic_data.angularVelocity;
        }

        /// <summary>
        /// this will has very low performance，batter to use MonoEx extension
        /// </summary>
        /// <param name="gobj"></param>
        /// <param name="data"></param>
        public static void ApplyPositionAndRotation(this GameObject gobj, ref KinematicData kinematic_data)
        {
            Transform trans = gobj.transform;
            trans.position = kinematic_data.position;
            trans.rotation = Quaternion.Euler(kinematic_data.orientation);
        }
    }
}
