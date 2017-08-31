using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace GameUtil.UI
{
    [ExecuteInEditMode]
    public class UGUIInventoryBuilder : MonoBehaviour
    {
        public Graphic content;
        public UGUIInventoryItem prototype;

        public int x_count;
        public int y_count;

        public float slot_width;
        public float slot_height;

        public float x_origin;  //X 原点
        public float y_origin;  //Y 原点
        public float x_space;   //水平间隔
        public float y_space;   //垂直间隔

        public void BuildInventory() 
        {
            UGUIInventoryItem[] olds = GetComponentsInChildren<UGUIInventoryItem>();
            foreach (var pending_delete in olds) 
            {
                DestroyImmediate(pending_delete.gameObject);
            }

            if (prototype != null) 
            {
                for (int iy = 0; iy < y_count; iy++)             
                {
                    for (int ix = 0; ix < x_count; ix++) 
                    {
                        UGUIInventoryItem slot = GameObject.Instantiate<UGUIInventoryItem>(prototype);
                        slot.x = ix;
                        slot.y = iy;
                        slot.name = slot.ToString();
                        
                        RectTransform rt = slot.transform as RectTransform;
                        rt.SetParent(content.transform);
                        if (rt != null) 
                        {
                            rt.anchoredPosition = new Vector2(x_origin + ix * slot_width + ix * x_space, y_origin - iy * slot_height - iy * y_space);
                        }
                    }
                }
            }
        }
    }

}

