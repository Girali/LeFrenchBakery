using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DUI_Client : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private Gradient g;

    [SerializeField]
    private GameObject happy;
    [SerializeField]
    private GameObject sad;

    public void ShowIcon(bool b, Article a)
    {
        gameObject.SetActive(b);
        icon.sprite = a.sprite;
    }

    public void UpdateView(float f)
    {
        fill.fillAmount = f;
        fill.color = g.Evaluate(f);
    }

    public void OnSuccess()
    {
        happy.SetActive(true);
    }

    IEnumerator CRT_Disable()
    {
        yield return new WaitForSeconds(4f);
        happy.SetActive(false);
        sad.SetActive(false);
    }

    public void OnFail()
    {
        sad.SetActive(true);
    }
}
