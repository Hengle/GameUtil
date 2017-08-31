using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil 
{
    public interface IPressStateHandler 
    {
        void OnBeginPress();
        void OnRelease();
        void OnPressing();
    }

    public class PressStateMonitor
    {
        public IPressStateHandler handler;
        public bool is_pressing;

        public void Tick(bool press_state) 
        {
            if (is_pressing == false && press_state == true)
            {
                if (handler != null) 
                {
                    handler.OnBeginPress();
                }
            }
            else if (is_pressing == true && press_state == false) 
            {
                if (handler != null) 
                {
                    handler.OnRelease();
                }
            }

            is_pressing = press_state;

            if (is_pressing && handler != null ) 
            {
                handler.OnPressing();
            }
        }
    }

}
