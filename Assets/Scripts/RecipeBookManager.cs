using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    public List<Image> RecipePages;
    public GameObject recipeBookParent;
    public bool canAccessBook;
    public bool bookOpen;
    // Start is called before the first frame update
    void Start()
    {
        canAccessBook = false;
        bookOpen = false;
        recipeBookParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canAccessBook)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                OpenCloseBook();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canAccessBook = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canAccessBook = false;
            if (recipeBookParent.activeInHierarchy) { OpenCloseBook(); }
        }
    }

    public void OpenCloseBook()
    {
        if (!recipeBookParent.activeInHierarchy)
        {
            Time.timeScale = 0;
            recipeBookParent.SetActive(true);
            bookOpen = true;
        }
        else if (recipeBookParent.activeInHierarchy)
        {
            Time.timeScale = 1;
            recipeBookParent.SetActive(false);
            bookOpen = false;
        }
    }
}
