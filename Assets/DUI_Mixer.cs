using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DUI_Mixer : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Jun_TweenRuntime button;

    public UnityAction finished;
    [SerializeField]
    private Jun_TweenRuntime successAnim;

    public void Click()
    {
        button.Play();
    }

    public void UpdateView(float v)
    {
        slider.value = v;
    }

    public void Success()
    {
        successAnim.gameObject.SetActive(true);
        successAnim.Play();
    }

    public void Finished()
    {
        if (finished != null)
            finished();
    }
}
