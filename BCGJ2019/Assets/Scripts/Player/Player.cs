using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.Animations;

public class Player : MonoBehaviour
{
    public PlayerColor playerColor;
    public Animator animController;
    public Transform overlapPosition;
    public GameObject ship;

    public Vector3 offscreenPosition = new Vector3(-100,0,0);
    public Vector3 lastPosition;
    public int launchTime = 3;

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

    public int FuelInventory
    {
        get { return fuelInventory; }
        set
        {
            fuelInventory = value;
            if (fuelInventory > maxInventoryCapacity) { fuelInventory = maxInventoryCapacity; }
            if (fuelInventory < 0) { fuelInventory = 0; }
        }
    }

    public float ShipFuelInventory
    {
        get { return shipFuelInventory; }
        set
        {
            shipFuelInventory = value;
            if (shipFuelInventory > 100) { ShipFuelInventory = 100; }
            if (shipFuelInventory < 0) { ShipFuelInventory = 0; }
        }
    }

    public bool IsTryingLaunch
    {
        get
        {
            return launchTimerStarted;
        }
    }

    //------------------------------

    const float slowSpeed = 20f;
    const float medSpeed = 40f;
    const float normalSpeed =70f;

    const float lowHealthThreshold = 30;
    const float highFuelThreshold = 95;

    const float suitDrainSpeed = 1f;
    const float fuelDrainSpeed = 0.05f;
    const float suitRechargeSpeed = 10f;
    //------------------------------
    private float suitHealth = 100f;
    private int fuelInventory;
    private float shipFuelInventory = 20;
    private int maxInventoryCapacity = 5;
    private bool isInShip = false;
    private bool tryingToEnterShip = false;

    private float launchTimer;
    private bool launchTimerStarted = false;



    Vector2 normalizedDirection;

    private List<GameObject> overlappingCraters = new List<GameObject>();
    private GameObject[] allCraters;




    //----------------------------------------------------------------------------------------------------

    void Awake()
    {
        //animController = gameObject.GetComponentInChildren<Animator>();
        allCraters = GameObject.FindGameObjectsWithTag("Crater");

    }
    void Start()
    {
        UpdatePlayerValuesInUI();
    }

