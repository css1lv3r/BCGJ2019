using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static SceneManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
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
