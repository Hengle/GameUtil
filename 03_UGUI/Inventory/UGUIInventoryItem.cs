using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil.UI
{
    public enum InventoryItemSlotType 
    {
        Storage,
        Reference,
    }

    public class UGUIInventoryItem : UGUIWidget,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler,IPointerDownHandler
    {
        private static UGUIInventoryItem last_drag_item;

        public System.Action<int> OnSelectCallBack;

        public InventoryItemSlotType slot_type = InventoryItemSlotType.Storage;
        public Image item_image;
        public Text item_count;
        [HideInInspector]
        public Inventory inventory_owner;
        [HideInInspector]
        public int x;
        [HideInInspector]
        public int y;

        private int inventory_slot_id;
        InventoryItemData inventory_data;

        void Awake()
        {
            onDropResponseList.Clear();
            onDropResponseList.Add(FilterItemType);             //not implemented
            onDropResponseList.Add(DropResponse_ToEmpty);
            onDropResponseList.Add(DropResponse_ToDifferent);
            onDropResponseList.Add(DropResponse_ToSameItem);
        }
            
        public override string ToString()
        {
            return "Slot[" + x.ToString("D2") + "，" + y.ToString("D2") + "]";
        }

        public void InitInventoryItem(Inventory inventory_source, int slot_id, bool init_as_empty = true) 
        {
            inventory_owner = inventory_source;
            inventory_slot_id = slot_id;
            if (init_as_empty) 
            {
                Empty();
            }
        }

        
        virtual public void SetItem( InventoryItemData in_data ) 
        {
            inventory_data = in_data;
            UGUIInventoryItemConfig item_info =  GameSettings.GetInventoryItemConfig(inventory_data.item_id);

            //set ui reference
            item_image.color = Color.white;   
            item_image.sprite = item_info.item_image;
            item_count.text = inventory_data.count.ToString();
        }

        private static Color C_EMPTY_COLOR = new Color(0, 0, 0, 0);
        void Empty()
        {
            inventory_data = null;
            item_image.color = C_EMPTY_COLOR;
            item_count.text = "";
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            if (OnSelectCallBack != null) 
            {
                OnSelectCallBack(inventory_slot_id);
            }
        }

        //为了效率，搞一个成员变量吧。
        UGUIInventroyDragingItem draging_image;
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (inventory_data != null && draging_image == null) 
            {
                draging_image = UGUIManager.Instance.OpenDialog<UGUIInventroyDragingItem>("Draging_Image");
                draging_image.image.sprite = item_image.sprite;

                RectTransform this_rect = transform as RectTransform;
                Vector2 delta_shift = new Vector2(item_image.rectTransform.sizeDelta.x * 0.5f, -item_image.rectTransform.sizeDelta.y * 0.5f);

                draging_image.image.rectTransform.anchoredPosition = this_rect.anchoredPosition + delta_shift;
                item_image.color = new Color(1, 1, 1, 0.6f);

                last_drag_item = this;
                discard_after_drag = true;
            }
        }

        [HideInInspector]
        public bool discard_after_drag;
        public void OnEndDrag(PointerEventData eventData)
        {
            if (draging_image != null) 
            {
                UGUIManager.Instance.CloseDialog(draging_image);

                if (inventory_data != null && inventory_data.count > 0)
                {
                    item_image.color = Color.white;
                }
                else
                {
                    Empty();
                }

                draging_image = null;
            }

            if (discard_after_drag) 
            {
                Debug.Log("No Drop Target, item will be discarded");                
            }
        }

        public void OnDrag(PointerEventData eventData) 
        {
            if (draging_image != null) 
            {
                Vector2 result;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    draging_image.transform.parent as RectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out result);
                draging_image.image.rectTransform.anchoredPosition = result;
            }
        }
        


        public enum EMessageChainOperation 
        {
            Continue,
            ReturnSuccess,
            Interrupt,
        }

        protected delegate EMessageChainOperation ResponseChain();
        protected List<ResponseChain> onDropResponseList = new List<ResponseChain>();

        void UpdateViewAfterDrop()
        {
            //InventoryItemData last_invt_data = inventory_owner.GetInventoryData(inventory_slot_id);
            InventoryItemData last_invt_data = inventory_owner[inventory_slot_id];
            if (last_invt_data != null && last_invt_data.count > 0)
            {
                SetItem(last_invt_data);
            }
            else
            {
                Empty();
            }
        }

        //TODO:实现过滤物件类型的功能。
        EMessageChainOperation FilterItemType() 
        {
            return EMessageChainOperation.Continue;
        }

        /// <summary>
        /// 只实现了实现函数级别的消息链模式
        /// 不想用类级别的消息链，是因为开销比较大。并且管理麻烦。还需要来回倒数据
        /// 这样做能做到调理清晰的目的就够了。
        /// 可扩展性并没有被作为首选考虑内容。性能和代码清晰度作为首选。
        /// TODO:以后会扩展出 不同Inventory之间的道具交换。暂时不考虑这一点。
        /// </summary>
        /// <returns></returns>
        EMessageChainOperation DropResponse_ToEmpty() 
        {
            //自己更自己的，跨Inventory也没问题，只需要检测是否允许接受该类型的物件，今后会加这个过滤器
            if ( inventory_data == null || (inventory_data != null && inventory_data.count == 0)) 
            {
                int move_count = last_drag_item.inventory_owner[last_drag_item.inventory_slot_id].count;
                last_drag_item.inventory_owner.TakeOutFormSlot(last_drag_item.inventory_slot_id, move_count);
                inventory_owner.PutIntoSlot(inventory_slot_id, last_drag_item.inventory_data.item_id, move_count);

                UpdateViewAfterDrop();
                last_drag_item.UpdateViewAfterDrop();
                return EMessageChainOperation.ReturnSuccess;
            }
            return EMessageChainOperation.Continue;
        }

        EMessageChainOperation DropResponse_ToDifferent() 
        {
            if (inventory_data != null &&
                inventory_data.item_id != last_drag_item.inventory_data.item_id )
            {
                InventoryItemData source =  last_drag_item.inventory_data.Duplicate();
                InventoryItemData target =  inventory_data.Duplicate();

                //一定是全拿出来的，不然就错了！！！
                last_drag_item.inventory_owner.TakeOutFormSlot(last_drag_item.inventory_slot_id, source.count);
                last_drag_item.inventory_owner.PutIntoSlot(last_drag_item.inventory_slot_id, target.item_id, target.count);

                inventory_owner.TakeOutFormSlot(inventory_slot_id, target.count);
                inventory_owner.PutIntoSlot(inventory_slot_id, source.item_id, source.count);

                UpdateViewAfterDrop();
                last_drag_item.UpdateViewAfterDrop();
                return EMessageChainOperation.ReturnSuccess;
            }
            return EMessageChainOperation.Continue;
        }

        EMessageChainOperation DropResponse_ToSameItem() 
        {
            if (inventory_data != null &&
                inventory_data.item_id == last_drag_item.inventory_data.item_id)
            {
                int max_stack = GameSettings.GetInventoryItemConfig(inventory_data.item_id).max_stack;

                //移动数量，是接受数量，和源Slot数量，取最小值。
                int transfer_count = Mathf.Min( max_stack - inventory_data.count, last_drag_item.inventory_data.count );
                if( transfer_count > 0 )
                {
                    inventory_owner.PutIntoSlot( inventory_slot_id, inventory_data.item_id, transfer_count );
                    last_drag_item.inventory_owner.TakeOutFormSlot(last_drag_item.inventory_slot_id, transfer_count);

                    UpdateViewAfterDrop();
                    last_drag_item.UpdateViewAfterDrop();
                }
                return EMessageChainOperation.ReturnSuccess;
            }
            return EMessageChainOperation.Continue;
        }

        public void OnDrop(PointerEventData eventData) 
        {
            if (last_drag_item != null)
            {
                last_drag_item.discard_after_drag = false;

                for (int i = 0; i < onDropResponseList.Count; i++) 
                {
                    //虽然比bool型麻烦，但是结果一目了然，逻辑清晰无歧义，不容易产生bug
                    EMessageChainOperation result = onDropResponseList[i]();
                    if (result == EMessageChainOperation.Interrupt || result == EMessageChainOperation.ReturnSuccess)
                        break;
                }

                last_drag_item = null;
            }
        }
    }

}

