using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    /// <summary>
    /// Flee 的实现，只有一点差异。那就是，方向和Seek相反。
    /// 这里只需要研究好原理即可，以保证看别人代码的时候不会懵逼。
    /// 知道原理，在阅读代码提高自己，而不是看代码瞎捉摸啥意思。
    /// 
    /// 然后可以在Apex的基础上做扩展，研究更好的寻路算法。
    /// 
    /// </summary>
    public class KinematicFlee : KinematicSeek
    {
        protected override int flee
        {
            get
            {
                return -1;
            }
        }
    }
}