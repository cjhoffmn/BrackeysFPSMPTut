﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager instance;
    //private GameObject Player;
    
	// Use this for initialization
	void Awake () {

            if (instance != null)
            {
                Debug.Log("more than one audio manager in scene");
            }
            else
            {
                instance = this;
            }

        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            //s.source.name = "AS" + s.name;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spacial;
            s.source.playOnAwake = s.plyAwake;
            s.source.outputAudioMixerGroup = s.outputToGroup;
        }

	}

    private void Start()
    {
        //Player = GameObject.Find("Player 2");
        Play("sndAtmosphere");
        
    }

    // Update is called once per frame
    public void Play(string name,  GameObject _sndSource = null, float _plytime = 300f)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if(_sndSource != null)
        {
            s.source = _sndSource.AddComponent<AudioSource>();
            
            //insert line to make sure the ASrc is named so I can destroy if its not needed anymore.
            SetupSound(s);
        }

        s.source.Play();

        if (s.source != null)
        {
            Destroy(s.source, _plytime);
        }
    }
    
    private void SetupSound(Sound s)
    {
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spacial;
            s.source.playOnAwake = s.plyAwake;
            s.source.outputAudioMixerGroup = s.outputToGroup;
    }
}
