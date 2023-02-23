using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueParent;
    public GameObject DialogueManager;
    public Message[] messages;
    public Actor[] actors;


    public int whichMessages;
    public Message[] firstMessages;
    public Message[] acceptedMessages;
    public Message[] deniedMessages;
    public Message[] correctItemMessages;
    public Message[] wrongItemMessages;

    public bool canTalkToPlayer;

    

    public void Start()
    {
        whichMessages = 0;
        dialogueParent = GameObject.Find("DialogueParent");
        DialogueManager = dialogueParent.transform.Find("DialogueBox").gameObject;
    }
    
    public void StartDialogue()
    {
        //DialogueManager = GameObject.Find("DialogueBox");
        DialogueManager.GetComponent<DialogueManager>().OpenDialogue(messages, actors);
        DialogueManager.GetComponent<DialogueManager>().currentCustomer = this.gameObject;
        //DialogueManager.GetComponent<DialogueManager>().coinAmount = this.transform.parent.GetComponent<CustomerAgent>().config.coinAmount;
        //DialogueManager.GetComponent<DialogueManager>().coinPaymentAmount.text = this.transform.parent.GetComponent<CustomerAgent>().config.coinAmount.ToString();
        //GetComponentInParent<CustomerActions>().spokenTo = true;
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.R)) && (canTalkToPlayer == true))
        {
            dialoguePaths();
            StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalkToPlayer = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalkToPlayer = false;
        }
    }

    //startDialogue(1); 1 = intro dialogue 2 = accepted / waiting dialogue 3 = denied dialogue 4 = correct item dialogue 5 = incorrect item
    public void dialoguePaths()
    {
        switch (whichMessages)
        {
            case 0:
                messages = firstMessages;
                break;
            case 1:
                messages = acceptedMessages;
                break;
            case 2:
                messages = deniedMessages;
                break;
            case 3:
                messages = correctItemMessages;
                break;
            case 4:
                messages = wrongItemMessages;
                break;
        }
    }


}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
