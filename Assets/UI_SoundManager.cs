using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoundManager : MonoBehaviour
{
    private static UI_SoundManager instance;
    public static UI_SoundManager Instance
    {
        get 
        { 
            if(instance == null)
                instance = FindObjectOfType<UI_SoundManager>();
            return instance; 
        }
    }

    [SerializeField]
    private AudioClip hover;

    private GameObject source;
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        GameObject c = Camera.main.gameObject;
        source = new GameObject("AudioUISource");
        source.transform.parent = c.transform;
        source.transform.localPosition = Vector3.zero;
    }

    private AudioSource FindFreeSource()
    {
        foreach (AudioSource s in audioSources)
        {
            if (!s.isPlaying)
                return s;
        }

        AudioSource audioSource = source.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
        return audioSource;
    }

    public void Hover()
    {
        AudioSource audioSource = FindFreeSource();
        audioSource.pitch = Random.Range(0.9f, 1.0f);
        audioSource.PlayOneShot(hover);
    }
}
