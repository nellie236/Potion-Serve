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

    public bool canTalkToPlayer;

    public void Start()
    {
        dialogueParent = GameObject.Find("DialogueParent");
        DialogueManager = dialogueParent.transform.Find("DialogueBox").gameObject;
    }
    
    public void StartDialogue()
    {
        //DialogueManager = GameObject.Find("DialogueBox");
        DialogueManager.GetComponent<DialogueManager>().OpenDialogue(messages, actors);
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.R)) && (canTalkToPlayer == true))
        {
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
