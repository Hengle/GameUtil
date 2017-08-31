using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{
    using GameUtil.Algorithm;
    using GameUtil.TileGen;

    /// <summary>
    /// 目前包含三大块功能
    /// 【1】AStar算法接口的实现
    /// 【2】Element栈实现
    /// 【3】点击处理函数的消息栈
    /// </summary>
	public class MapTile : MonoBehaviour,IAStarNode 
    {
		public MapTile[] neibhours = new MapTile[4];

		#region AStar Relative.TilePoint Relative
		const int c_manhaton_dist = 10;

		struct AStartStruct 
		{
			//public IAStarNode [] cached_nodes;
			public IAStarNode parent;
			public int HValue;
			public int GValue;
		}
		AStartStruct m_astar_data;

		[SerializeField]
		TilePoint m_point;
		public TilePoint point
		{
			get{return m_point; }
			set{ m_point = value; }
		}

		//获得邻居节点的接口，这样就不限于只能移动上下左右了。
		public IAStarNode [] Neibhours
		{
			get 
			{
				return neibhours;
			}
		} 
		//获得点的X坐标
		public int X
		{
			get
			{
				return m_point.x;
			}
		}
		//获得点的Y坐标
		public int Y
		{
			get
			{
				return m_point.y;
			}
		}

		//EG：曼哈顿距离*10
		public void CalculeteH(IAStarNode dest)
		{
			MapTile dest_tile = dest as MapTile;
			m_astar_data.HValue = (dest_tile.m_point - this.m_point) * c_manhaton_dist;
		}

		//EG：水平垂直每次加10，斜角每次加14等（约等于根号200）
		public int G 
		{
			get 
			{
				return m_astar_data.GValue;
			}
			set 
			{
				m_astar_data.GValue = value;
			}
		}

		/// <summary>
		/// 这样做是为了方便计算，斜角的时候，的GWeight
		/// </summary>
		/// <param name="target_node"></param>
		/// <returns></returns>
		public int GWeightTo( IAStarNode target_node )
		{
			return c_manhaton_dist;
		}

		//F = G + H
		public int F 
		{
			get 
			{
				return m_astar_data.GValue + m_astar_data.HValue;
			} 
		}

		virtual public bool Walkable 
		{
			get
			{
				return true;
			}
		}

		public IAStarNode ParentNode 
		{
			get 
			{
				return m_astar_data.parent;
			}
			set 
			{
				m_astar_data.parent = value;
			}
		}
		#endregion





        #region Element Stack
        protected List<MapElement> element_stack = new List<MapElement>();

        public void Push(MapElement element) 
        {
            if (element_stack.Contains(element) == false) 
            {
                element_stack.Add(element);
                element.OnDeployOnTile(this);
            }
        }

        public int Remove(MapElement element) 
        {
            int index = element_stack.IndexOf(element);
            if (index >= 0) 
            {
                element_stack[index].OnRemoveFromTile(this);
                element_stack.RemoveAt(index);
            }
            return index;
        }

        public MapElement Top() 
        {
            if (element_stack.Count > 0)
                return element_stack[element_stack.Count - 1];
            return null;
        }

        public MapElement Pop() 
        {
            if (element_stack.Count > 0)
            {
                MapElement top_element = element_stack[element_stack.Count - 1];
                element_stack.RemoveAt(element_stack.Count - 1);
                top_element.OnRemoveFromTile(this);
                return top_element;
            }
            return null;
        }

        #endregion






        #region Event And Handlers
        [HideInInspector]
        protected int m_repeat_selectcount = 0;
		protected List<System.Func<bool>> OnFocusHandlers = new List<System.Func<bool>>();
		protected List<System.Func<bool>> OnLostFocusHandlers = new List<System.Func<bool>>();

        public int repeat_selectcount 
        {
            get 
            {
                return m_repeat_selectcount;
            }
        }

		virtual public void OnFocus()
		{
            m_repeat_selectcount++;
			for (int i = 0; i < OnFocusHandlers.Count; i++)
			{
				if (OnFocusHandlers[i] != null && OnFocusHandlers[i]()) break;
			}
		}

		virtual public void OnLostFocus()
		{
            m_repeat_selectcount = 0;
			for (int i = 0; i < OnLostFocusHandlers.Count; i++)
			{
				if (OnLostFocusHandlers[i] != null && OnLostFocusHandlers[i]()) break;
			}
		}

        virtual public void SetRange( ERangeHint hint = ERangeHint.Hide ){}
        virtual public void SetPath(EPathHint hint = EPathHint.Hide) { }

        //virtual public void SetRange( bool show, string range_type_string){}
		#endregion 
	}	
}

