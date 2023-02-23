using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    //public List<Image> RecipePages;
    public List<GameObject> recipePages;
    public int currentPage;
    public GameObject recipeBookParent;
    public bool canAccessBook;
    public bool bookOpen;

    public Button nextPage;
    public Button previousPage;

    public KeyCode ToggleRecipeBook;
    // Start is called before the first frame update
    void Start()
    {
        canAccessBook = false;
        bookOpen = false;
        TurnOffAll();
        recipeBookParent.SetActive(false);
        currentPage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAccessBook)
        {
            if (Input.GetKeyUp(ToggleRecipeBook))
            {
                OpenCloseBook();
            }
        }

        if (bookOpen)
        {
            if (currentPage == 0)
            {
                previousPage.gameObject.SetActive(false);
            }
            else
            {
                previousPage.gameObject.SetActive(true);
            }

            if (currentPage + 1 == recipePages.Count)
            {
                nextPage.gameObject.SetActive(false);
            }
            else
            {
                nextPage.gameObject.SetActive(true);
            }
        }
    }

    public void TurnOffAll()
    {
        foreach (GameObject obj in recipePages)
        {
            obj.SetActive(false);
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
            recipePages[currentPage].SetActive(true);
            bookOpen = true;
        }
        else if (recipeBookParent.activeInHierarchy)
        {
            Time.timeScale = 1;
            recipeBookParent.SetActive(false);
            bookOpen = false;
        }
    }

    public void NextPage()
    {
        if (currentPage + 1 != recipePages.Count)
        {
            recipePages[currentPage].SetActive(false);
            currentPage++;
            recipePages[currentPage].SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if (currentPage != 0)
        {
            recipePages[currentPage].SetActive(false);
            currentPage--;
            recipePages[currentPage].SetActive(true);
        }
    }
}
