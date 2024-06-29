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
    public static AudioClip lose;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        buttonClick = LoadAudioClip("Sound/click");
        pickup = LoadAudioClip("Sound/pickup");
        complete = LoadAudioClip("Sound/complete");
        alert = LoadAudioClip("Sound/alert");
        lose = LoadAudioClip("Sound/lose");
    }

    public void ButtonClick()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            audioSource.clip = buttonClick;
            audioSource.Play();
        }
    }

    public static void PickUp()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            audioSource.clip = pickup;
            audioSource.Play();
        }
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

    public static void Mistake()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            audioSource.clip = alert;
            audioSource.Play();
        }
    }

    public static void Losing()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            audioSource.clip = lose;
            audioSource.Play();
        }
    }

    AudioClip LoadAudioClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
