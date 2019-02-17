using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    public PlayerColor playerColor;

    const float normalSpeed = 100f;
    const float buttonSpeed = 70f;

    Vector2 normalizedDirection;

    Vector3 initialPosition;
        
    void Awake()
    {
        initialPosition = transform.position;
    }
    
    void Update()
    {
        normalizedDirection = new Vector2(Input.GetAxis("Horizontal_" + (int)playerColor), Input.GetAxis("Vertical_" + (int)playerColor)).normalized;
    }


    void FixedUpdate()
    {
        //move character
        Vector3 adjustedMoveVector = new Vector3(normalizedDirection.x, normalizedDirection.y, 0) * Time.fixedDeltaTime * normalSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = adjustedMoveVector;
    }

    public void Reset()
    {
        gameObject.transform.position = initialPosition;
    }
}
