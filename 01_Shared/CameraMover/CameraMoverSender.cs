using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{
    /// <summary>
    /// 适合简单的2DCamera同步，或者3D锁视角的摄影机同步，不适合复杂的摄影机控制。
    /// 复杂FPS，TPS，MMO，ACT游戏不要用这个脚本，使用第三方插件完成行为。
    /// </summary>
    public class CameraMoverSender : MonoBehaviour
    {
        public CameraMoverReceiver receiver;

        public EUpdateType update_type = EUpdateType.Update;
        public enum EPlayerCameraSynType
        {
            DirectSyn,
            LerpSyn,
        }
        public EPlayerCameraSynType camera_syn_type = EPlayerCameraSynType.DirectSyn;

        public enum EPlayerCameraSynContentType 
        {
            PositionOnly,
            PositionAndAxis_Y,
            PositionAndAxis_Z,
        }
        public EPlayerCameraSynContentType content_type = EPlayerCameraSynContentType.PositionOnly;

        bool inited = false;
        Transform _trans;
        [SerializeField]
        int lerp_inverval = 1;
        int frame_count = 0;

        void OnEnable() 
        {
            receiver.Init(camera_syn_type, content_type);
            inited = true;
            _trans = transform;
        }

        void SynCamera() 
        {
            if (inited == false) return;
            
            if (camera_syn_type == EPlayerCameraSynType.DirectSyn)
            {
                DirectSynCamera();
            }
            else 
            {
                if (frame_count % lerp_inverval == 0) 
                {
                    LerpSynCamera();
                }                
            }

            frame_count++;
        }

        void DirectSynCamera()
        {
            receiver.DirectFollow(_trans.position, _trans.eulerAngles);
        }

        void LerpSynCamera() 
        {
             receiver.SetLerpTarget(_trans.position, _trans.eulerAngles);
        }

        // Update is called once per frame
        void Update()
        {
            if (inited && update_type == EUpdateType.Update) 
            {
                SynCamera();
            }
        }

        void LateUpdate() 
        {
            if (inited && update_type == EUpdateType.LateUpdate) 
            {
                SynCamera();
            }
        }

        void FixedUpdate()
        {
            if (inited && update_type == EUpdateType.FixedUpdate) 
            {
                SynCamera();
            }
        }
    }
}
