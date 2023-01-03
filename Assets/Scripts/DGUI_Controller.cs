using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGUI_Controller : MonoBehaviour
{
    private static DGUI_Controller _instance;
    public static DGUI_Controller Insatance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DGUI_Controller>();
            }
            return _instance;
        }
    }

    public GameObject interactIndicator;
    public GameObject interactTimer;

    private Transform toFollowObject = null;
    private Vector3 offsetToFollow = Vector3.zero;

    private void Update()
    {
        if(toFollowObject != null) 
        {
            interactIndicator.transform.position = toFollowObject.transform.position + offsetToFollow;
        }
    }

    public void ShowInidicator(Transform pos, bool b, Vector3 offset = new Vector3())
    {
        interactIndicator.SetActive(b);

        if (pos != null) 
        {
            Collider c = pos.GetComponent<Collider>();
            Vector3 p = c.bounds.center + Vector3.up;

            offsetToFollow = (p - pos.position) + offset;
            toFollowObject = pos;

            interactIndicator.transform.position = pos.position + offsetToFollow;
        }
    }

    public DUI_Timer StartTimer(Transform pos, float cookingTime, float overcookingTime)
    {
        Collider c = pos.GetComponent<Collider>();
        Vector3 p = c.bounds.center + Vector3.up;
        DUI_Timer t = Instantiate(interactTimer, transform).GetComponent<DUI_Timer>();
        t.gameObject.SetActive(true);
        t.transform.position = p;
        t.StartTimer(cookingTime, overcookingTime);
        return t;
    }
}
