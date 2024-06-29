using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip buttonClick;
    public static AudioClip pickup;
    public static AudioClip complete;
    public static AudioClip alert;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        buttonClick = LoadAudioClip("Sound/click");
        pickup = LoadAudioClip("Sound/pickup");
        complete = LoadAudioClip("Sound/complete");
        alert = LoadAudioClip("Sound/alert");
    }

    public void ButtonClick()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
    }

    public static void PickUp()
    {
        audioSource.clip = pickup;
        audioSource.Play();
    }

    public static void Complete()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            audioSource.clip = complete;
            audioSource.Play();
            UIManager.flag = false;
        }
    }

    AudioClip LoadAudioClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
