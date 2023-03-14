using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour
{
    public GameObject itemToDispense;
    public float dispenseDelay;
    public float remainingDelay;
    public bool itemPresent;
    //public Transform spawnPosition;
    public GameObject spawner;
    public List<GameObject> currentObjects;

    // Start is called before the first frame update
    void Start()
    {
        //spawnPosition = this.transform;
        //spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        itemPresent = false;
        remainingDelay = dispenseDelay;
        currentObjects = new List<GameObject>();
        StartCoroutine(DispenseItemTimer());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            itemPresent = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            itemPresent = true;
            currentObjects.Add(collision.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            currentObjects.Remove(collision.gameObject);
            if (currentObjects.Count <= 0)
            {
                itemPresent = false;
                StartCoroutine(DispenseItemTimer());
            }
            //start timer
        }
    }

    private IEnumerator DispenseItemTimer()
    {
        if (!itemPresent)
        {
            while (remainingDelay >= 0)
            {
                remainingDelay--;
                yield return new WaitForSeconds(1f);
            }
        }
        OnEnd();
    }

    private void OnEnd()
    {
        GameObject dispensed = Instantiate(itemToDispense, new Vector2(spawner.transform.position.x, spawner.transform.position.y), Quaternion.identity) as GameObject;
        dispensed.transform.localPosition = spawner.transform.position;
        remainingDelay = dispenseDelay;
    }
}
