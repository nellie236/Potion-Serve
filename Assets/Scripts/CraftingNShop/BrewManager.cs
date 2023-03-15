using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewManager : MonoBehaviour
{
    public List<ItemClass> potItems;
    //public List<ItemClass> craftItems;
    public int numOfItems;
    public GameObject spawnPoint;
    public List<CraftingRecipe> recipes;
    public BrewManager brewManager;

    public List<GameObject> potGameObjects;
    public GameObject progressBar;
    Animator progBarAnim;
    

    private void Start()
    {
        brewManager = this.GetComponent<BrewManager>();
        spawnPoint = GameObject.Find("SpawnPoint");
        potItems = new List<ItemClass>();
        potGameObjects = new List<GameObject>();
        progressBar = GameObject.Find("progressBar");
        progBarAnim = progressBar.GetComponent<Animator>();
        progBarAnim.gameObject.SetActive(false);
        
    }

    //bool canCraft;
    private void Update()
    {
        numOfItems = potItems.Count;
        
        
    }

    IEnumerator GoCraft()
    {
        if (potItems.Count != 0)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                //recipes[i].CompareLists(potItems, recipes[i].Ingredients);
                recipes[i].Craft(brewManager);
            }
        }
        yield return new WaitForFixedUpdate();
    }

    public void PlayProgressBarAnim()
    {
        StartCoroutine(ProgressBar());
    }

    public IEnumerator ProgressBar()
    {
        progBarAnim.gameObject.SetActive(true);
        progBarAnim.Play("progBar");
        yield return new WaitForSeconds(3f);
        progBarAnim.gameObject.SetActive(false);
    }

    //private int numOfItems;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != null)
        {
            if (collision.gameObject.tag == "Ingredient")
            {
                potItems.Add(collision.GetComponent<Projectile>().myItem);
                //craftItems.Add(collision.GetComponent<Projectile>().myItem);
                potGameObjects.Add(collision.gameObject);
                StartCoroutine(GoCraft());
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
                //craftItems.Remove(collision.GetComponent<Projectile>().myItem);
                potGameObjects.Remove(collision.gameObject);
            }
        }
    }
}
