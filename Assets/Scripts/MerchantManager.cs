using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{

    //bool shopActive;
    Animator merchantAnim;

    // Start is called before the first frame update
    void Start()
    {
        merchantAnim = GetComponent<Animator>();
        merchantAnim.SetBool("shopActive", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openStore()
    {
        merchantAnim.SetBool("shopActive", true);
    }


}
