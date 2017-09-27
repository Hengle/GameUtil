using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{

    public static class SteerUtils
    {
        //为了方便处理，这里我把弧度转换成角度了。因为最后Rotation接受的是角度
        //这个函数以后会提取出去，目前还比较懵逼，不知道这应该归为哪一类……
        //看明白了，这个应该属于KinematicSeek
        public static float GetNewOrientation(float current_orientation, Vector3 velocity)
        {
            if (velocity.magnitude > 0)
            {
                return Mathf.Atan2(-velocity.x, velocity.z) * Mathf.Rad2Deg;
            }
            return current_orientation;
        }



        public static float MapToRange(float rotation)
        {
            float rot = rotation;

            while (rot < -Mathf.PI)
            {
                rot += 2 * Mathf.PI;
            }

            while (rot > Mathf.PI)
            {
                rot -= 2 * Mathf.PI;
            }

            return rot;
        }

        public static float RandomBinomial()
        {
            return Random.Range(0, 1) - Random.Range(0, 1);
        }

        public static Vector3 OrientationToVector3_XZ(float orientation)
        {
            return new Vector3(Mathf.Sin(orientation * Mathf.Deg2Rad), 0, Mathf.Cos(orientation * Mathf.Deg2Rad));
        }
    }
}