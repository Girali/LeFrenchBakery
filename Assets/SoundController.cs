using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{

    private static SoundController instance;
    public static SoundController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoundController>();
            return instance;
        }
    }

    [Space(20)]
    [Header("MIXER GROUPS")]
    [SerializeField]
    private AudioMixerGroup uiGroup;
    [SerializeField]
    private AudioMixerGroup sfxGroup;
    [SerializeField]
    private AudioMixerGroup musicGroup;

    [Space(20)]
    [Header("UI SOUNDS")]
    [SerializeField]
    private AudioClip hover;
    [SerializeField]
    private AudioClip close;
    [SerializeField]
    private AudioClip open;

    [Space(20)]
    [Header("EFFECT SOUNDS")]
    [SerializeField]
    private AudioClip buyItem;
    [SerializeField]
    private AudioClip sellProduct;
    [SerializeField]
    private AudioClip dingFournace;
    [SerializeField]
    private AudioClip fire;
    [SerializeField]
    private AudioClip interact;
    [SerializeField]
    private AudioClip mix;
    [SerializeField]
    private AudioClip putIngredient;
    [SerializeField]
    private AudioClip success;
    [SerializeField]
    private AudioClip failed;

    [Space(20)]
    [Header("MUSIC SOUNDS")]
    [SerializeField]
    private AudioClip bakeryTheme;
    [SerializeField]
    private AudioClip menuTheme;
    [SerializeField]
    private AudioClip bakeryEnter;

    private AudioSource mainMusicSource;

    private GameObject sourceUI;
    private GameObject sourceEffect;
    private GameObject sourceMusic;
    private List<AudioSource> uiAudioSources = new List<AudioSource>();
    private List<AudioSource> effectAudioSources = new List<AudioSource>();
    private List<AudioSource> musicAudioSources = new List<AudioSource>();

    private void Awake()
    {
        GameObject c = Camera.main.gameObject;
        sourceUI = new GameObject("AudioUISource");
        sourceUI.transform.parent = c.transform;
        sourceUI.transform.localPosition = Vector3.zero;

        sourceEffect = new GameObject("AudioEffectSource");
        sourceEffect.transform.parent = c.transform;
        sourceEffect.transform.localPosition = Vector3.zero;

        sourceMusic = new GameObject("AudioMusicSource");
        sourceMusic.transform.parent = c.transform;
        sourceMusic.transform.localPosition = Vector3.zero;
    }

    #region Utility
    private IEnumerator CRT_FadeIn(AudioSource audioSource, float time)
    {
        float t = 0;
        float inter = (1f / 60f) * time;

        while (t < time)
        {
            t += inter;
            audioSource.volume = t / time;
            yield return new WaitForEndOfFrame();
        }

        audioSource.volume = 1f;
    }

    private IEnumerator CRT_FadeOut(AudioSource audioSource, float time)
    {
        float t = time;
        float inter = (1f / 60f) * time;

        while (t > 0)
        {
            t -= inter;
            audioSource.volume = t / time;
            yield return new WaitForEndOfFrame();
        }

        audioSource.volume = 0f;
    }

    private AudioSource FindFreeUISource()
    {
        foreach (AudioSource s in uiAudioSources)
        {
            if (!s.isPlaying)
                return s;
        }

        AudioSource audioSource = sourceUI.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = uiGroup;
        uiAudioSources.Add(audioSource);
        audioSource.volume = 1f;
        return audioSource;
    }
    private AudioSource FindFreeEffectSource()
    {
        foreach (AudioSource s in effectAudioSources)
        {
            if (!s.isPlaying)
                return s;
        }

        AudioSource audioSource = sourceEffect.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxGroup;
        effectAudioSources.Add(audioSource);
        return audioSource;
    }
    private AudioSource FindFreeMusicSource()
    {
        foreach (AudioSource s in musicAudioSources)
        {
            if (!s.isPlaying)
                return s;
        }

        AudioSource audioSource = sourceMusic.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = musicGroup;
        musicAudioSources.Add(audioSource);
        return audioSource;
    }
    #endregion

    #region UI
    public void Hover()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(hover);
    }

    public void OpenUI()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.PlayOneShot(open);
    }

    public void CloseUI()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.PlayOneShot(close);
    }
    #endregion

    #region Music
    public void MenuThemeStart()
    {
        AudioSource audioSource = FindFreeMusicSource();
        audioSource.clip = menuTheme;
        audioSource.loop = true;
        audioSource.Play();
        mainMusicSource = audioSource;
        StartCoroutine(CRT_FadeIn(audioSource, 4f));
    }

    public void BakeryThemeStart()
    {
        AudioSource audioSource = FindFreeMusicSource();
        audioSource.clip = bakeryTheme;
        audioSource.loop = true;
        audioSource.Play();
        mainMusicSource = audioSource;
        StartCoroutine(CRT_FadeIn(audioSource, 4f));
    }

    public void EnterBackery()
    {
        AudioSource audioSource = FindFreeMusicSource();
        audioSource.PlayOneShot(bakeryEnter);
        StartCoroutine(CRT_FadeOut(mainMusicSource, 2f));
    }
    #endregion

    #region SFX
    public void BuyItem()
    {
        AudioSource audioSource = FindFreeEffectSource();
        audioSource.PlayOneShot(buyItem);
    }

    public void Sell()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(sellProduct);
    }

    public void Ding()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(dingFournace);
    }

    public void Fire()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(fire);
    }

    public void Interact()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(interact);
    }

    public void Mix()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(mix);
    }

    public void PutIngredient()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(putIngredient);
    }

    public void Success()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(success);
    }

    public void Failed()
    {
        AudioSource audioSource = FindFreeUISource();
        audioSource.PlayOneShot(failed);
    }
    #endregion
}
