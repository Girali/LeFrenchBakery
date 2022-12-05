using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0,24)] public float TimeOfDay;
    [SerializeField] private float speed;
    public float timeOffset;
    public PeriodOfDay period;
    public enum PeriodOfDay
    {
        PetitMatin,
        Matin,
        Midi,
        Aprem,
        Soir,
        Nuit
    }

    public UnityAction<PeriodOfDay> onTimePeriodChange;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update() 
    {
        if (TimeOfDay + 6 > 6)
        {
            if (TimeOfDay + 6 <= 8)
            {
                if (period != PeriodOfDay.PetitMatin)
                {
                    GUI_Controller.Insatance.Notify("Petit Matin");
                    period = PeriodOfDay.PetitMatin;
                    if(onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }

        if (TimeOfDay + 6 > 8)
        {
            if (TimeOfDay + 6 <= 12)
            {
                if (period != PeriodOfDay.Matin)
                {
                    GUI_Controller.Insatance.Notify("Matin");
                    period = PeriodOfDay.Matin;
                    if (onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }

        if (TimeOfDay + 6 > 12)
        {
            if (TimeOfDay + 6 <= 14)
            {
                if (period != PeriodOfDay.Midi)
                {
                    GUI_Controller.Insatance.Notify("Midi");
                    period = PeriodOfDay.Midi;
                    if (onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }

        if (TimeOfDay + 6 > 14)
        {
            if (TimeOfDay + 6 <= 16)
            {
                if (period != PeriodOfDay.Aprem)
                {
                    GUI_Controller.Insatance.Notify("Apres-Midi ");
                    period = PeriodOfDay.Aprem;
                    if (onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }

        if (TimeOfDay + 6 > 16)
        {
            if (TimeOfDay + 6 <= 19)
            {
                if (period != PeriodOfDay.Soir)
                {
                    GUI_Controller.Insatance.Notify("Soir");
                    period = PeriodOfDay.Soir;
                    if (onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }

        if (TimeOfDay + 6 > 19)
        {
            if (TimeOfDay + 6 <= 24)
            {
                if (period != PeriodOfDay.Nuit)
                {
                    GUI_Controller.Insatance.Notify("Nuit");
                    period = PeriodOfDay.Nuit;
                    if (onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }
        if (TimeOfDay + 6 > 0)
        {
            if (TimeOfDay + 6 <= 6)
            {
                if (period != PeriodOfDay.Nuit)
                {
                    GUI_Controller.Insatance.Notify("Nuit");
                    period = PeriodOfDay.Nuit;
                    if (onTimePeriodChange != null)
                        onTimePeriodChange(period);
                }
            }
        }

        if (Preset == null)
            return;
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime*speed;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        if (DirectionalLight!=null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - timeOffset, 170, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
