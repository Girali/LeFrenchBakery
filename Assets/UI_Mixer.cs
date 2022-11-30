using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Mixer : MonoBehaviour
{
    public Slider slider;
    public GameObject left;
    public GameObject right;

    public void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    public void UpdateView(float v, bool isLeft)
    {
        slider.value = v;

        left.SetActive(isLeft);
        right.SetActive(!isLeft);
    }
}
