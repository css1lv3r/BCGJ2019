using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    public PlayerColor playerColor;
    public Animator animController;
    public Transform overlapPosition;

    public float SuitHealth
    {
        get { return suitHealth; }
        set
        {
            suitHealth = value;
            if (suitHealth > 100) { suitHealth = 100; }
            if (suitHealth < 0) { suitHealth = 0; }
        }
    }

    //------------------------------

    const float slowSpeed = 10f;
    const float medSpeed = 20f;
    const float normalSpeed =50f;
    //------------------------------
    private float suitHealth;
    private int fuelInventory;
    private int maxInventoryCapacity = 5;


    Vector2 normalizedDirection;

    private List<GameObject> overlappingCraters = new List<GameObject>();
    private GameObject[] allCraters;




    //----------------------------------------------------------------------------------------------------

    void Awake()
    {
        //animController = gameObject.GetComponentInChildren<Animator>();
        allCraters = GameObject.FindGameObjectsWithTag("Crater");
    }

    // Update is called once per frame
    void Update()
    {

        //get movement
        normalizedDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        //Debug.Log(normalizedDirection);

        //get interact input
        if (Input.GetKey(KeyCode.E))
        {
            UpdateOverlappingCraters();
            Debug.Log(overlappingCraters.Count);
        }
        

    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Crater")
    //    {
    //        if (!overlappingCraters.Contains(col.gameObject))
    //        {
    //            overlappingCraters.Add(col.gameObject);
    //        }
            
    //    }
    //}

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Crater")
    //    {
    //        if (overlappingCraters.Contains(col.gameObject))
    //        {
    //            overlappingCraters.Remove(col.gameObject);
    //        }

    //    }
    //}

    void FixedUpdate()
    {
        //move character
        Vector3 adjustedMoveVector = new Vector3(normalizedDirection.x, normalizedDirection.y, 0) * Time.fixedDeltaTime*normalSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = adjustedMoveVector ;
        //rotate character
        float moveDirection = (Mathf.Atan2(-normalizedDirection.x, normalizedDirection.y)*Mathf.Rad2Deg);
        //Debug.Log(moveDirection);

        if (normalizedDirection != Vector2.zero) {
            transform.rotation = Quaternion.Euler(0, 0, moveDirection);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
        

        //update animation
        animController.SetBool("isMoving",normalizedDirection!=Vector2.zero);
        //Debug.Log(animController.GetCurrentAnimatorClipInfo(0));
    }
    //----------------------------------------------------------------------------------------------------

     public bool TryToAddToInventory(int count)
    {
        if ((fuelInventory + count) > maxInventoryCapacity)
        {
            return false;
        }
        else
        {
            ++fuelInventory;
            return true;
        }

    }

    //----------------------------------------------------------------------------------------------------

    private bool IsInCrater()
    {
        return (overlappingCraters.Count > 0);
    }

    private void UpdateOverlappingCraters()
    {
        overlappingCraters.Clear();
        Vector2 overlapPoint = new Vector2(overlapPosition.position.x, overlapPosition.position.y);
        Debug.Log(allCraters.Length);
        foreach(GameObject craterObject in allCraters)
        {
            if (craterObject.GetComponent<Collider2D>().OverlapPoint(overlapPoint))
            {
                overlappingCraters.Add(craterObject);
            }
        }
    }
}

public enum PlayerColor { Red, Blue};
