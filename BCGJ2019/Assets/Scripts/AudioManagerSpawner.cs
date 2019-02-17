using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSpawner : MonoBehaviour
{
    private static AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = new AudioManager();
        }
        DontDestroyOnLoad(instance);
    }

    public static AudioManager GetInstance()
    {
        return instance;
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
