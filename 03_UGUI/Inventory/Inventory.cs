using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil.UI
{

    //这个Class用于处理底层的Inventory数据存放和管理，抛去上层的所有数据
    //只考虑数据的存放，堆叠，分离，以及数据在不同的Inventory之间的传输。
    //slot_position 物品在某个Inventory里面的索引
    //item_id       物品对应整个Item表中的id。
    //count         物品数量。
    [System.Serializable]
    public class InventoryItemData
    {
        public int slot_position;
        public int item_id;
        public int count;
        public InventoryItemData Duplicate() 
        {
            InventoryItemData temp = new InventoryItemData();
            temp.slot_position = this.slot_position;
            temp.item_id = this.item_id;
            temp.count = this.count;
            return temp;
        }
    }

    //背包算法操作处理结果。
    public enum EInventoryOperateResult
    {
        Success,
        InsertNotFinish,
        ItemIDNotMatch,
        ItemNotExist,
        ItemNotEnough,
        InventoryIsFull,
        Error,
    }

    /// <summary>
    ///这个类，主要处理Inventory的底层数据。和表层显示分离开。
    ///包括：拾取，消耗，插入（堆叠插入，一个物品堆叠满了，要自动开始下一个物品格子，
    ///如果不能开启新格子，那么剩下的舍弃掉，或者保留原位）。物品栏之间的物品交互。
    ///玩家可定制的物品栏内物品排序和归纳功能
    ///
    /// TODO：Split功能没有实现
    /// TODO：跨Inventory传输数据没有实现
    /// TODO：存档没有实现
    /// 
    /// </summary>
    [System.Serializable]
    public class Inventory
    {
        int capacity;
        Dictionary<int, InventoryItemData> slot_index = new Dictionary<int, InventoryItemData>();

        public Inventory(int size) 
        {
            capacity = size;
        }

        /// <summary>
        /// 返回Inventory的最大容量
        /// </summary>
        public int Capacity 
        {
            get 
            {
                return capacity;
            }
        }

        /// <summary>
        ///返回Inventory的实际占用格子数 
        /// </summary>
        public int Count 
        {
            get 
            {
                return slot_index.Count;
            }
        }

        /// <summary>
        /// 返回InventoryItemData 数据集,每次请求会通过SlotIndex生成一个List。
        /// </summary>
        public List<InventoryItemData> InventoryDataSet
        {
            get 
            {
                return new List<InventoryItemData>(slot_index.Values);
            }
        }

        public void GiveItem(int item_id, int item_count) 
        {
            TryAddItem(item_id, ref item_count, item_count);
        }

        
        /// <summary>
        /// 拾取并放入背包：
        /// 先把同组别，没装满的给装满。
        /// 然后尝试开新Slot。
        /// 如果没有新Slot可以开，说明背包满了。剩余的物品将会被舍弃。
        /// 具体舍弃量，通过ref count 返还。
        /// </summary>
        /// <param name="item_id">该物品id</param>
        /// <param name="count">该物品的入包数量</param>
        /// <param name="max_stack">该物品的最大堆叠数量</param>
        /// <returns>装包结果</returns>
        public EInventoryOperateResult TryAddItem(int item_id,ref int count, int max_stack ) 
        {
            //Debug.Log(" add  item_id " + item_id + " count " + count + " max stack " + max_stack);
            //先把没满的填满
            if (TryFillUnFullSlot(item_id, ref count, max_stack) == EInventoryOperateResult.Success) 
            {
                return EInventoryOperateResult.Success;
            }

            while (count > 0) 
            {
                int slot_id = TryFindFirstEmptySlotID();
                
                if ( slot_id < 0 ) 
                {
                    return EInventoryOperateResult.InventoryIsFull;
                }

                if (max_stack >= count)//如果余量小于一个Slot的容量，那么填入余量，返回成功。
                {                    
                    PutIntoSlot(slot_id, item_id, count);
                    count -= count;
                    return EInventoryOperateResult.Success;
                }
                else //如果余量大于一个Slot的容量，则填满这个Slot，继续寻找下一个空的Slot，不要考虑有未满的情况，因为已经处理完这种情况了。
                {
                    PutIntoSlot(slot_id, item_id, max_stack);
                    count -= max_stack;
                }
            }
            return EInventoryOperateResult.Error;
        }

        /// <summary>
        /// 尝试把所用同类ID，没填满的，给填满。如果能装满，可以直接结束本次装包操作。没填满后面会考虑开新的Slot。
        /// </summary>
        /// <param name="item_id">  物品ID </param>
        /// <param name="rest">     传输数量</param>
        /// <param name="max_stack">该类物品的最大堆叠数量</param>
        /// <returns>装包结果</returns>
        EInventoryOperateResult TryFillUnFullSlot(int item_id, ref int rest, int max_stack) 
        {
            foreach (var slot in slot_index.Values) 
            {
                if (slot.item_id == item_id && slot.count < max_stack )
                {
                    int accept = max_stack - slot.count;
                    if (accept >= rest)
                    {
                        slot.count += rest;
                        rest -= rest;
                    }
                    else 
                    {
                        slot.count += accept;
                        rest -= accept;
                    }

                    if (rest == 0) 
                    {
                        return EInventoryOperateResult.Success;
                    }
                }
            }
            return EInventoryOperateResult.InsertNotFinish;
        }

        /// <summary>
        /// 寻找第一个空的格子。当装包操作，没能在已有同类ID的物件格子里面，满足装包需求时，会调用这个函数
        /// 没有Key的格子，和容量为0的格子，都视为，空格子。
        /// </summary>
        /// <returns>空格子的ID，如果返回-1，说明背包已经满了。</returns>
        int TryFindFirstEmptySlotID() 
        {
            for (int i = 0; i < capacity; i++) 
            {
                if (slot_index.ContainsKey(i) == false) 
                {
                    return i;
                }
                else if( slot_index[i].count == 0 ) 
                {
                    return i;
                }
            }

            return -1;
        }

        //注意，不检查Stack上限限制，那个应该在外部就完成
        public EInventoryOperateResult PutIntoSlot(int slot_id, int item_id, int count) 
        {
            //Debug.Log(string.Format("Slot {0}, id {1}, count {2}", slot_id, item_id, count));

            InventoryItemData data = null;
            slot_index.TryGetValue(slot_id, out data);
            //if 开一个新格子。
            //else 装填一个旧格子。
            if (data == null)
            {
                data = new InventoryItemData();
                data.item_id = item_id;
                data.slot_position = slot_id;
                data.count = count;
                slot_index[slot_id] = data;
                return EInventoryOperateResult.Success;
            }
            else 
            {
                if (data.count > 0 && data.item_id != item_id)
                {
                    return EInventoryOperateResult.ItemIDNotMatch;
                }
                else 
                {
                    data.item_id = item_id;
                    data.count += count;
                    return EInventoryOperateResult.Success;
                }
            }            
        }

        /// <summary>
        /// if count is less than zero, just empty it.
        /// </summary>
        /// <param name="slot_id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EInventoryOperateResult TakeOutFormSlot( int slot_id, int count = -1 ) 
        {
            //如果count不填，则全部拿出来
            if (count < 0) 
            {
                slot_index.Remove(slot_id);
                return EInventoryOperateResult.Success;
            }
            
            //如果填写count，则拿出指定的个数。
            InventoryItemData data = this[slot_id];
            if (data == null)
            {
                return EInventoryOperateResult.ItemNotExist;
            }
            else 
            {
                if (data.count < count)
                {
                    return EInventoryOperateResult.ItemNotEnough;
                }
                else 
                {
                    data.count -= count;//结果为0，也不用remove，count为0之后会认为是一个空格子。
                    if (data.count == 0) 
                    {
                        slot_index.Remove(slot_id);
                    }
                    return EInventoryOperateResult.Success;
                }
            }
        }

        /// <summary>
        /// 通过 inventory[slot_id]的方式，去访问slot成员
        /// </summary>
        /// <param name="id">想要访问的格子id</param>
        /// <returns>InventoryItemData,如果id不存在，则会返回空</returns>
        public InventoryItemData this[int id]
        {
            get
            {
                InventoryItemData result = null;
                slot_index.TryGetValue( id, out result);
                return result;
            }
        }
    }
}
