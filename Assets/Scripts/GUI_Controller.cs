using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUI_Controller : MonoBehaviour
{
    private static GUI_Controller _instance;
    public static GUI_Controller Insatance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GUI_Controller>();
            }
            return _instance;
        }
    }
    
    [SerializeField]
    private Jun_TweenRuntime notifyAnim;
    [SerializeField]
    private TMP_Text notifyText;

    [SerializeField]
    private UI_Pause pause;

    public UI_DaySummery daySummery;

    public void ShowPause(bool b)
    {
        pause.gameObject.SetActive(b);
    }

    public void Notify(string t)
    {
        notifyAnim.Play();
        notifyText.text = t;
    }
}
