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

    //private bool landed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Throw");
        target = player.transform.GetChild(0).gameObject;
        ///this.transform.position = target.transform.position;
        remainingDelay = pickupDelay;
        canPickUp = false;
        StartCoroutine(PickupDelay());
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

    /*public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }*/

    private void OnEnd()
    {
        canPickUp = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //onGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            //landed = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canPickUp)
        {
            if ((collision.gameObject.CompareTag("Player")))
            {
                GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>().Add(myItem, 1);
                //landed = true;
                Destroy(gameObject);
            }
        }
    }
}
