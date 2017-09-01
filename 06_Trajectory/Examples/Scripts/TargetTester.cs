using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTester : MonoBehaviour 
{
    int hit_counter;
    [SerializeField]
    string hint_string = "I am hit!";
    void OnHit( object target ) 
    {
        Debug.Log(hint_string + hit_counter + " hit at " + ((Vector3)target).ToString("F6"));
        hit_counter++;
    }
}
