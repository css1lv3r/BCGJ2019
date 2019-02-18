using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] BGM;
    private AudioSource source;
    
    private AudioSource effectSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        effectSource = GetComponent<AudioSource>();
        PlayBGM(0);
        Debug.Log("Playing Main Menu BGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySFX(int clipIndex)
    {
        effectSource.PlayOneShot(clips[clipIndex]);
        //Debug.Log("Playing:" + clipIndex);
    }

    public void SwitchSFX(string s)
    {
        switch (s)
        {
            case "AImpact":
                PlaySFX(0);
                break;
            case "Crash":
                PlaySFX(1);
                break;
            case "Refuel":
                PlaySFX(2);
                break;
            case "Sputter":
                PlaySFX(3);
                break;
            case "Rocket":
                PlaySFX(4);
                break;
            case "Take off":
                PlaySFX(5);
                break;
            case "Power up":
                PlaySFX(6);
                break;
            case "Bicker":
                PlaySFX(7);
                break;
            case "Tada":
                PlaySFX(8);
                break;
            case "Uhoh":
                PlaySFX(9);
                break;
            case "Plant Harvest":
                PlaySFX(10);
                break;
            case "Shovel":
                PlaySFX(11);
                break;
            case "Water":
                PlaySFX(12);
                break;
            case "Water Harvest":
                PlaySFX(13);
                break;
        }
    }

    public void SwitchBGM(string s)
    {
        switch (s)
        {
            case "Main Menu":
                PlayBGM(0);
                break;
            case "Theme 1":
                PlayBGM(1);
                break;
            case "Theme 2":
                PlayBGM(2);
                break;
        }
    }

    void PlayBGM(int bgm)
    {
        source.clip = BGM[bgm];
        source.loop = true;
        source.Play();
    }
}
