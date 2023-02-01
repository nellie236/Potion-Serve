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

    //private bool landed;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Throw");
        this.transform.position = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!landed)
        {
            playerX = player.transform.position.x;
            targetX = target.transform.position.x;

            dist = targetX - playerX;
            nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
            baseY = Mathf.Lerp(player.transform.position.y, target.transform.position.y, (nextX - playerX) / dist);
            //height = 2 * (nextX - playerX) * (nextX - targetX) / (-0.12f * dist * dist);
            height = (nextX - playerX) * (nextX - targetX) / (0.25f * dist * dist);

            Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
            transform.rotation = LookAtTarget(movePosition - transform.position);
            transform.position = movePosition;
        }*/

    }

    /*public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //onGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            //landed = true;
        }

        if (collision.gameObject.CompareTag("NPC"))
        {

            GameObject desired = collision.gameObject.GetComponent<CustomerActions>().desiredItem;
            if (gameObject.tag == desired.tag)
            {
                Debug.Log("This is right!");
                //landed = true;
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
        if ((collision.gameObject.CompareTag("Player")))
        {
            GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>().Add(myItem, 1);
            //landed = true;
            Destroy(gameObject);
        }
    }
}
