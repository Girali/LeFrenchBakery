using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Temps : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    public Transform pivot;
    public LightingManager lightingManager;
    public float rotationStart;
    public float rotationEnd;

    public Sprite open;
    public Sprite closed;

    public float rotationSpeed;
    private bool isOpen;

    private void Awake()
    {
        lightingManager.onTimePeriodChange += OnDayTimeChange;
    }

    public void OnDayTimeChange(LightingManager.PeriodOfDay periodOfDay)
    {
        if (isOpen == false && periodOfDay == LightingManager.PeriodOfDay.Matin)
        {
            isOpen = true;
            GetComponent<Image>().sprite = open;
        }
        else if (isOpen == true && periodOfDay == LightingManager.PeriodOfDay.Nuit)
        {
            isOpen = false;
            GetComponent<Image>().sprite = closed;
        }
    }
    
    private void Update()
    {
        float timepercent = lightingManager.TimePercent;
        fill.color = gradient.Evaluate(timepercent);
        pivot.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(rotationStart, rotationEnd, timepercent)));
    }
}
