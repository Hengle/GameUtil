using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;
using GameUtil.OpenWorld;

namespace GameUtil.Examples
{
    
    public class GameFacade : Singleton<GameFacade>  
    {
        [SerializeField]
        WorldManager world_manager;

        [SerializeField]
        LocalPlayer player;


        public LocalPlayer GetLocalPlayer() 
        {
            return player;
        }

        public WorldManager GetWorldManager() 
        {
            return world_manager;
        }

        public void SaveWorld() 
        {
            world_manager.SaveWorld();
            //存完盘，在回复这个值。
            can_quick_save = true;
        }

        public void LoadWorld() 
        {
            //测试版就不加UI遮罩了。正式版应该只在加载场景的时候，才允许LoadWorld(包括加载区块场景，但是加载的时候那个场景一定看不到的！不用担心玩家感到诡异)
            world_manager.ClearWorld();
            world_manager.LoadWorld();
        }

        //防止连续按F5快存的标志位。
        bool can_quick_save = true;
        void Update() 
        {
            if (Input.GetKey(KeyCode.F5)) 
            {
                if (can_quick_save) 
                {
                    can_quick_save = false;
                    SaveWorld();
                }
            }
        }
    }

}
