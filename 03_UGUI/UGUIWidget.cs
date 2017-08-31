using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtil.UI;

namespace GameUtil.UI
{

    //所有的受到UGUImanager管理的页面，都应继承这个基类
    public class UGUIWidget : MonoBehaviour
    {
        List<UGUIWidget> cascade_closing_list = new List<UGUIWidget>();
        protected RectTransform rect_transform = null;

        public Vector2 widget_position 
        {
            get 
            {
                return rect_transform.anchoredPosition;
            }
            set 
            {
                rect_transform.anchoredPosition = value;
            }
        }

        /// <summary>
        /// 增加一个级联关闭对象，当这个页面关闭时，级联关闭的页面，也会跟着关闭。
        /// </summary>
        /// <param name="target"></param>
        public void AppendCascadeClosing(UGUIWidget target) 
        {
            cascade_closing_list.Add(target);
        }

        void CascadeClose() 
        {
            if (cascade_closing_list.Count > 0) 
            {
                foreach (var widget in cascade_closing_list) 
                {
                    UGUIManager.Instance.CloseDialog(widget);
                }

                cascade_closing_list.Clear();
            }
        }


        //模态对话框，就是通过增加遮罩层来实现的
        //因此只分三种情况讨论，人畜无害的Dialog，需要隐藏掉其他窗体的Dialog_HideOthers，和钉子户Dialog_CanNotBeHide。
        public enum EDialogType 
        {
            NonDialog,
            Dialog,
            Dialog_HideOthers,
            Dialog_CanNotBeHide,            
        }

        public EDialogType dialog_type = EDialogType.Dialog;

        [HideInInspector]
        public int hide_batch;

        void OnEnable() 
        {
            rect_transform = transform as RectTransform;
            OnShowPage();
        }

        virtual protected void OnHidePage() 
        {

        }

        virtual protected void OnShowPage() 
        {

        }

        public void OnExit() 
        {
            UGUIManager.Instance.CloseDialog(this);
            CascadeClose();
        }

        public void Dispose() 
        {
            OnHidePage();
            Destroy(gameObject);
        }
    }



}
