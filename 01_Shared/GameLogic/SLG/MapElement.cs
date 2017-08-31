using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    public interface ISelectable
    {
        void OnFocus();
        void OnLostFocus();
    }

    /// <summary>
    /// 注意，切换模式的时候，要先Remove，再重新绑定上去，虽然麻烦，但是不出错。
    /// </summary>
    public interface IDeployable
    {
        void OnDeployOnTile(MapTile tile);
        void OnRemoveFromTile(MapTile tile);
    }

    /// <summary>
    /// 所有SLG游戏地图上元素的基类。这里实现最最基础的功能。
    /// 无论是一个可控制的战斗单位
    /// 不可移动的建筑单位，还是无关紧要的装饰，还是消耗性可占有补给，都要继承这个类
    /// </summary>
    public class MapElement : MonoBehaviour,ISelectable,IDeployable
    {
        //SLG地图，一定是可索引的，不然遍历整个地图去寻找一个点，
        //计算量太恐怖了。
        protected int m_tile_code = int.MinValue;

        virtual public void OnFocus() { }
        virtual public void OnLostFocus() { }
        
        /// <summary>
        /// Override this method need call base.xxx;
        /// </summary>
        /// <param name="tile"></param>
        virtual public void OnDeployOnTile(MapTile tile) 
        {
            m_tile_code = SharedUtil.PointHash(tile.point.x, tile.point.y);
            //同步位置
            transform.position = tile.transform.position;
        }
        
        /// <summary>
        /// Override this method need call base.xxx;
        /// </summary>
        /// <param name="tile"></param>
        virtual public void OnRemoveFromTile(MapTile tile)
        {
            m_tile_code = int.MinValue;
        }

        virtual public MapTile OwnerTile {  get;  }

        virtual public bool IsSelectElement { get; }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

