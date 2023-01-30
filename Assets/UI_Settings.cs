using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class UI_Settings : MonoBehaviour
{
    public void ChangeMasterVolume(float f)
    {
        AppController.Instance.ChangeMasterVolume(f);
    }

    public void ChangeUIVolume(float f)
    {
        AppController.Instance.ChangeUIVolume(f);
    }

    public void ChangeSFXVolume(float f)
    {
        AppController.Instance.ChangeSFXVolume(f);
    }

    public void ChangeMusicVolume(float f)
    {
        AppController.Instance.ChangeMusicVolume(f);
    }

}
