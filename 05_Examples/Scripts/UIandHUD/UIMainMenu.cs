using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtil.Examples
{
    public class UIMainMenu : MonoBehaviour
    {
        public void OnSaveClicked()
        {
            GameFacade.Instance.SaveWorld();
        }

        public void OnLoadClicked() 
        {
            GameFacade.Instance.LoadWorld();
        }

        public void OnQuitClicked() { }
        public void OnOptionClicked() { }
        public void OnReturnToTitleClicked() { }
    }
}