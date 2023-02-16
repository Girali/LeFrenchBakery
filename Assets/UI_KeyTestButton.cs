using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyTestButton : MonoBehaviour
{
    public KeyCode key;
    public Color colorDisabled;
    public Color colorEnabled;
    public Graphic graphic;

    [ContextMenu("Awake")]
    private void Awake()
    {
        graphic.CrossFadeColor(colorDisabled, 0, true, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            graphic.CrossFadeColor(colorEnabled, 0, true, true);
            SoundController.Instance.Hover();
        }
        else if (Input.GetKeyUp(key))
        {
            graphic.CrossFadeColor(colorDisabled, 0, true, true);
        }
    }
}
