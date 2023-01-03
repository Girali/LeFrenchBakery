using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSliderVolume : MonoBehaviour
{
    void Awake()
    {
        Slider slider = GetComponent<Slider>();
        slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(name, 0));
    }
}
