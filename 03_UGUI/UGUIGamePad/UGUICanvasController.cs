using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.UI
{
    /// <summary>
    /// 设计一个多用途，可定制的UGUI摇杆。
    /// 支持最多2个轴和N个按钮。
    /// </summary>
    public class UGUICanvasController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Joystick can not more than two!")]
        private List<UGUIJoystick> joysticks = new List<UGUIJoystick>();
        [SerializeField]
        private List<UGUIJoyStickButton> buttons = new List<UGUIJoyStickButton>();
 
        private Dictionary<string, UGUIJoyStickButton> button_index = new Dictionary<string, UGUIJoyStickButton>();

        public UGUIJoystick MainStick 
        {
            get 
            {
                if (joysticks.Count > 0) 
                {
                    return joysticks[0];
                }
                return null;
            }
        }

        public UGUIJoystick ViseStick 
        {
            get 
            {
                if (joysticks.Count > 1) 
                {
                    return joysticks[1];
                }
                return null;
            }
        }

        public UGUIJoyStickButton GetButton(string button_name) 
        {
            UGUIJoyStickButton button = null;
            button_index.TryGetValue(button_name, out button);
            return button;
        }

        void Awake() 
        {
            foreach (var btn in buttons) 
            {
                button_index[btn.button_name] = btn;
            }
        }
    }

}
