using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
namespace BehaviorDesigner.Runtime.Tasks 
{
    [TaskDescription("Support 360 degree scan and PingPong Scan with a FOV like angle less than 360 degree, based on xz plane.")]
    [TaskCategory("Movement")]
    [TaskIcon("Assets/GameUtil/PluginExtension/BehaviorDesigner/Icons/{SkinColor}ScanRotate.png")]
    public class ScanRotate : Action
    {
        public SharedBool full_angle;
        public bool CCW = true;
        public SharedFloat Fov;
        public SharedFloat rotate_speed;
        public float rotationEpsilon = 0.5f;
        public float maxLookAtRotationDelta = 1.0f;

        
        float y;
        int index = 0;
        const int C_KEY_FRAME_COUNT = 4;
        Quaternion[] target_candidates = new Quaternion[C_KEY_FRAME_COUNT];
        Quaternion target 
        {
            get 
            {
                return target_candidates[index % C_KEY_FRAME_COUNT];
            }
        }

        public override void OnStart()
        {
            if (Fov.Value >= 360) 
            {
                full_angle.Value = true;
            }

            target_candidates[0] = Quaternion.Euler(0, -Fov.Value / 2, 0);
            target_candidates[1] = Quaternion.Euler(0, 0, 0);
            target_candidates[2] = Quaternion.Euler(0, Fov.Value / 2, 0);
            target_candidates[3] = Quaternion.Euler(0, 0, 0);
            index = 0;
        }

        /// <summary>
        /// 这个状态，必须被强制打断
        /// </summary>
        /// <returns></returns>
        public override TaskStatus OnUpdate()
        {
            if (full_angle.Value == true)
            {
                y += (CCW ? 1 : -1) * Time.deltaTime * rotate_speed.Value;
                transform.localRotation = Quaternion.Euler(0, y, 0);
            }
            else 
            {
                if (Quaternion.Angle(transform.localRotation, target) < rotationEpsilon)
                {
                    index++;
                }
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, maxLookAtRotationDelta);
            }

            return TaskStatus.Running;
        }

        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (full_angle.Value ) 
            {
                Color old_color = UnityEditor.Handles.color;
                UnityEditor.Handles.color = new Color(1, 0, 0, 0.1f);
                UnityEditor.Handles.DrawSolidDisc( Owner.transform.position, Vector3.up, 2.0f);
                UnityEditor.Handles.color = old_color;
            }
#endif
        }
    }
}

