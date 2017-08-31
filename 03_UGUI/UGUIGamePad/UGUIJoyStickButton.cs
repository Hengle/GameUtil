using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UGUIJoyStickButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public System.Action OnPress;
    public System.Action OnRelease;
    public bool is_pressing = false;
    
    public string button_name = "Action";

    public void OnPointerDown(PointerEventData eventData) 
    {
		if (OnPress != null) 
		{
			OnPress ();
		}
		is_pressing = true;
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
		if (OnRelease != null) 
		{
			OnRelease ();
		}
		is_pressing = false;
    }
}
