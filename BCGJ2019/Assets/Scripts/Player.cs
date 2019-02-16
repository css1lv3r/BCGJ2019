using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    public PlayerColor playerColor;

    public Animator animController;

    const float slowSpeed = 10f;
    const float medSpeed = 20f;
    const float normalSpeed =50f;

    private float suitHealth;
    private int fuelInventory;



    Vector2 normalizedDirection;


    //----------------------------------------------------------------------------------------------------
    // Start is called before the first frame update


    void Start()
    {
        //animController = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        normalizedDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Debug.Log(normalizedDirection);
        

    }

    void OnTriggerEnter2D(Collider2D)
    {

    }

    void FixedUpdate()
    {
        //move character
        Vector3 adjustedMoveVector = new Vector3(normalizedDirection.x, normalizedDirection.y, 0) * Time.fixedDeltaTime*normalSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = adjustedMoveVector ;
        //rotate character
        float moveDirection = (Mathf.Atan2(-normalizedDirection.x, normalizedDirection.y)*Mathf.Rad2Deg);
        Debug.Log(moveDirection);

        transform.rotation = Quaternion.Euler(0,0,moveDirection);

        //update animation
        animController.SetBool("isMoving",normalizedDirection!=Vector2.zero);
        //Debug.Log(animController.GetCurrentAnimatorClipInfo(0));
    }


    //----------------------------------------------------------------------------------------------------
}

public enum PlayerColor { Red, Blue};
