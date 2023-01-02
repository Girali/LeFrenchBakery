using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ButtonSounds : MonoBehaviour , IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_SoundManager.Instance.Hover();
    }
}
