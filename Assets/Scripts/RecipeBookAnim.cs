using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookAnim : MonoBehaviour
{

    public RecipeBookManager recipeBookManager;
    public Animator myAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = this.gameObject.GetComponent<Animator>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLeftAnim()
    {
        this.gameObject.SetActive(true);
        myAnim.Play("leftPageFlip");
    }

    public void PlayRightAnim()
    {
        this.gameObject.SetActive(true);
        myAnim.Play("rightPageFlip");
    }

    public void RecipeBookNextPage()
    {
        recipeBookManager.NextPage();
        this.gameObject.SetActive(false);
    }

    public void RecipeBookPreviousPage()
    {
        recipeBookManager.PreviousPage();
        this.gameObject.SetActive(false);
    }
}
