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
