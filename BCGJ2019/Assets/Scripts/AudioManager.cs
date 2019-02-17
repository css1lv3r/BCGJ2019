﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*boolean fields for representing different critical game events: <-- not sure about this
     Audio Source
     Volume
    */
    public AudioClip BGM;

    private AudioSource source;
    // private float volHighRange = 1.0f;
    
    private AudioSource effectSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        effectSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect(int clipIndex)
    {
        effectSource.clip = clips[clipIndex];
        if (!effectSource.isPlaying)
        {
            effectSource.Play();
        }
    }

    void playBGM()
    {
        source.Play();
    }
}
