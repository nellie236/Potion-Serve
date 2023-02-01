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
    public GameObject desiredItem;
    public SpriteRenderer myObject;
    public bool atShop;
    public bool orderFulfilled;
    public PatienceTimer patienceTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        //orderFulfilled = false;
        myRB = GetComponent<Rigidbody2D>();
        dialogueTrigger = transform.GetChild(0).gameObject.GetComponent<DialogueTrigger>();
        myObject = transform.GetChild(1).GetComponent<SpriteRenderer>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        patienceTimer = GameObject.Find("PatienceTimer").GetComponent<PatienceTimer>();
        patienceTimer.Duration = config.orderPatience;

        stateMachine = new CustomerStateMachine(this);
        stateMachine.RegisterState(new CustomerEnterState());
        stateMachine.RegisterState(new CustomerQueueState());
        stateMachine.RegisterState(new CustomerOrderInProgressState());
        stateMachine.RegisterState(new CustomerExitState());
        stateMachine.RegisterState(new CustomerIdleState());
        

        stateMachine.ChangeState(initialState);
        atShop = false;
    }

    // Update is called once per frame
    void Update()
    {
        inProgState = stateMachine.currentState;
        stateMachine.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShopTrigger"))
        {
            atShop = true;
        }

        if (collision.CompareTag("VoidTrigger"))
        {
            stateMachine.ChangeState(CustomerStateId.Idle);
        }
    }
}
