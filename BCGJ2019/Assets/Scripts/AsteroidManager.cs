using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager instance = null;
    public float timer = 0.0f;
    public float countdown = 0.0f;
    public GameObject asteroid;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCountDown();
    }

    void SetCountDown()
    {
        float num = 20.0f; // Random.Range(10.0f, 15.0f); commenting out for testing
        countdown = num;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > countdown)
        {
            float xPos = Random.Range(-2.5f, 2.5f);
            float yPos = Random.Range(-2.5f, 2.5f);
            GameObject.Instantiate(asteroid,new Vector3(xPos,yPos,0), Quaternion.identity);
            timer = 0.0f;
            SetCountDown();
        }
    }
}
