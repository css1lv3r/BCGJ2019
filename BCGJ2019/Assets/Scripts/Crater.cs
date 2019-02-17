using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    public bool isDry;
    // lets us know whether dry crater or wet crater, determines Crater appearance
    // can be accessed by Player also

    private int growth=0; 
    // 0 for unplanted, 1 for immature plants, 2 for ready-to-harvest
    // is initialized to 0 (empty crater)


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    // If crater has immature plants, have them mature (updateSprite already included within)
    void Update()
    {
        //TODO: specify time between growth intervals
        if (growth==1)
        {
            incGrowth(); //matures the plant
        }      
    }

    // immediate swatch to sprite corresponding to current plant maturity
    void updateSprite()
    {
        //TODO: find way to access sprite file path & find appropriate data type
        if (isDry) //if dry crater
        {
            if (growth==0)
            {
                //update sprite to empty dry crater 
            } else if (growth==1)
            {
                //update sprite to dry crater with buds
            } else 
            {
                //update sprite to dry crater with mature plants
            }
        } else // if wet crater
        {
            if (growth==0)
            {
                //update sprite to empty wet crater
            } else if (growth==1)
            {
                //update sprite to crater with immature algae
            } else
            {
                //update sprite to crater with mature algae
            }
        }
    }

    // matures plant by one stage, or plants plant if empty crater
    // called in update with immature plants 
    // AND in Player when it interacts with ready to plant crater (tries to plant in empty crater)
    public void incGrowth()
    {
        if (growth < 2)
        {
            growth++;
            updateSprite();
        }
    }

    // resets Crater plant growth stage to 0
    // should be called in a type of harvest function in Player
    public void resetGrowth()
    {
        growth = 0;
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
