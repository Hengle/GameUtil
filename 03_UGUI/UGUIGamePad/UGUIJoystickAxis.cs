using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UGUIJoystickAxis : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler 
{
    [HideInInspector]
    public UGUIJoystick owner;
    [HideInInspector]
    public Vector2 axis;
    [HideInInspector]
    public float angle_z;
    
    RectTransform p_rect;
    public RectTransform rectTrans 
    {
        get 
        {
            if (p_rect == null) 
            {
                p_rect = transform as RectTransform;
            }
            return p_rect;
        }
    }

    Vector2 start_position;
    public void OnBeginDrag(PointerEventData eventData) 
    {
        start_position = rectTrans.position;
        if (owner.OnPress != null)
        {
            owner.OnPress();
        }
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        rectTrans.anchoredPosition = Vector3.zero;
        if (owner.OnRelease != null) 
        {
            owner.OnRelease();
        }
        Zero();
    }

    public void OnDrag(PointerEventData eventData) 
    {
        Vector2 full_local_position = eventData.position - start_position;

        float magnitude = full_local_position.magnitude;
        axis = full_local_position.normalized;

        if (magnitude > owner.drag_distance)
        {
            rectTrans.localPosition = axis * owner.drag_distance;
        }
        else 
        {
            rectTrans.localPosition = full_local_position;
        }

        if (magnitude < owner.deadzone)
        {
            Zero();
        }
        else 
        {
            angle_z = axis.x > 0? -Vector2.Angle(rectTrans.up, axis):Vector2.Angle(rectTrans.up, axis);
        }
    }

    void Zero() 
    {
        axis = Vector2.zero;
        if (owner.reset_angle_z_when_release)
        {
            angle_z = 0;
        }
    }

}

