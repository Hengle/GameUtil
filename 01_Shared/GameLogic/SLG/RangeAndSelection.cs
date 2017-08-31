using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    public interface ISubManager
    {
        void OnRegister();
        void OnInitBattlefield();
    }

    public enum EPathHint 
    {
        Hide,
        MovePath,
    }

    public enum ERangeHint
    {
        Hide,
        Deploy,
        Attack,
        Guard,
        CanMoveTo,
        CanNotMoveTo,
    }

    public enum EClearRangeType
    {
        Range,
        MovePath,
        All,
    }

    /// <summary>
    /// ToDo：把这个拆成两部分，一部分是实现ISubManager的管理器类
    /// 而另一部分，是代表一种RangeHint的具体Hint类RangeHintCollection。这个以后还要做扩展，支持Custom生成mesh的RangeHintGen类
    /// 可以通过拖拽的方式把一个Hint的Prefab添加给RangeHintCollection类，然后可以以协程的方式生成路径或区域。
    /// </summary>
    public class RangeAndSelection:ISubManager
    {
        ISelectable m_select_element;
        MapTile m_select_tile;
        List<MapTile> m_show_range_list = new List<MapTile>();
        List<MapTile> m_move_path_list = new List<MapTile>();

        virtual public void OnRegister()
        {
            m_select_tile = null;
            m_select_element = null;
        }

        virtual public void OnInitBattlefield(){}
        
        public void ClearSelect(EClearRangeType type = EClearRangeType.All)
        {
            if (select_tile != null) 
            {
                select_tile.SetPath(EPathHint.Hide);
                select_tile.SetRange(ERangeHint.Hide);
            }            

            Debug.Log("move list " + m_move_path_list.Count + " normal range list " + m_show_range_list.Count);

            foreach (var tile in m_show_range_list)
            {
                if (type == EClearRangeType.All || type == EClearRangeType.Range) 
                {
                    tile.SetRange(ERangeHint.Hide);
                }
            }

            foreach (var tile in m_move_path_list)
            {
                if (type == EClearRangeType.All || type == EClearRangeType.MovePath)
                {
                    tile.SetPath(EPathHint.Hide);
                }
            }

            if (type == EClearRangeType.All || type == EClearRangeType.Range) m_show_range_list.Clear();
            if (type == EClearRangeType.All || type == EClearRangeType.MovePath) m_move_path_list.Clear();
        }

        public MapTile select_tile
        {
            get
            {
                return m_select_tile;
            }
            set
            {
                if (m_select_tile == null || (m_select_tile != null && m_select_tile != value))
                {
                    //On Select Tile Changed
                    if (m_select_tile != null)
                    {
                        m_select_tile.OnLostFocus();
                    }
                    m_select_tile = value;
                    if (m_select_tile != null)
                    {
                        m_select_tile.OnFocus();
                    }
                }
                else if (m_select_tile == value)
                {
                    m_select_tile.OnFocus();
                }
            }
        }

        public ISelectable select_element
        {
            get 
            {
                return m_select_element;
            }
            set 
            {
                if (m_select_element == null || (m_select_element != null && m_select_element != value))
                {
                    //On Select Tile Changed
                    if (m_select_element != null)
                    {
                        m_select_element.OnLostFocus();
                    }
                    m_select_element = value;
                    if (m_select_element != null)
                    {
                        m_select_element.OnFocus();
                    }
                }
                else if (m_select_element == value)
                {
                    m_select_element.OnFocus();
                }
            }
        }

        public void ShowDeployRange(List<MapTile> tile_list) 
        {
            foreach (var node in tile_list)
            {
                m_show_range_list.Add(node);
                node.SetRange( ERangeHint.Deploy );
            }
        }

        /// <summary>
        /// Move Path 至少大于1才会有意义！
        /// </summary>
        /// <param name="tile_list"></param>
        public void ShowMovePath(List<MapTile> tile_list) 
        {
            if (tile_list.Count > 1) 
            {
                foreach (var node in tile_list)
                {
                    m_move_path_list.Add(node);
                    node.SetPath(EPathHint.MovePath);
                }
            }
        }

        public void ShowAttackRange(List<MapTile> tile_list)
        {
            foreach (var node in tile_list)
            {
                m_show_range_list.Add(node);
                node.SetRange(ERangeHint.Attack);
            }
        }
    }
}
