using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{

    public float speed = 1f;
    float counter;
    bool landed;
    bool onGround;
    Vector3 startPos;
    public ItemClass myItem;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player"); 
        counter = 0;
        landed = false;
        startPos = GameObject.Find("Player").transform.position;
        
    }

    void Update()
    {
        if (landed)
        {
            //has reached customer
            if (gameObject)
                Destroy(gameObject);
            return;
        }

        if (!landed)
        {
            if (GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>().facingLeft == false) 
            {
                Vector3 pos = new Vector3(startPos.x + Mathf.Sin(Mathf.PI * 4 * counter / 360), startPos.y, startPos.z);
                transform.position = Vector3.Lerp(transform.position, pos, 1f);
            }
            else if (GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>().facingLeft == true)
            {
                Vector3 pos = new Vector3(startPos.x + Mathf.Sin(Mathf.PI * -4 * counter / 360), startPos.y, startPos.z);
                transform.position = Vector3.Lerp(transform.position, pos, 1f);
            }
        }

        counter += speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //onGround = true;

        if (collision.gameObject.CompareTag("NPC"))
        {
            
            GameObject desired = collision.gameObject.GetComponent<CustomerActions>().desiredItem;
            if (gameObject.tag == desired.tag)
            {
                Debug.Log("This is right!");
                landed = true;
                collision.gameObject.GetComponent<CustomerActions>().CorrectItem();
                //true
            }
            else if (gameObject.tag != desired.tag)
            {
                collision.gameObject.GetComponent<CustomerActions>().WrongItem();
            }
            
            //
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player")) && (counter > 180))
        {
            GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>().Add(myItem, 1);
            landed = true;
        }
    }


}
