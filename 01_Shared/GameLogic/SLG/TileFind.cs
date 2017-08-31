using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    /// <summary>
    /// AI辅助函数，寻找范围内目标最符合某种条件的一个格子（及其上面的单位？）
    /// </summary>
    public static class TileFind
    {
        /// <summary>
        /// 寻找备选List中具有最小值的那个数据
        /// </summary>
        /// <param name="range">备选格子范围</param>
        /// <param name="compare_value">不符合比较条件的单位，应该返回Int.MaxValue.否则，返回比较值和当前值比较</param>
        /// <returns></returns>
        public static MapTile FindMinInRange(List<MapTile> range, System.Func<MapTile, int> compare_value ) 
        {
            MapTile result = null;

            int min_val = int.MaxValue;

            for (int i = 0; i < range.Count; i++) 
            {
                if (i == 0)
                {
                    min_val = compare_value(range[0]);
                    if (min_val < int.MaxValue)
                    {
                        result = range[0];
                    }
                }
                else 
                {
                    int new_val = compare_value(range[i]);
                    if (new_val < min_val) 
                    {
                        min_val = new_val;
                        result = range[i];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 寻找备选List中具有最小值的那个数据
        /// </summary>
        /// <param name="range">备选格子范围</param>
        /// <param name="compare_value">不符合比较条件的单位，应该返回Int.MinValue.否则，返回比较值和当前值比较</param>
        /// <returns></returns>
        public static MapTile FindMaxInRange(List<MapTile> range, System.Func<MapTile, int> compare_value)
        {
            MapTile result = null;

            int max_val = int.MinValue;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0)
                {
                    max_val = compare_value(range[0]);
                    if (max_val > int.MinValue)
                    {
                        result = range[0];
                    }
                }
                else
                {
                    int new_val = compare_value(range[i]);
                    if (new_val > max_val)
                    {
                        max_val = new_val;
                        result = range[i];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 寻找备选List中具有最小值的那个数据
        /// </summary>
        /// <param name="range">备选格子范围</param>
        /// <param name="compare_value">不符合比较条件的单位，应该返回Int.MaxValue.否则，返回比较值和当前值比较</param>
        /// <returns></returns>
        public static MapTile FindMinInRangeF(List<MapTile> range, System.Func<MapTile, float> compare_value)
        {
            MapTile result = null;

            float min_val = float.MaxValue;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0)
                {
                    min_val = compare_value(range[0]);
                    if (min_val < float.MaxValue)
                    {
                        result = range[0];
                    }
                }
                else
                {
                    float new_val = compare_value(range[i]);
                    if (new_val < min_val)
                    {
                        min_val = new_val;
                        result = range[i];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 寻找备选List中具有最小值的那个数据
        /// </summary>
        /// <param name="range">备选格子范围</param>
        /// <param name="compare_value">不符合比较条件的单位，应该返回Int.MinValue.否则，返回比较值和当前值比较</param>
        /// <returns></returns>
        public static MapTile FindMaxInRangeF(List<MapTile> range, System.Func<MapTile, float> compare_value)
        {
            MapTile result = null;

            float max_val = float.MinValue;

            for (int i = 0; i < range.Count; i++)
            {
                if (i == 0)
                {
                    max_val = compare_value(range[0]);
                    if (max_val > float.MinValue)
                    {
                        result = range[0];
                    }
                }
                else
                {
                    float new_val = compare_value(range[i]);
                    if (new_val > max_val)
                    {
                        max_val = new_val;
                        result = range[i];
                    }
                }
            }

            return result;
        }
    }
}
