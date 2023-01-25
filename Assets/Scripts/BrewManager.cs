using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewManager : MonoBehaviour
{
    public List<ItemClass> potItems;
    public int numOfItems;
    public GameObject spawnPoint;
    public List<CraftingRecipe> recipes;
    public BrewManager brewManager;
    public List<GameObject> potItemsGameObjects;

    private void Start()
    {
        brewManager = this.GetComponent<BrewManager>();
        spawnPoint = GameObject.Find("SpawnPoint");
        potItems = new List<ItemClass>();
    }

    private void Update()
    {

        // CheckIngredients();
        if (potItems.Count != 0)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                //recipes[i].CompareLists(potItems, recipes[i].Ingredients);
                recipes[i].Craft(brewManager);
            }
        }
    }

    void CheckIngredients()
    {
        numOfItems = potItems.Count;

        for (int i = potItems.Count - 1; i >= 0; i--)
        {
            if (potItems[i] != null)
            {
                //do things
            }
            else if (potItems[i] == null)
            {
                potItems.Remove(potItems[i]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != null)
        {
            if (collision.gameObject.tag == "Ingredient")
            {
                potItems.Add(collision.GetComponent<Projectile>().myItem);
                potItemsGameObjects.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != null)
        {
            if (collision.gameObject.tag == "Ingredient")
            {
                potItems.Remove(collision.GetComponent<Projectile>().myItem);
                potItemsGameObjects.Remove(collision.gameObject);
            }
        }
    }
}
