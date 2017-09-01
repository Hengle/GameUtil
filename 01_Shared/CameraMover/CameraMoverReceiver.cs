using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{

    public class CameraMoverReceiver : MonoBehaviour
    {
        public EUpdateType lerp_update_type = EUpdateType.Update;
        private CameraMoverSender.EPlayerCameraSynType syn_type = CameraMoverSender.EPlayerCameraSynType.DirectSyn;
        private CameraMoverSender.EPlayerCameraSynContentType content_type = CameraMoverSender.EPlayerCameraSynContentType.PositionOnly;

        Vector3 target_position;
        Vector3 target_angle;
        Transform _trans;
                
        public float position_follow_speed;
        public float angle_follow_speed;

        public void SetLerpTarget(Vector3 pos, Vector3 angle) 
        {
            target_position = pos;
            target_angle = angle;
        }

        public void Init(CameraMoverSender.EPlayerCameraSynType syn_type, CameraMoverSender.EPlayerCameraSynContentType content_type) 
        {
            this.syn_type = syn_type;
            this.content_type = content_type;
            this._trans = this.transform;
        }

        void Lerp(float delta_time ) 
        {
            if (syn_type == CameraMoverSender.EPlayerCameraSynType.LerpSyn)
            {
                _trans.position = Vector3.Lerp(_trans.position, target_position, delta_time * position_follow_speed);
                
                Vector3 euler = _trans.eulerAngles;

                if( content_type == CameraMoverSender.EPlayerCameraSynContentType.PositionAndAxis_Y )
                {
                    euler.y = Mathf.Lerp(euler.y, target_angle.y, delta_time * angle_follow_speed);
                    _trans.eulerAngles = euler;
                }

                if(content_type == CameraMoverSender.EPlayerCameraSynContentType.PositionAndAxis_Z )
                {
                    euler.z = Mathf.Lerp(euler.z, target_angle.z, delta_time * angle_follow_speed);
                    _trans.eulerAngles = euler;
                }
            }
        }

        public void DirectFollow(Vector3 position, Vector3 angle)
        {
            _trans.position = position;
            Vector3 euler = _trans.eulerAngles;

            if (content_type == CameraMoverSender.EPlayerCameraSynContentType.PositionAndAxis_Y)
            {
                euler.y = angle.y;                
            }

            if (content_type == CameraMoverSender.EPlayerCameraSynContentType.PositionAndAxis_Z)
            {
                euler.z = angle.z;
            }

            _trans.eulerAngles = euler;
        }

        // Update is called once per frame
        void Update()
        {
            if (lerp_update_type == EUpdateType.Update) 
            {
                Lerp(Time.deltaTime);
            }
        }

        void LateUpdate() 
        {
            if (lerp_update_type == EUpdateType.LateUpdate)
            {
                Lerp(Time.deltaTime);
            }
        }

        void FixedUpdate() 
        {
            if (lerp_update_type == EUpdateType.FixedUpdate)
            {
                Lerp(Time.fixedDeltaTime);
            }
        }
    }

}
