using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    [SerializeField] private UIBarFill blueSuitHealthBar;
    [SerializeField] private UIBarDiscrete blueInventoryBar;
    [SerializeField] private UIBarFill blueShipFuelBar;

    [SerializeField] private UIBarFill redSuitHealthBar;
    [SerializeField] private UIBarDiscrete redInventoryBar;
    [SerializeField] private UIBarFill redShipFuelBar;

    public static SceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static SceneManager instance;
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
}
