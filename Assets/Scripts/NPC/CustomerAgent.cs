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
    public bool atShop;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        dialogueTrigger = transform.GetChild(0).gameObject.GetComponent<DialogueTrigger>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        stateMachine = new CustomerStateMachine(this);
        stateMachine.RegisterState(new CustomerEnterState());
        stateMachine.RegisterState(new CustomerQueueState());
        stateMachine.RegisterState(new CustomerOrderInProgressState());
        stateMachine.RegisterState(new CustomerExitState());
        stateMachine.RegisterState(new CustomerIdleState());
        inProgState = stateMachine.currentState;

        stateMachine.ChangeState(initialState);
        atShop = false;
    }

    // Update is called once per frame
    void Update()
    {
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
