using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroid;
    private int xPos;
    private int yPos;
    const float countdown = 10.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        xPos = Random.Range(0, 1920);
        yPos = Random.Range(0, 1080);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("Damage");
        }
        
        if (collision.gameObject.tag == "Crater")
        {
            collision.gameObject.SendMessage("SetPlant", false);
        }
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if ((countdown - timer) <= 1.5f)
        {
            // play falling sound clip
            // AudioManager.instance.Play();
        }
        if ((countdown - timer) <= 0.0f)
        {
            // Asteroid collides at point (xPos, yPos) 
            Instantiate(asteroid, new Vector3(xPos, yPos), Quaternion.identity);
        }

    }
}
