using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;
using GameUtil.OpenWorld;

namespace GameUtil.Examples 
{


    [System.Serializable]
    public class BuildingObjectData : WorldSaveObjectData 
    {
        public BuildingObjectData(BuildingObject bo) 
        {
            bo.SaveBasicInfo(this);
        }
    }

    public class BuildingObject : InteractObject
    {
        Collider main_collider;
        BoxCollider[] bounds;
        Renderer mesh_renderer;
        int snaped_slot_count;

        EPlaceableObjectState current_state = EPlaceableObjectState.Normal;

        public EPlaceableObjectState BuildingBlockState
        {
            get 
            {
                return current_state;
            }
        }

        public BoxCollider[] BoundingBoxes 
        {
            get 
            {
                return bounds;
            }
        }
        
        bool EnableCollider
        {
            set
            {
                main_collider.enabled = value;                
            }
        }

        public int SnapCount 
        {
            get 
            {
                return snaped_slot_count;
            }
        }
        
        void OnEnable()
        {
            main_collider = GetComponent<Collider>();
            //临时代码，用maya做的模型不会有这个问题！
            if (main_collider.GetType() == typeof(MeshCollider)) 
            {
                MeshCollider mc = main_collider as MeshCollider;
                mc.sharedMesh = GetComponent<MeshFilter>().sharedMesh;

                Transform bounding_colliders = transform.FindChild("BoundingColliders");
                if (bounding_colliders != null) 
                {
                    bounds = bounding_colliders.GetComponentsInChildren<BoxCollider>();
                    Debug.Log("Bounds count " + bounds.Length);
                }         
            }
            mesh_renderer = GetComponent<Renderer>();

            ///初始化的时候，要给加上引用
            BuildingSnapSlot[] slots = GetComponentsInChildren<BuildingSnapSlot>();
            for (int i = 0; i < slots.Length; i++) 
            {
                slots[i].building_owner = this;
            }
        }

        Vector3 old_snap_pos;
        public void SnapToSlot(BuildingSnapSlot source_slot, BuildingSnapSlot target_slot) 
        {
            if (snaped_slot_count == 0)
            {
                //暂时放弃控制权。
                transform.SetParent(GameFacade.Instance.GetWorldManager().building_root);

                BuildingObject target_bo = target_slot.building_owner;
                
                //rotation
                float y = target_bo.transform.eulerAngles.y;
                float this_y = transform.eulerAngles.y;
                float delta_y = y - this_y;
                while (delta_y > 90) { delta_y -= 90; }
                while (delta_y < -90) { delta_y += 90; }

                Rotate(delta_y);

                //position
                Vector3 delta = target_slot.transform.position - source_slot.transform.position;
                transform.position += delta;
                transform.localScale = Vector3.one;

                old_snap_pos = detector_transform.position;
            }

            snaped_slot_count++;
            Debug.Log("Current Snaped Count is " + snaped_slot_count);

        }
        
        public void ResetSnap() 
        {
            Debug.Log("ResetSnap");
            transform.SetParent(detector_transform);
            this.transform.localPosition = Vector3.zero;
            this.transform.localEulerAngles = Vector3.zero;
            snaped_slot_count = 0;
        }

        public void SwitchDisplayState(EPlaceableObjectState state, BuildingBlockConfig config_file)
        {
            LocalPlayer lp = GameFacade.Instance.GetLocalPlayer();
                
            if (state != current_state)
            {
                current_state = state;

                switch (state)
                {
                    case EPlaceableObjectState.Normal:
                        mesh_renderer.material = config_file.standard;
                        lp.operation_state = EOperationState.Default;
                        EnableCollider = true;
                        //无论如何这里应该把Snaping状态设置为False
                        snaped_slot_count = 0;
                        break;
                    case EPlaceableObjectState.Dropable:
                        mesh_renderer.material = config_file.enable;
                        lp.operation_state = EOperationState.Building_Dropable;
                        EnableCollider = false;
                        break;
                    case EPlaceableObjectState.Conflict:
                        mesh_renderer.material = config_file.confict;
                        lp.operation_state = EOperationState.Building_Confict;
                        EnableCollider = false;
                        break;
                }
            }
        }

        public override WorldSaveObjectData Save()
        {
            return new BuildingObjectData(this);
        }

        public override void Load(WorldSaveObjectData data)
        {

        }

        public override void OnInteracted(LocalPlayer toucher)
        {
            
        }

        public override string HintString
        {
            get
            {
                return "GUID:" + GUID.ToString();
            }
        }

        public void Rotate( float val ) 
        {
            if (current_state != EPlaceableObjectState.Normal)
            {
                Vector3 angle = transform.eulerAngles;
                angle.y += val;
                transform.eulerAngles = angle;
            }
        }

        Transform p_detector_transform;

        protected Transform detector_transform
        {
            get 
            {
                if( p_detector_transform == null )
                {
                    p_detector_transform = GameFacade.Instance.GetLocalPlayer().building_block_params.placing_building_detector.transform;
                }
                return p_detector_transform;
            }
        }

        void Update() 
        {
            if (current_state != EPlaceableObjectState.Normal) 
            {
                Vector3 angle = transform.eulerAngles;
                angle.x = 0;
                angle.z = 0;
                transform.eulerAngles = angle;

                if ( snaped_slot_count > 0 && (old_snap_pos - detector_transform.position).magnitude > 1.5f )
                {
                    ResetSnap();
                }
            }
        }


    }

}
