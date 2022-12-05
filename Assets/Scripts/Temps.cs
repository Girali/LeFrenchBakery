using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        float timepercent = lightingManager.TimeOfDay/13f;
        fill.color = gradient.Evaluate(timepercent);
        pivot.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(rotationStart, rotationEnd, timepercent)));
    }
}
