using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    const float countdown = 10.0f;
    private float timer = 0.0f;
    public SpriteRenderer sprite;
    private Color shadowA;
    private const int impact_index = 0;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        shadowA = sprite.color;
        shadowA.a = 0f;
        sprite.color = shadowA;
    }

    private void HasHit()
    {
        Collider2D[] hit;
        Vector2  pos = sprite.transform.position;
        hit = Physics2D.OverlapPointAll(pos);
        int numHit = hit.Length;
        for(int i = 0; i < numHit; i++)
        {
            if (hit[i].tag == "Player")
            {
                hit[i].SendMessage("Injure");
                Debug.Log("Hit player");
            }

            if (hit[i].tag == "Crater")
            {
                hit[i].SendMessage("resetGrowth");
                Debug.Log("Hit crater");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // using timer to fade in the shadow
        if (timer > 10f && sprite.color.a < 1.0f)
        {
            float t = (timer - 10f) / countdown;
            shadowA.a = (1.0f * t);
            sprite.color = shadowA;
        }

        // start playing the sound effect roughly 1.5s before impact
        // leaving a margin on error
        if (timer >= 18.5f && timer < 18.6f)
        {
            AudioManagerSpawner.instance.SwitchSFX("AImpact");
        }

        // check if the asteroid has hit anything
        if (timer >= 19.9f && timer < 20f)
        {
            HasHit();
        }

        // Get rid of asteroid
        if (timer > 20.2f)
        {
            shadowA.a = 0f;
            sprite.color = shadowA;
            Debug.Log("Asteroid hit");
            Destroy(gameObject);
        }
    }
}
