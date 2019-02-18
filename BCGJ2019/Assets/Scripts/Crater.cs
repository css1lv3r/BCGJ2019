using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    public bool isDry;    // lets us know whether dry crater or wet crater, determines Crater appearance
    public SpriteRenderer plantSprite;
    public Sprite[] plantStateSprites;
    public int fuelQuantity = 1;

    private int growth=0;    // 0 for unplanted, 1 for immature plants, 2 for ready-to-harvest
    private float growthTimer = 0f;

    private const float kGrowTime = 20f;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    // If crater has immature plants, have them mature (updateSprite already included within)
    void Update()
    {
        //TODO: specify time between growth intervals
        
         tryIncGrowth(); //matures the plant   
    }

    // immediate swatch to sprite corresponding to current plant maturity
    void updateSprite()
    {
        //TODO: find way to access sprite file path & find appropriate data type
        if (isDry) //if dry crater
        {
            
        } else // if wet crater
        {
        }


        //set plant growth sprite
        plantSprite.sprite = plantStateSprites[growth];
    }

    // matures plant by one stage, or plants plant if empty crater
    // called in update with immature plants 
    // AND in Player when it interacts with ready to plant crater (tries to plant in empty crater)
    public void tryIncGrowth()
    {
        if (growth==1)
        {
            growthTimer += Time.deltaTime;
            if (growthTimer > kGrowTime)
            {
                ++growth;
                updateSprite();
            }
            
        }
    }

    public bool TryToPlant(bool needsDry)
    {
        if (growth == 0)
        {
            if (needsDry == isDry)
            {
                growth = 1;
                updateSprite();
                return true;
            }
            
        }
        return false;
    }

    public float TryToHarvest(float inventorySpace)
    {
        if ((growth == 2) && (inventorySpace >= fuelQuantity))
        {
            resetGrowth();
            return fuelQuantity;
        }
        return 0f;
    }




    // resets Crater plant growth stage to 0
    // should be called in a type of harvest function in Player
    public void resetGrowth()
    {
        growth = 0;
        growthTimer = 0f;
        updateSprite();
    }

    // returns whether crater contains mature plants (ready to harvest)
    // should be called in Player
    public bool isReadyToHarvest()
    {
        return (growth == 2);
    }

    // returns whether crater is empty (player is able to plant something in it)
    // should be called in Player
    public bool isReadyToPlant()
    {
        return (growth == 0);
    }
}
