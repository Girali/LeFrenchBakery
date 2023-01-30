using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;
    [SerializeField]
    private LightingPreset preset;
    [SerializeField, Range(0, 24)]
    private float timeOfDay;
    [SerializeField]
    private float timeCycle;
    private float speed;
    [SerializeField]
    private float timeOffset;
    [SerializeField]
    private float timeOffsetPercent;
    [SerializeField]
    private float rotationOffset;
    private float timePercent;
    [SerializeField]
    private PeriodOfDay period = PeriodOfDay.Minuit;

    [SerializeField]
    private Vector2 rotationLimit;

    public UnityAction<PeriodOfDay> onTimePeriodChange;
    public enum PeriodOfDay
    {
        PetitMatin,
        Matin,
        Midi,
        Aprem,
        Soir,
        Nuit,
        Minuit
    }

    public float TimeOfDay
    {
        get => timeOfDay;
    }
    public float TimePercent { get => timePercent; }

    public void ResetDay()
    {
        timeOfDay = timeOffset;
    }

    private void Awake()
    {
        timeOfDay = timeOffset;
        speed = 12 / timeCycle;
    }

    private void Update()
    {
        if (timeOfDay > 0 && timeOfDay <= 2)
        {
            if (period != PeriodOfDay.Minuit)
            {
                period = PeriodOfDay.Minuit;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (timeOfDay > 2 && timeOfDay <= 6)
        {
            if (period != PeriodOfDay.PetitMatin)
            {
                period = PeriodOfDay.PetitMatin;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (timeOfDay > 6 && timeOfDay <= 12)
        {
            if (period != PeriodOfDay.Matin)
            {
                period = PeriodOfDay.Matin;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (timeOfDay > 12 && timeOfDay <= 14)
        {
            if (period != PeriodOfDay.Midi)
            {
                period = PeriodOfDay.Midi;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (timeOfDay > 14 && timeOfDay <= 16)
        {
            if (period != PeriodOfDay.Aprem)
            {
                period = PeriodOfDay.Aprem;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (timeOfDay > 16 && timeOfDay <= 19)
        {
            if (period != PeriodOfDay.Soir)
            {
                period = PeriodOfDay.Soir;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (timeOfDay > 19 && timeOfDay <= 24)
        {
            if (period != PeriodOfDay.Nuit)
            {
                period = PeriodOfDay.Nuit;
                if (onTimePeriodChange != null)
                    onTimePeriodChange(period);
            }
        }

        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * speed;
            timeOfDay %= 24;
            timePercent = ((timeOfDay + timeOffsetPercent) % 24) / 24f;
            UpdateLighting(timePercent);
        }
        else
        {
            timePercent = ((timeOfDay + timeOffsetPercent) % 24) / 24f;
            UpdateLighting(timePercent);
        }
    }
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
        if (directionalLight != null)
        {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Lerp(rotationLimit.x, rotationLimit.y, timePercent) - rotationOffset, 170, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
