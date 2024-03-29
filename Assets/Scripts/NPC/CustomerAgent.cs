using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAgent : MonoBehaviour
{
    public CustomerStateMachine stateMachine;
    public CustomerStateId initialState;
    public CustomerStateId inProgState;
    public CustomerAgentConfig config;

    public Rigidbody2D myRB;
    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;
    public CoinManager coinManager;
    public GameObject desiredItem;
    public GameObject displayOrder;
    public SpriteRenderer myObject;
    public bool atShop;
    public bool orderFulfilled;
    public bool voided;
    public PatienceTimer patienceTimer;
    public InitialWaitTimer waitTimer;
    public GameObject myNextCustomer;
    public GameObject myPrefab;
    public bool addedNext;
    

    // Start is called before the first frame update
    void Start()
    {
        //orderFulfilled = false;
        myRB = GetComponent<Rigidbody2D>();
        dialogueTrigger = transform.GetChild(0).gameObject.GetComponent<DialogueTrigger>();
        myObject = transform.GetChild(1).GetComponent<SpriteRenderer>();
        dialogueManager = GameObject.Find("DialogueParent").transform.GetChild(0).GetComponent<DialogueManager>();
        coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();
        patienceTimer = GameObject.Find("PatienceTimer").GetComponent<PatienceTimer>();
        patienceTimer.Duration = config.orderPatience;
        waitTimer = GetComponent<InitialWaitTimer>();
        waitTimer.Duration = config.waitTime;
        displayOrder = transform.GetChild(2).gameObject;
        //dialogueManager.coinPaymentAmount.text = config.coinAmount.ToString();
        //addedNext = false;


        stateMachine = new CustomerStateMachine(this);
        stateMachine.RegisterState(new CustomerEnterState());
        stateMachine.RegisterState(new CustomerQueueState());
        stateMachine.RegisterState(new CustomerOrderInProgressState());
        stateMachine.RegisterState(new CustomerExitState());
        stateMachine.RegisterState(new CustomerIdleState());
        

        stateMachine.ChangeState(initialState);
        atShop = false;
        voided = false;
    }

    // Update is called once per frame
    void Update()
    {
        inProgState = stateMachine.currentState;
        stateMachine.Update();

        if (myRB.velocity.magnitude > 0.01f)
        {
            Animator anim = transform.GetChild(3).GetComponent<Animator>();
            anim.SetBool("moving", true);
        }
        if (myRB.velocity.magnitude < 0.01f)
        {
            Animator anim = transform.GetChild(3).GetComponent<Animator>();
            anim.SetBool("moving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShopTrigger"))
        {
            if (!orderFulfilled)
            {
                atShop = true;
                StartCoroutine(AlertPlayer());
            }
        }

        if (collision.CompareTag("VoidTrigger"))
        {
            stateMachine.ChangeState(CustomerStateId.Idle);
            voided = true;
        }
    }

    private IEnumerator AlertPlayer()
    {
        GameObject.Find("Player").transform.GetChild(7).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        GameObject.Find("Player").transform.GetChild(7).gameObject.SetActive(false);
    }
}
