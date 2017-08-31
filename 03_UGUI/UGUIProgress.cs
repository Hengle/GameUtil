using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GameUtil.UI
{
    public class UGUIProgress : MonoBehaviour
    {
        public Image front_bar;

        float progress;
        public float percent 
        {
            get 
            {
                return progress;
            }
            set 
            {
                progress = Mathf.Clamp(value, 0, 1);
                rect_trans.sizeDelta = new Vector2(width * progress, rect_trans.sizeDelta.y);
            }
        }

        float width;
        RectTransform rect_trans;

        // Use this for initialization
        void Start()
        {
            rect_trans = front_bar.GetComponent<RectTransform>();
            width = rect_trans.sizeDelta.x;
        }
    }

}

