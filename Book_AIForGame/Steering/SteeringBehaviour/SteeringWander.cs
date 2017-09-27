using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.AI.Steering
{
    //这个反而很好理解。
    //PS，也可以考虑将SteeringAgent改写成完全逻辑和表现分离的方式。
    //这样就完全用数据结构去驱动
    public class SteeringWander : SteeringFaceTarget
    {
        public float wanderOffset;
        public float wanderRadius;

        public float wanderRate;                    //蜿蜒的最大角度限制
        public float wanderOrientation;             //当前朝向

        public float maxAccerlation;                //最大加速度限制

        float target_orientation;
        
        /// <summary>
        /// 这样做并不是很好，后面的处理碰撞预测，和碰墙预测，就搞不好了。
        /// 建议还是改成，将Target作为结构体出现，并且，增加GetTargetFormGameObject和ApplyTargetToGameObject两个方法
        /// 用来驱动Unity的GameObject行动，这样还可以做到，完全的，游戏逻辑和显示层分离。先不写了，整理代码的时候，在搞这块
        /// 我要丰富一下Apex和BehaviorDesigner的算法
        /// </summary>
        /// <returns></returns>
        protected override float CalculateOrientation()
        {
            //更新蜿蜒朝向
            wanderOrientation = SteerUtils.RandomBinomial() * wanderRate;

            //计算蜿蜒方向，相对于当前朝向的方向。
            target_orientation = wanderOrientation + character.orientation * Mathf.Deg2Rad;

            return target_orientation;
        }

        public override bool GetSteering(out SteeringOutput output)
        {
            //计算直线Offset
            Vector3 target = character.position + wanderOffset * SteerUtils.OrientationToVector3_XZ(character.orientation);

            //计算小圆半径Offset
            target += wanderRadius * SteerUtils.OrientationToVector3_XZ(target_orientation);

            base.GetSteering(out output);

            output.linearAccerlation = maxAccerlation * SteerUtils.OrientationToVector3_XZ(character.orientation);

            return true;
        }
    }
}