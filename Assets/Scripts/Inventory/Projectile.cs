using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public ItemClass myItem;

    public float speed = 1f;

    private float playerX;
    private float targetX;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    public float pickupDelay;
    private float remainingDelay;
    private bool canPickUp;
    public bool thrown;

    public float expireTimer;
    private float remainingExpire;
    public bool canExpire;

    public float outBoundsTimer;
    private float remainingOutBounds;
    //private bool landed;

    // Start is called before the first frame update
    void Start()
    {
        outBoundsTimer = 1f;
        remainingOutBounds = outBoundsTimer;
        expireTimer = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Throw");
        target = player.transform.GetChild(0).gameObject;
        ///this.transform.position = target.transform.position;
        
        if (gameObject.tag == "Potion")
        {
            remainingDelay = remainingDelay / 2;
            canExpire = false;
        }
        else
        {
            canPickUp = false;
        }

        remainingExpire = expireTimer;
        remainingDelay = pickupDelay;
        StartCoroutine(PickupDelay());

        if (gameObject.tag == "Ingredient")
        {
            canExpire = true;
            StartCoroutine(ExpireTimer());
        }
        //start timer
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Throw");
        
    }

    private IEnumerator PickupDelay()
    {
        while (remainingDelay >= 0)
        {
            //Debug.Log(remainingDelay);
            remainingDelay--;
            yield return new WaitForSeconds(1f);
            
        }
        OnEnd();
    }

    private IEnumerator ExpireTimer()
    {
        while (remainingExpire >= 0)
        {
            if (!canExpire)
            {
                break;
            }
            //Debug.Log(remainingDelay);
            if (remainingExpire <= expireTimer / 2)
            {
                //blink stuff
                StartCoroutine(Blink());
            }

            remainingExpire--;
            yield return new WaitForSeconds(1f);
            //GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
        if (canExpire)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator Blink()
    {
        while (remainingExpire <= expireTimer / 2)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            yield return new WaitForSeconds(.5f);
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator OutOfBounds()
    {
        while (remainingOutBounds >= 0)
        {
            remainingOutBounds--;
            yield return new WaitForSeconds(1f);
        }
        GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>().Add(myItem, 1);
        Destroy(gameObject);
    }

    private void OnEnd()
    {
        canPickUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ItemDispenser")
        {
            canExpire = false;
        }

        if (collision.tag == "Cauldron")
        {
            canExpire = false;
        }

        if (collision.tag == "OutBounds")
        {
            StartCoroutine(OutOfBounds());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ItemDispenser")
        {
            canExpire = true;
            StartCoroutine(ExpireTimer());
        }

        if (collision.tag == "Cauldron")
        {
            canExpire = true;
            StartCoroutine(ExpireTimer());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canPickUp)
        {
            if ((collision.gameObject.CompareTag("Player")))
            {
                if (!collision.gameObject.GetComponent<CharacterController2D>().inventoryManager.inventoryFull)
                {
                    collision.gameObject.GetComponent<CharacterController2D>().inventoryManager.Add(myItem, 1);
                    collision.transform.GetChild(6).gameObject.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                }
            }
        }
    }
}
