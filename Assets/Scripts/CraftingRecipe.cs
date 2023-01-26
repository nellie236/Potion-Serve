using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        Debug.Log("Crafting");
        if (CompareLists(brewManager.potItems, Ingredients))
        {
            foreach (GameObject gameObject in brewManager.potGameObjects)
            {
                for (int i = 0; i < Ingredients.Count; i++)
                {
                    if (brewManager.potGameObjects[brewManager.potGameObjects.IndexOf(gameObject)].GetComponent<Projectile>().myItem == Ingredients[i])
                    {
                        brewManager.potGameObjects.Remove(gameObject);
                        //Destroy(gameObject);
                    }
                }
            }

            foreach (ItemClass item in Ingredients)
            {
                brewManager.potItems.Remove(item);
            }

            foreach (ItemClass item in Results)
            {
                Instantiate(item.throwablePrefab, brewManager.spawnPoint.transform);
            }
        }
    }

    public bool CompareLists<T>(List<T> cauldronItems, List<T> recipeItems)
    {
        Debug.Log("Comparing Lists");
        if (cauldronItems == null || recipeItems == null || cauldronItems.Count != recipeItems.Count)
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
    }
}
