using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTester : MonoBehaviour 
{
    SphereDeviationRaycaster sdr;
    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject aim_hint;
    [SerializeField]
    Text accuracy;
    [SerializeField]
    GameObject hitSphere;
    
    
	// Use this for initialization
    Vector3 standard_aim_scale;
    void Start()
    {
        sdr = Camera.main.gameObject.GetComponent<SphereDeviationRaycaster>();
        standard_aim_scale = aim_hint.transform.localScale;
        OnSliderChanged(0.03f);
    }

    public void OnButtonFireTargetClicked() 
    {
        Vector3 start = Camera.main.transform.position;
        RaycastHit hit = sdr.DeviationRaycastOnHitPoint(Camera.main.transform.position, target.position);

        if (hit.collider != null) 
        {
            hit.collider.gameObject.SendMessage("OnHit", hit.point, SendMessageOptions.DontRequireReceiver);

            GameObject hit_s = Instantiate(hitSphere) as GameObject;
            hit_s.transform.position = hit.point;
            Destroy(hit_s, 2);
        }
    }

    public void OnSliderChanged( float value ) 
    {
        aim_hint.transform.localScale = standard_aim_scale * value * 2 * (target.position - Camera.main.transform.position).magnitude;
        sdr.DeviationPerUnit = 0.001f + value;
        if (sdr.DeviationPerUnit > 0.998f)
            sdr.DeviationPerUnit = 0.998f;

        accuracy.text = "Deviation: " + sdr.DeviationPerUnit;
    }
}
