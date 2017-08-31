using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UGUIJoystick : MonoBehaviour 
{

    public float deadzone = 30;
    public float drag_distance = 144;
    public bool reset_angle_z_when_release = false;
    public bool debug_info = true;

    public System.Action OnPress;
    public System.Action OnRelease;

    public float angle_z 
    {
        get 
        {
            return joystick_axis.angle_z;
        }
    }

    public Vector2 axis 
    {
        get 
        {
            return joystick_axis.axis;
        }

    }
    public UGUIJoystickAxis joystick_axis;

    void Awake() 
    {
        joystick_axis.owner = this;
    }

    public override string ToString()
    {
        return string.Format(" X {0}, Y {1}, Angle {2} ", axis.x.ToString("F6"), axis.y.ToString("F6"), angle_z.ToString("F6"));
    }

    void Update() 
    {
        if (debug_info) 
        {
            Debug.Log(ToString());
        }        
    }
}
