using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    private float update;
    public bool isDry;
    private int growth; // 0 for unplanted, 1 for immature plants, 2 for ready-to-harvest
    public bool readyToHarvest;
    public bool readyToPlant;

    // Start is called before the first frame update
    void Start()
    {

    
    }

    // Update is called once per frame
    void Update()
    {
        if (growth=1)
        {
            incGrowth();
        }
        updateCraterStatus();
        updateSprite();      
    }

    void updateSprite()
    {
        if (isDry)
        {
            if (growth=0)
            {
                //update sprite to empty dry crater 
            } else if (growth=1)
            {
                //update sprite to dry crater with buds
            } else 
            {
                //update sprite to dry crater with mature plants
            }
        } else
        {
            if (growth=0)
            {
                //update sprite to empty wet crater
            } else if (growth=1)
            {
                //update sprite to crater with immature algae
            } else
            {
                //update sprite to crater with mature algae
            }
        }
        
    }

    // called in update AND when player interacts with readyToPlant crater
    void incGrowth()
    {
        if (growth < 2)
        {
            growth++;
        }
    }

    // to be called by other class when player interacts
    void resetGrowth()
    {
        growth = 0;
    }

    void updateCraterStatus()
    {
        if (growth=0)
        {
            readyToPlant = true;
        } else if (growth=2)
        {
            readyToHarvest = true;
        }
    }
}
