using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    private static AppController instance;
    public static AppController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AppController>();
            return instance;
        }
    }

    [SerializeField]
    private GameObject fadeMenu;

    [SerializeField]
    private string mainScene;

    [SerializeField]
    private AudioMixer audioMixer;

    private bool paused = false;
    private bool pausable = false;

    public float VolumeMaster
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume_Master", 1);
        }

        set
        {
            PlayerPrefs.SetFloat("Volume_Master", value);
        }
    }

    public float VolumeUI
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume_UI", 1);
        }

        set
        {
            PlayerPrefs.SetFloat("Volume_UI", value);
        }
    }
    public float VolumeSFX
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume_SFX", 1);
        }

        set
        {
            PlayerPrefs.SetFloat("Volume_SFX", value);
        }
    }
    public float VolumeMusic
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume_Music", 1);
        }

        set
        {
            PlayerPrefs.SetFloat("Volume_Music", value);
        }
    }

    public bool Pausable { get => pausable; set => pausable = value; }

    public void ChangeMasterVolume(float f)
    {
        VolumeMaster = f;
        audioMixer.SetFloat("Volume_Master",f);
    }

    public void ChangeUIVolume(float f)
    {
        VolumeUI = f;
        audioMixer.SetFloat("Volume_UI",f);
    }

    public void ChangeSFXVolume(float f)
    {
        VolumeSFX = f;
        audioMixer.SetFloat("Volume_SFX", f);
    }

    public void ChangeMusicVolume(float f)
    {
        VolumeMusic = f;
        audioMixer.SetFloat("Volume_Music", f);
    }

    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            GUI_Controller.Insatance.ShowPause(true);
        }
    }

    public void Unpause()
    {
        if (paused)
        {
            GUI_Controller.Insatance.ShowPause(false);
            paused = false;
            Time.timeScale = 1f;
        }
    }

    public void LoadGame()
    {
        fadeMenu.SetActive(true);
        fadeMenu.GetComponent<Jun_TweenRuntime>().Play();
        SoundController.Instance.EnterBackery();
    }

    public void EnterBakery()
    {
        SceneManager.LoadScene(mainScene);
        pausable = true;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    private void Start()
    {
        SoundController.Instance.MenuThemeStart();

        ChangeMasterVolume(VolumeMaster);
        ChangeMusicVolume(VolumeMusic);
        ChangeSFXVolume(VolumeSFX);
        ChangeUIVolume(VolumeUI);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausable)
        {
            Pause();
        }
    }
}
