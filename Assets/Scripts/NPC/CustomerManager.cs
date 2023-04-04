using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<GameObject> allCustomers;
    public GameObject currentCustomer;
    PatienceTimer patienceTimer;
    public bool shopOpen;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        shopOpen = false;
        patienceTimer = GameObject.Find("PatienceTimer").GetComponent<PatienceTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCustomer = patienceTimer.currentCustomer;
        currentCustomer = GameObject.FindGameObjectWithTag("NPC");

        if (!shopOpen)
        {
            if (currentCustomer != null)
            {
                if (currentCustomer.GetComponent<CustomerAgent>().atShop)
                {
                    currentCustomer.GetComponent<CustomerAgent>().stateMachine.ChangeState(CustomerStateId.Exit);
                }

                if (currentCustomer.GetComponent<CustomerAgent>().voided)
                {
                    Destroy(currentCustomer.gameObject);
                }
            }
        }

        if (shopOpen)
        {
            if (currentCustomer != null)
            {
                if (!currentCustomer.GetComponent<CustomerAgent>().atShop && currentCustomer.GetComponent<CustomerAgent>().dialogueTrigger.whichMessages == 0 && currentCustomer.GetComponent<CustomerAgent>().waitTimer.hasPatience)
                {
                    currentCustomer.GetComponent<CustomerAgent>().stateMachine.ChangeState(CustomerStateId.Enter);
                }

                if (currentCustomer.GetComponent<CustomerAgent>().voided == true)
                {
                    if (currentCustomer.GetComponent<CustomerAgent>().myNextCustomer != null)
                    {
                        allCustomers.Add(currentCustomer.GetComponent<CustomerAgent>().myNextCustomer);
                    }
                    //destroy current customer.
                    Destroy(currentCustomer.gameObject);
                }
            }

            if (currentCustomer == null)
            {
                //spawn new customer
                int randomCustomer = Random.Range(0, allCustomers.Count);
                //Vector3 whereToSpawn = new Vector3(-12, -1, 0);
                Instantiate(allCustomers[randomCustomer], spawnPoint.localPosition, allCustomers[randomCustomer].transform.rotation);
            }
        }
        
    }

    public void AddCustomer(GameObject customer)
    {
        allCustomers.Add(customer);
    }
}
