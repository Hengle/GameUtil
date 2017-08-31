using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameUtil 
{
    public interface ITicker
    {
        void Tick(float delta_time);
    }

    public class TickManager : ISubManager
    {
        List<ITicker> ticker_list = new List<ITicker>();

        public void OnRegister() 
        {
            ticker_list.Clear();
        }

        public void OnInitBattlefield() 
        {

        }        

        public void AddTickListener(ITicker tick_listener) 
        {
            if (ticker_list.Contains(tick_listener) == false) 
            {
                ticker_list.Add(tick_listener);
            }            
        }

        public void RemoveTickListener(ITicker tick_listerer) 
        {
            ticker_list.Remove(tick_listerer);
        }

        public void TickForAllListeners(float delta_time) 
        {
            if( ticker_list.Count > 0 )
            {
                for (int i = ticker_list.Count - 1; i >= 0; i--) 
                {
                    if (ticker_list[i] == null)
                    {
                        ticker_list.RemoveAt(i);
                    }
                    else 
                    {
                        ticker_list[i].Tick(delta_time);
                    }
                }
            }
        }
    }

}
