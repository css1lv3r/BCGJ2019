using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    const float countdown = 10.0f;
    private float timer = 0.0f;
    public SpriteRenderer sprite;
    private Color shadowA;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        shadowA = sprite.color;
        shadowA.a = 0f;
        sprite.color = shadowA;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("SuitHealth.set", 0);
        }
        
        if (collision.gameObject.tag == "Crater")
        {
            collision.gameObject.SendMessage("resetGrowth");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f && sprite.color.a < 1.0f)
        {
            float t = (timer - 10f) / countdown;
            shadowA.a = (1.0f * t);
            sprite.color = shadowA;
        }

        if (timer >= 18.5f)
        {
            // AudioManager.instance.Play();
        }

        if (timer > 21f)
        {
            shadowA.a = 0f;
            sprite.color = shadowA;
        }
    }
}
