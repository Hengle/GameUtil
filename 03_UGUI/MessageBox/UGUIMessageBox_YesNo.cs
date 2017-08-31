using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameUtil.UI
{
    public class UGUIMessageBox_YesNo : UGUIWidget
    {
        [SerializeField]
        Text txt_content;

        public delegate void UserSelectDelegate();
        public UserSelectDelegate OnYes;
        public UserSelectDelegate OnNo;

        public void InitMessageBox(string countent, UserSelectDelegate on_yes, UserSelectDelegate on_no = null ) 
        {
            OnYes = on_yes;
            OnNo = on_no;
        }

        public void OnYesClicked() 
        {
            if (OnYes != null)
            { 
                OnYes(); 
            }

            OnExit();
        }

        public void OnNoClicked() 
        {
            if (OnNo != null)
            {
                OnNo();
            }

            OnExit();
        }
    }
}
