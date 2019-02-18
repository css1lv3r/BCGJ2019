using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private UIBarFill blueSuitHealthBar;
    [SerializeField] private UIBarDiscrete blueInventoryBar;
    [SerializeField] private UIBarFill blueShipFuelBar;

    [SerializeField] private UIBarFill redSuitHealthBar;
    [SerializeField] private UIBarDiscrete redInventoryBar;
    [SerializeField] private UIBarFill redShipFuelBar;

    [SerializeField] private Text blueShipTimer;
    [SerializeField] private Text redShipTimer;

    public Player redPlayer;
    public Player bluePlayer;

    public bool EndGameTriggered = false;

    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static LevelManager instance;
    //----------------------------------------------------------------------------------------------------
    void Awake()
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

    //----------------------------------------------------------------------------------------------------
    public void UpdatePlayerValues(PlayerColor pColor, float suitHealth, int inventory, float shipFuel)
    {
        if (pColor == PlayerColor.Blue)
        {
            blueSuitHealthBar.UpdateDisplay(suitHealth);
            blueInventoryBar.UpdateDisplay(inventory);
            blueShipFuelBar.UpdateDisplay(shipFuel);
        }
        else
        {
            redSuitHealthBar.UpdateDisplay(suitHealth);
            redInventoryBar.UpdateDisplay(inventory);
            redShipFuelBar.UpdateDisplay(shipFuel);
        }
    }

    public void UpdatePlayerHealth(PlayerColor pColor, float health)
    {
        if (pColor == PlayerColor.Blue)
        {
            blueSuitHealthBar.UpdateDisplay(health);
        }
        else
        {
            redSuitHealthBar.UpdateDisplay(health);
        }
    }

    public void UpdatePlayerInventory(PlayerColor pColor, int inventory)
    {
        if (pColor == PlayerColor.Blue)
        {
            blueInventoryBar.UpdateDisplay(inventory);
        }
        else
        {
            redInventoryBar.UpdateDisplay(inventory);
        }
    }

    public void UpdateShipFuel(PlayerColor pColor, float fuel)
    {
        if (pColor == PlayerColor.Blue)
        {
            blueShipFuelBar.UpdateDisplay(fuel);
        }
        else
        {
            redShipFuelBar.UpdateDisplay(fuel);
        }
    }

    public void DisplayTimer(PlayerColor pc, int time)
    {
        Debug.Log("Displaying time: " + time);
        switch (pc)
        {
            case PlayerColor.Blue:
                blueShipTimer.text = (time > 3 || time < 0) ? "" : time + "";
                break;
            case PlayerColor.Red:
                redShipTimer.text = (time > 3 || time < 0) ? "" : time + "";
                break;
            default:
                break;
        }
    }

    public Player GetPlayer(PlayerColor pc)
    {
        return (pc == PlayerColor.Blue) ? bluePlayer : redPlayer;
    }

    public Player GetOtherPlayer(PlayerColor pc)
    {
        return (pc == PlayerColor.Red) ? bluePlayer : redPlayer;
    }
}
