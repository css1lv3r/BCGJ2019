using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager instance = null;
    public float timer = 0.0f;
    public float countdown = 0.0f;
    public Asteroid asteroid;

    private void Awake()
    {
        if (instance == null && instance != this)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad( gameObject );
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCountDown();
    }

    void SetCountDown()
    {
        float num = Random.Range(10.0f, 15.0f);
        countdown = num;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > countdown)
        {
            asteroid = new Asteroid();
            timer = 0.0f;
            SetCountDown();
        }
    }
}
