using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Examples 
{
    /// <summary>
    /// 所有具有HP，Armor类属性的建筑，生物，需要实现这个接口。
    /// </summary>
    public interface iHealthy 
    {
        int HP { get; set; }
        int HPMax { get; set; }
    }
}