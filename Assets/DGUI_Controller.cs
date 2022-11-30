using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
