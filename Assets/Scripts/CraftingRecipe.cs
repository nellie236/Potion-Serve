using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct ItemAmount
{
    public ItemClass item;
    [Range(1, 999)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemClass> Ingredients;
    //public List<ItemAmount> Materials; //ingredients needed for recipe
    public List<ItemClass> Results; //what you are crafting
    CraftingRecipe thisRecipe;

    public void Awake()
    {
        thisRecipe = this;
    }


    public void Craft(BrewManager brewManager)
    {

        //Debug.Log("Crafting");
        //Debug.Log(CompareLists(brewManager.potItems, Ingredients));
        if (CompareLists(brewManager.potItems, Ingredients))
        {
            foreach (ItemClass item in Results)
            {
                Instantiate(item.throwablePrefab, brewManager.spawnPoint.transform);
            }

            for (int p = 0; p < brewManager.potGameObjects.Count; p++)
            {
                for (int i = 0; i < Ingredients.Count; i++)
                {
                    if (brewManager.potGameObjects[p].GetComponent<Projectile>().myItem == Ingredients[i])
                    {
                        Destroy(brewManager.potGameObjects[p]);
                        //brewManager.potGameObjects.Remove(brewManager.potGameObjects[p]);
                    }
                }
            }

            foreach (ItemClass item in Ingredients)
            {
                brewManager.potItems.Remove(item);
            }
        }
    }

    public bool CompareLists(List<ItemClass> cauldronItems, List<ItemClass> recipeItems)
    {
        //bool sameList = false;
        //Debug.Log("Comparing Lists");
       
        if (cauldronItems == null || recipeItems == null || !(cauldronItems.Count >= recipeItems.Count)) //|| cauldronItems.Count != recipeItems.Count 
            return false;
        if (cauldronItems.Count == 0)
            return true;

        List<ItemClass> overLap = new List<ItemClass>();

        for (int index = 0; index < recipeItems.Count; index++) //foreach (ItemClass item in recipeItems)
        {
            //int index = recipeItems.IndexOf(item);
            

            //Debug.Log("foreach");
            for (int i = 0; i < cauldronItems.Count; i++)
            {
                //Debug.Log(overLap.Contains(cauldronItems[i]));
                //Debug.Log("Current recipe item index: " + index + ". Current cauldron item index: " + i);
                //Debug.Log("Overlap contains this item: " + cauldronItems[i] + " '" + i + "' " + overLap.Contains(cauldronItems[i]));
                if (!overLap.Contains(cauldronItems[i])) //(!overLap.Contains(cauldronItems.ElementAt(i)))
                {
                    if (EqualityComparer<ItemClass>.Default.Equals(cauldronItems[i], recipeItems[index]))
                    {
                        overLap.Add(cauldronItems[i]); 
                        //Debug.Log("Adding item, new count: " + overLap.Count);
                        break;
                    }
                }
                else 
                    //Debug.Log("Contains item already! Current count: " + overLap.Count); //currently calling this every time after first item is added.
                    continue;
                    //Debug.Log("contains item already!");
            }
        }

        if (overLap.Count == recipeItems.Count)
        {
            //sameList = true;
            Debug.Log("returning true");
            return true;
        }
        else
            return false;
  
    }


    /*public bool CompareLists<T>(List<T> cauldronItems, List<T> recipeItems)
    {
        Debug.Log("Comparing Lists");
        if (cauldronItems == null || recipeItems == null) //|| cauldronItems.Count != recipeItems.Count 
            return false;
        if (cauldronItems.Count == 0)
            return true;



        Dictionary<T, int> lookUp = new Dictionary<T, int>();
        //create index for the first list
        for (int i = 0; i < cauldronItems.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(cauldronItems[i], out count))
            {
                lookUp.Add(cauldronItems[i], 1);
                continue;
            }
            lookUp[cauldronItems[i]] = count + 1;
        }

        for (int i = 0; i < recipeItems.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(recipeItems[i], out count))
            {
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(recipeItems[i]);
            else
                lookUp[recipeItems[i]] = count;
        }

        return lookUp.Count == 0;
    }*/
}
