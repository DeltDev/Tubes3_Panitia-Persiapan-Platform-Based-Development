using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] SoundList;
    private AudioSource UniversalAudioPlayer;
    void Awake()
    {
        foreach(Sound s in SoundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.sound;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    void Start()
    {
        PlaySound("BGMTheme");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(SoundList, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("suara tidak ada");
            return;
        } else
        {
            Debug.Log("Suara ada");
        }
        s.source.Play();
    }
}
