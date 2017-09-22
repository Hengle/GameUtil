using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    /// <summary>
    /// FPS 人物站立状态
    /// </summary>
    public enum EPlayerStandingState
    {
        Standing,
        Couching,
        Inching,
        Standing_HoldBreathing,
        Couching_HoldBreathing,
        Inching_HoldBreaching,
        Airing,//挑起，衰落，拱飞，甚至被击震动，暂时统一为Airing状态
    }


}