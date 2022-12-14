using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Mixer : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject right;
    public UnityAction finished;
    [SerializeField]
    private Jun_TweenRuntime successAnim;

    public void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    public void Success()
    {
        successAnim.gameObject.SetActive(true);
        successAnim.Play();
    }

    public void UpdateView(float v, bool isLeft)
    {
        slider.value = v;

        left.SetActive(isLeft);
        right.SetActive(!isLeft);
    }

    public void Finished()
    {
        if(finished != null)
            finished();
    }
}
