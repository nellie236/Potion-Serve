using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject DialogueManager;
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
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
