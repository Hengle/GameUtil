using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUtil.UI
{
    public class UGUIScrollViewButton: MonoBehaviour
    {
        public Image icon;
        public Image select;
        public Material gray_material;

        static Color color_invisible = new Color(0, 0, 0, 0);
        
        protected Color invisible_color 
        {
            get 
            {
                return color_invisible;
            }
        }

        /// <summary>
        /// if select hint need a different color
        /// use it
        /// </summary>
        virtual protected Color slect_color
        {
            get
            {
                return Color.red;
            }
        }

        protected System.Action<object> OnChoosenChanged;

        virtual public bool Choosen 
        {
            get 
            {
                return select.color.a > 0.1f;
            }
            set 
            {
                select.color = value == true ? slect_color : invisible_color;
            }
        }

        bool m_enable = true;
        virtual public bool IsEnable 
        {
            get 
            {
                return m_enable;
            }
            set 
            {
                m_enable = value;
                if (m_enable == false)
                {
                    icon.material = gray_material;
                }
                else 
                {
                    icon.material = null;
                }                
            }
        }

        public void OnItemClicked() 
        {
            if (IsEnable) 
            {
                Choosen = !Choosen;
                if (OnChoosenChanged != null)
                {
                    OnChoosenChanged(this);
                }
            }
        }        
    }

}
