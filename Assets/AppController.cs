using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private string mainScene;

    public float Volume
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume", 1);
        }

        set
        {
            PlayerPrefs.SetFloat("Volume", value);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(mainScene);
    }

    public void ChangeVolume(float f)
    {
        Volume = f;
        AudioListener.volume = f;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
