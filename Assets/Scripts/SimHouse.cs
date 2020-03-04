using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHouse : MonoBehaviour
{
    private int maxNumHabitants;
    private SimNPC[] habitants;

    private int currentWaterAmount;
    private SimNPC waterCollector;

    private int currentFoodAmount;
    private SimNPC foodCollector;

    private bool hasOpenSpotsAvailable; //If there is enough supplies and not at max capacity. //TODO Need to make method to check for this.

    // Start is called before the first frame update
    void Start()
    {
        //Houses start out empty, with no water, and no food.
        habitants = new SimNPC[maxNumHabitants]; //Should I change this to arrayList to account for varying size?
        currentWaterAmount = 0;
        currentFoodAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if maxHabitant size should change based on supplies.
        CheckSuppliesMaxHabitants();

        //Check if water-collector is needed.
        CheckWaterCollectorNeeded();
        //Check if food-collector is needed.
        CheckFoodCollectorNeeded();

        //Every 2 seconds, inhabitants each consume one water and one food, if any is available. Coroutine?

        //After consumption, if house cant support all current inhabitants, extras should be kicked out.
        //      Should this be done in CheckSuppliesMaxHabitants()?
    }

    /*
     * This method checks what the maxNumHabitants should be for this house, based on the current supplies.
     */
    private void CheckSuppliesMaxHabitants()
    {
        
        if(currentWaterAmount <= 0) //A house without water can have 2 NPCs max.
        {
            maxNumHabitants = 2;
        }
        else if(currentFoodAmount <= 0) //A house with water, but no food, can have 4 NPCs max.
        {
            maxNumHabitants = 4;
        }
        else                        //A house with water and food can have 6 NPCs max.
        {
            maxNumHabitants = 6;
        }
    }

    private void CheckWaterCollectorNeeded()
    {
        if (currentWaterAmount < 10) //If house has fewer than 10 (pints) water,
        {
            if (waterCollector == null) //if nobody is already assigned to collect water,
            {
                SimNPC newWaterCollector = FindNonBusyNPC();
                if (newWaterCollector != null) // and if there is a non-busy npc in the house,
                {
                    //An NPC will be assigned to collect water.
                    waterCollector = newWaterCollector;
                    //Tell water collector to get to work!
                    waterCollector.FetchWater();
                }
            }
        }
    }

    private void CheckFoodCollectorNeeded()
    {
        if (currentFoodAmount < 20) //If house has fewer than 20 (meals) food,
        {
            if (foodCollector == null) //if nobody is already assigned to collect food,
            {
                SimNPC newFoodCollector = FindNonBusyNPC();
                if (newFoodCollector != null) // and if there is a non-busy npc in the house,
                {
                    //An NPC will be assigned to collect food.
                    foodCollector = newFoodCollector;
                    //Tell food collector to get to work!
                    foodCollector.FetchFood();
                }
            }
        }
    }

    private SimNPC FindNonBusyNPC()
    {
        SimNPC result = null;
        foreach (SimNPC habitant in habitants)
        {
            if (habitant.IsBusy() == false)
            {
                result = habitant;
            }
        }
        return result;
    }
}
