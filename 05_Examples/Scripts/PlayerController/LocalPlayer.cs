using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;
using GameUtil.UI;
using GameUtil.OpenWorld;

namespace GameUtil.Examples 
{
    public enum EOperationState 
    {
        Default,
        Building_Dropable,
        Building_Confict,
        Managing_Inventory,
    }

    public class LocalPlayer : MonoBehaviour
    {
        public System.Action<Inventory> OnSwiftInventoryUpdate;

        [HideInInspector]
        public Inventory inventory = new Inventory(50);

        [HideInInspector]
        public Inventory swift_inventory = new Inventory(9);
        private int active_swift_inventory_index = -1;

        List<ISwiftInventoryTakeOut> swift_inventory_takeout_handler = new List<ISwiftInventoryTakeOut>();
     
        //和放置建筑相关的参数和临时变量
        public BuildingBlockParams building_block_params;

        /// <summary>
        /// Dot Not Modify this directly!!!
        /// </summary>        
        private EOperationState p_operation_state;
        private PressStateMonitor p_action_monitor = new PressStateMonitor();
        private PressStateMonitor p_alter_monitor = new PressStateMonitor();
        private System.Action<float> p_scroll_wheel_handler;
        private Dictionary<EOperationState, IPressStateHandler> p_action_handler_index = new Dictionary<EOperationState,IPressStateHandler>();
        private Dictionary<EOperationState, IPressStateHandler> p_alter_handler_index = new Dictionary<EOperationState, IPressStateHandler>();
        private Dictionary<EOperationState, System.Action<float>> p_scroll_wheel_handler_index = new Dictionary<EOperationState, System.Action<float>>();

        /// <summary>
        /// 切换状态的同时，切换事件的处理类。全自动完成
        /// 
        /// 所以以后的操作，只要保证，状态切换对了，那么其他的，都自动OK。
        /// </summary>
        public EOperationState operation_state 
        {
            get 
            {
                return p_operation_state;
            }
            set 
            {
                p_operation_state = value;
                
                //action mouse button                
                p_action_handler_index.TryGetValue(value, out p_action_monitor.handler);
                
                //alter mouse button
                p_alter_handler_index.TryGetValue(value, out p_alter_monitor.handler);
                
                //wheel
                p_scroll_wheel_handler_index.TryGetValue(value, out p_scroll_wheel_handler);

                if (p_operation_state == EOperationState.Managing_Inventory) 
                {
                    PutAway();
                }

                Debug.Log("<color=red> Operation State is " + p_operation_state + "</color>");
            }
        }


        void OnEnable()
        {
            swift_inventory.GiveItem(1001, 25);
            swift_inventory.GiveItem(1002, 25);
            swift_inventory.GiveItem(1003, 25);
            swift_inventory.GiveItem(1004, 25);
            swift_inventory.GiveItem(1005, 25);
            swift_inventory.GiveItem(1006, 25);
            swift_inventory.GiveItem(1007, 25);
            
            swift_inventory_takeout_handler.Add(new TakeOut_BuildingObject());

            p_action_handler_index[EOperationState.Default] = new ActionButtonHandler_Default();
            p_action_handler_index[EOperationState.Building_Dropable] = new ActionButtonHandler_CanDropBuilding();

            p_alter_handler_index[EOperationState.Default] = new AlterButtonHandler_Default();
            p_alter_handler_index[EOperationState.Building_Dropable] = new AlterButtonHandler_PlacingBuilding();
            p_alter_handler_index[EOperationState.Building_Confict] = new AlterButtonHandler_PlacingBuilding();

            p_scroll_wheel_handler_index[EOperationState.Default] = ScrollWheelHandlers.Scroll_Default;
            p_scroll_wheel_handler_index[EOperationState.Building_Confict] = ScrollWheelHandlers.Scroll_ZoomBuildingObject;
            p_scroll_wheel_handler_index[EOperationState.Building_Dropable] = ScrollWheelHandlers.Scroll_ZoomBuildingObject;

            operation_state = EOperationState.Default;
        }

        void OnDisable() 
        {
            swift_inventory_takeout_handler.Clear();
        }

        public void TakeOut(UGUIInventoryItemConfig item_info, int short_cut_index )
        {
            //不管三七二十一，先收起旧的
            PutAway();
            active_swift_inventory_index = short_cut_index;

            if (item_info != null) 
            {
                for (int i = 0; i < swift_inventory_takeout_handler.Count; i++)
                {
                    if (swift_inventory_takeout_handler[i].OnTakeOut(item_info, this) == true)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 消耗上一个选中的一个建筑物件，记住，只能消耗一个。不可以连续消耗多个，那就是BUG了！
        /// </summary>
        public void CostSwiftSlotItem() 
        {
            swift_inventory.TakeOutFormSlot(active_swift_inventory_index,1);
            active_swift_inventory_index = -1;
            if (OnSwiftInventoryUpdate != null) 
            {
                OnSwiftInventoryUpdate(swift_inventory);
            }
        }

        void PutAway()
        {
            building_block_params.placing_building_detector.Clear();
        }

        void Update() 
        {
            p_action_monitor.Tick(Input.GetMouseButton(0));
            p_alter_monitor.Tick(Input.GetMouseButton(1));


            float wheel_value = Input.GetAxis("Mouse ScrollWheel");
            if (wheel_value != 0 && p_scroll_wheel_handler != null ) 
            {                
                p_scroll_wheel_handler( wheel_value );
            }
        }
    }

}
