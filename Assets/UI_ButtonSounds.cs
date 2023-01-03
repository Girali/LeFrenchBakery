using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ButtonSounds : MonoBehaviour , IPointerEnterHandler , IPointerDownHandler
{
    private Selectable selectable;
    public Type soundType;

    public enum Type
    {
        Hover,
        Exit,
        Open
    }

    private void Start()
    {
        selectable = GetComponent<Selectable>();
        if (soundType == Type.Open)
            SoundController.Instance.OpenUI();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (soundType == Type.Exit && selectable.interactable)
            SoundController.Instance.CloseUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (soundType == Type.Hover && selectable.interactable)
            SoundController.Instance.Hover();
    }
}
