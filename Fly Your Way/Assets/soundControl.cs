using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundControl : MonoBehaviour
{
    //private AudioSource flySound;
    //private AudioSource pointsSound;
    //private AudioSource dieSound;

    //public float volume;

    //void Start()
    //{
    //    flySound = GetComponent<AudioSource>();
    //    pointsSound = GetComponent<AudioSource>();
    //    dieSound = GetComponent<AudioSource>();
    //}

    //void Update()
    //{
    //    flySound.volume = volume;
    //    pointsSound.volume = volume - 0.6f;
    //    dieSound.volume = volume - 0.2f;

    //}
    //public void SetVolume(float vol)
    //{
    //    volume = vol;
    //}

    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
    }


}
