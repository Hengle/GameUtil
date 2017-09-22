using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{
    /// <summary>
    /// 定义轴，很多地方可以用，不知道Unit用有没有默认的轴
    /// </summary>
    public enum EAxis
    {
        X,
        Y,
        Z,
    }

    /// <summary>
    /// 抽出来是因为这个比较通用。
    /// </summary>
    public enum EUpdateType
    {
        Update,
        LateUpdate,
        FixedUpdate,
    }

}

