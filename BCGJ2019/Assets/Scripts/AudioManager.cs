using System.Collections;
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
    private float volHighRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playBGM()
    {
        source.Play();
    }
}
