using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0,24)] public float TimeOfDay;
    [SerializeField] private float timeCycle;
    private float speed;
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

    private void Awake()
    {
        speed = 12 / timeCycle;
    }

    private void Update() 
    {
        if (TimeOfDay + 6 > 6)
        {
            if (TimeOfDay + 6 <= 8)
            {
                if (period != PeriodOfDay.PetitMatin)
                {
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