    // Update is called once per frame
    void Update()
    {
        DrainHealth();
        DrainShip();

        if (checkNearShip() || isInShip)
        {
            TryRechargeSuit();
        }
        //get movement
        normalizedDirection = new Vector2(Input.GetAxis("Horizontal_"+(int)playerColor), Input.GetAxis("Vertical_"+(int)playerColor)).normalized;
        //Debug.Log(normalizedDirection);

        //get interact input
        if (Input.GetButtonDown("Interact_" + (int)playerColor))
        {
            
            UpdateOverlappingCraters();
            TryToInteract();


        }
        else if (Input.GetButton("Interact_" + (int)playerColor)) //if they're holding and not letting go
        {

            CountDownToLaunch();
        }else if (Input.GetButtonUp("Interact_" + (int)playerColor)) //if button released
        {
            if (isInShip)
            {
                TryStopLaunch();
            }
            if (tryingToEnterShip)
            {
                EnterShip();
            }
            
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
        Vector3 adjustedMoveVector = new Vector3(normalizedDirection.x, normalizedDirection.y, 0) * Time.fixedDeltaTime*getCurrentSpeed();
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

    public void TryToInteract()
    {
        if (suitHealth > 0) {
            //Try to interact with ship
            TryToInteractWithCraters();
            if (ship.GetComponent<Collider2D>().OverlapPoint(overlapPosition.position))
            {
                TryOffloadFuel();
            }
            TryToInteractWithShip();
        }
        

        //Update UI
        UpdatePlayerValuesInUI();
        // Play Audio
        // ????
        //Try to interact with ship

    }

    public void Injure()
    {
        if (SuitHealth > lowHealthThreshold)
        {
            SuitHealth = lowHealthThreshold;
        }
    }

    //----------------------------------------------------------------------------------------------------

    private void TryToInteractWithCraters()
    {
        //Try to intereact with craters
        bool planted = false;
        bool harvested = false;
        foreach (GameObject crater in overlappingCraters)
        {
            bool succeededPlant = crater.GetComponent<Crater>().TryToPlant((playerColor == PlayerColor.Red));
            planted = succeededPlant || planted;
            if (planted && crater.GetComponent<Crater>().isDry)
                AudioManagerSpawner.instance.SwitchSFX("Shovel");
            if (planted && !crater.GetComponent<Crater>().isDry)
                AudioManagerSpawner.instance.SwitchSFX("Water");

            int harvestResult = (int)crater.GetComponent<Crater>().TryToHarvest(maxInventoryCapacity - fuelInventory);
            fuelInventory += harvestResult;
            harvested = harvested || (harvestResult > 0);
            
            if (harvested && crater.GetComponent<Crater>().isDry)
                AudioManagerSpawner.instance.SwitchSFX("Plant Harvest");
            if (harvested && !crater.GetComponent<Crater>().isDry)
                AudioManagerSpawner.instance.SwitchSFX("Water Harvest");
        }
        Debug.Log("Planted: " + planted + "\nHarvested: " + harvested);
    }

    private void TryToInteractWithShip()
    {
        if (!(isInShip || tryingToEnterShip)){
            TryEnterShip();
        }
        

    }
       

    private void DrainHealth()
    {
        SuitHealth -= suitDrainSpeed * (Time.deltaTime);
        LevelManager.Instance.UpdatePlayerHealth(playerColor, SuitHealth);
        //Debug.Log(suitHealth);
    }

    private void DrainShip()
    {
        ShipFuelInventory -= fuelDrainSpeed*Time.deltaTime;
        LevelManager.Instance.UpdateShipFuel(playerColor, ShipFuelInventory);
    }

    private bool IsInCrater()
    {
        return (overlappingCraters.Count > 0);
    }

    private void UpdateOverlappingCraters()
    {
        overlappingCraters.Clear();
        Vector2 overlapPoint = new Vector2(overlapPosition.position.x, overlapPosition.position.y);
        //Debug.Log(allCraters.Length);
        foreach(GameObject craterObject in allCraters)
        {
            if (craterObject.GetComponent<Collider2D>().OverlapPoint(overlapPoint))
            {
                overlappingCraters.Add(craterObject);
            }
        }
    }

    public void TryOffloadFuel()
    {
        float availableSpace = 100 - ShipFuelInventory;
        if (availableSpace > FuelInventory)
        {
            ShipFuelInventory += FuelInventory;
            FuelInventory = 0;
            AudioManagerSpawner.instance.SwitchSFX("Refuel");
        }
        else
        {
            ShipFuelInventory += availableSpace;
            FuelInventory -= (int)availableSpace;
        }
    }


    private void UpdatePlayerValuesInUI()
    {
        LevelManager.Instance.UpdatePlayerValues(playerColor, SuitHealth, FuelInventory, ShipFuelInventory);
    }

    private float getCurrentSpeed()
    {
        if (SuitHealth > lowHealthThreshold)
        {
            return normalSpeed;
        }
        else if (SuitHealth > 0)
        {
            return medSpeed;
        }
        else return slowSpeed;
    }

    private bool checkNearShip()
    {
        return (ship.GetComponent<Collider2D>().OverlapPoint(transform.position));

    }

    private void TryRechargeSuit()
    {
        if (SuitHealth < 100)
        {
            SuitHealth += suitRechargeSpeed*Time.deltaTime;
            AudioManagerSpawner.instance.SwitchSFX("Power up");
        }
    }

    private void TryEnterShip()
    {
        if (checkNearShip() && ShipFuelInventory > highFuelThreshold)
        {
            lastPosition = transform.position;
            transform.position = offscreenPosition;
            tryingToEnterShip = true;
        }
    }

    private void EnterShip()
    {
        if (tryingToEnterShip)
        {
            isInShip = true;
            tryingToEnterShip = false;
        }
    }

    private void TryStopLaunch()
    {
        Debug.Log("Launch timer: " + launchTimer);
        if (launchTimer < launchTime)
        {
            //Cancel launch
            launchTimerStarted = false;
            launchTimer = 0f;
            LevelManager.Instance.DisplayTimer(playerColor, -1);
            if (launchTimer < 1f)
            {
                TryExitShip();
            }
            //Play appropriate sound
        }
        else //try to launch anyways
        {
            CheckLaunchSuccess();
        }
    }

    private void CountDownToLaunch()
    {

        launchTimer += Time.deltaTime;
        if (IsTryingLaunch)
        {
            LevelManager.Instance.DisplayTimer(playerColor, (int)(launchTime-launchTimer));
        }
    }

    private void CheckLaunchSuccess()
    {
        bool success = (LevelManager.Instance.GetOtherPlayer(playerColor)).IsTryingLaunch;
        Debug.Log("Try to launch: " + success);
        //if other player is also launching
        if (success)
        {
            LevelManager.Instance.EndGameTriggered = true;
            SceneManager.LoadSceneAsync("MainMenu");
            //trigger end game
        }
        else
        {
            FailToLaunch();

        }
        //if not


    }

    private void FailToLaunch()
    {
        launchTimerStarted = false;
        launchTimer = 0;
        TryStopLaunch();
        ShipFuelInventory = 0;
    }

    private void TryExitShip()
    {
        if (isInShip)
        {
            transform.position = lastPosition;
            isInShip = false;
        }
    }
}

public enum PlayerColor {Blue, Red};
