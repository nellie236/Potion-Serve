using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text characterName;
    public Text messageText;
    public Image dialogueBox;
    public GameObject acceptDenyChoice;

    Message[] currentMessages;
    Actor[] currentActors;
    public GameObject currentCustomer;
    int activeMessage = 0;
    public bool isActive = false;

    private void Start()
    {
        isActive = false;
        dialogueBox.gameObject.SetActive(false);
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        dialogueBox.gameObject.SetActive(true);
        acceptDenyChoice.SetActive(false);
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started Conversation! Loaded messages: " + messages.Length);
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        characterName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation Ended!");

            if (currentCustomer.GetComponent<DialogueTrigger>().whichMessages == 0)
            {
                Debug.Log("Will you accept?");
                acceptDenyChoice.SetActive(true);
            }
            else 
            {
                currentCustomer.GetComponent<DialogueTrigger>().dialoguePaths();
                dialogueBox.gameObject.SetActive(false);
                isActive = false;
            }

            


        }
    }

    public void AcceptCustomer()
    {
        acceptDenyChoice.SetActive(false);
        currentCustomer.GetComponent<DialogueTrigger>().whichMessages = 1;
        //currentCustomer.GetComponentInParent<CustomerActions>().accepted = true;
        //currentCustomer.GetComponentInParent<CustomerActions>().denied = false;
        //currentCustomer.GetComponent<DialogueTrigger>().StartDialogue();
        dialogueBox.gameObject.SetActive(false);
        isActive = false;
    }

    public void DenyCustomer()
    {
        acceptDenyChoice.SetActive(false);
        currentCustomer.GetComponent<DialogueTrigger>().whichMessages = 2;
        //currentCustomer.GetComponentInParent<CustomerActions>().accepted = false;
        //currentCustomer.GetComponentInParent<CustomerActions>().denied = true;
        //currentCustomer.GetComponent<DialogueTrigger>().StartDialogue();
        dialogueBox.gameObject.SetActive(false);
        isActive = false;
    }

    void Update()
    {
        if (isActive)
        {
            Time.timeScale = 0;
        }
        else if (!isActive)
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
        {
            NextMessage();
        }

    }
}
