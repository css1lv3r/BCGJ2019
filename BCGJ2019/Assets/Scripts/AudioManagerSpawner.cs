using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSpawner : MonoBehaviour
{
    public static AudioManager instance = null;
    public GameObject AudioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = GameObject.Instantiate(AudioManager, transform.position, Quaternion.identity).GetComponent<AudioManager>();
        }
        DontDestroyOnLoad(instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
