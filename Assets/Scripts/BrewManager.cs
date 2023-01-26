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

    public List<GameObject> potGameObjects;

    private void Start()
    {
        brewManager = this.GetComponent<BrewManager>();
        spawnPoint = GameObject.Find("SpawnPoint");
        potItems = new List<ItemClass>();
        potGameObjects = new List<GameObject>();
    }

    private void Update()
    {
        numOfItems = potItems.Count;
        
        if (potItems.Count != 0)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                //recipes[i].CompareLists(potItems, recipes[i].Ingredients);
                recipes[i].Craft(brewManager);
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
                potGameObjects.Add(collision.gameObject);
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
                potGameObjects.Remove(collision.gameObject);
            }
        }
    }
}
