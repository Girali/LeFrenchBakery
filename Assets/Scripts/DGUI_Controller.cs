using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public DUI_InteractIndicator interactIndicator1;
    public DUI_InteractIndicator interactIndicator2;
    public Transform timerParent;

    public void ShowInidicator(PlayerController.PlayerIndex p, Transform pos, bool b, string t, Sprite s, Vector3 offset = new Vector3())
    {
        if (p == PlayerController.PlayerIndex.P1)
            interactIndicator1.ShowInidicator(pos, b, t, s, offset);
        else
            interactIndicator2.ShowInidicator(pos, b, t, s, offset);
    }

    public void ShowInidicator(PlayerController.PlayerIndex p, Transform pos, bool b, string t, Vector3 offset = new Vector3())
    {
        if (p == PlayerController.PlayerIndex.P1)
            interactIndicator1.ShowInidicator(pos, b, t, offset);
        else
            interactIndicator2.ShowInidicator(pos, b, t, offset);
    }
    
    public void ShowInidicator(PlayerController.PlayerIndex p, bool b)
    {
        if (p == PlayerController.PlayerIndex.P1)
            interactIndicator1.ShowInidicator(b);
        else                                        
            interactIndicator2.ShowInidicator(b);
    }


    public GameObject interactTimer;
    public GameObject interactMixer;

    public DUI_Timer StartTimer(Transform pos, float cookingTime, float overcookingTime)
    {
        Collider c = pos.GetComponent<Collider>();
        Vector3 p = c.bounds.center + Vector3.up;
        DUI_Timer t = Instantiate(interactTimer, timerParent).GetComponent<DUI_Timer>();
        t.gameObject.SetActive(true);
        t.transform.position = p;
        t.StartTimer(cookingTime, overcookingTime);
        return t;
    }

    public DUI_Mixer StartMixer(Transform pos)
    {
        Collider c = pos.GetComponent<Collider>();
        Vector3 p = c.bounds.center + Vector3.up;
        DUI_Mixer t = Instantiate(interactMixer, transform).GetComponent<DUI_Mixer>();
        t.gameObject.SetActive(true);
        t.transform.position = p;
        return t;
    }
}
