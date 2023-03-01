using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    public bool facingRight = false;
    float moveDirection = 0;
    bool isGrounded = false;

    public float coyoteTime = 0.2f;
    float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    float jumpBufferCounter;



    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
    GameObject dialogueBox;
    Animator anim;
    public DialogueManager dialogueManager;
    public InventoryManager inventoryManager;
    public bool talkToCustomer;
    public bool leverTrigger;
    public bool canGiveItem;
    public GameObject currentCustomer;


    public KeyCode ToggleShop;
    public KeyCode GiveItem;
    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueManager = dialogueBox.GetComponent<DialogueManager>();
        inventoryManager = GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>();
        anim = GetComponent<Animator>();
        //Debug.Log(dialogueManager);
        
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        facingRight = false;
        canGiveItem = false;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeverTrigger"))
        {
            leverTrigger = true;
        }

        if (collision.CompareTag("DialogueTrigger"))
        {
            //give item to customer, turn bool true, if bool true,
            //can press button (perhaps hovers over the characters head)
            canGiveItem = true;
            currentCustomer = collision.transform.parent.gameObject;
            GameObject.Find("PatienceTimer").GetComponent<PatienceTimer>().ourCustomer(currentCustomer);
            //Debug.Log(currentCustomer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeverTrigger"))
        {
            leverTrigger = false;
        }

        if (collision.CompareTag("DialogueTrigger"))
        {
            canGiveItem = false;
            currentCustomer = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueManager.isActive == true || GameObject.Find("RecipeBookManager").GetComponent<RecipeBookManager>().bookOpen)
        {
            if (inventoryManager.inventoryOn == true)
            {
                inventoryManager.SwitchInventory();
            }
            return;
        }

        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            //anim.SetBool("moving", true);
        }
        else
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (!isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
            {
                moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
                //anim.SetBool("moving", true);
            }

            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
                anim.SetBool("moving", false);
            }

            
        }

        anim.SetBool("grounded", isGrounded);

        // Change facing direction
        if (moveDirection != 0)
        {
            anim.SetBool("moving", true);
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
        else if (moveDirection == 0)
        {
            anim.SetBool("moving", false);
        }

        // Jumping
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0f)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);

            jumpBufferCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space) && r2d.velocity.y > 0f)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, r2d.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y + 1, cameraPos.z);
        }

        if ((leverTrigger == true) && (Input.GetKeyUp(ToggleShop)))
        {
            //hitLever = !hitLever;
            //GameObject.Find("Main Camera").GetComponent<DayManager>().NextDay();
            //GameObject.Find("Main Camera").GetComponent<LoadSceneTrigger>().LoadScene();
            GameObject.Find("ShopManagerObject").GetComponent<ShopManager>().SwitchOpenClose();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryManager.SwitchInventory();
        }

        if ((canGiveItem && currentCustomer != null && currentCustomer.GetComponent<CustomerAgent>().inProgState == CustomerStateId.OrderInProgress) && (Input.GetKey(GiveItem)))
        {
            if (inventoryManager.selectedItem.throwablePrefab == currentCustomer.GetComponent<CustomerAgent>().desiredItem)
            {
                inventoryManager.GiveCustomerDesired(currentCustomer.GetComponent<CustomerAgent>().desiredItem.GetComponent<Projectile>().myItem, currentCustomer);
                currentCustomer.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = currentCustomer.GetComponent<CustomerAgent>().desiredItem.GetComponent<SpriteRenderer>().sprite;
            }
            //also play animation of customer "grabbing potion"
            //turn on sprite for customer, look like customer is holding potion
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    
}
    
