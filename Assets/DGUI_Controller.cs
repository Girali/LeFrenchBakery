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

    public void ShowInidicator(Transform pos, bool b)
    {
        interactIndicator.SetActive(b);

        if (pos != null) 
        {
            Collider c = pos.GetComponent<Collider>();
            Vector3 p = c.bounds.center + Vector3.up;

            interactIndicator.transform.position = p;
        }
    }
}
