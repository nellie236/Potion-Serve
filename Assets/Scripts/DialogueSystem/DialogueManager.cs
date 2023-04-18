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
    //public int coinAmount;
    public Text coinPaymentAmount;

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

    public void Update()
    {
        if (isActive)
        {
            Time.timeScale = 0;
        }

        if (!isActive)
        {
            Time.timeScale = 1;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isActive == true)
        {
            NextMessage();
        }

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
        //coinPaymentAmount.text = currentCustomer.transform.parent.GetComponent<CustomerAgent>().config.coinAmount.ToString();
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
                Debug.Log("Ending dialogue");
                currentCustomer.GetComponent<DialogueTrigger>().dialoguePaths();
                dialogueBox.gameObject.SetActive(false);
                isActive = false;
                Time.timeScale = 1;
            }

        }
    }

    public void AcceptCustomer()
    {
        StartCoroutine(AcceptOrder());
    }

    public IEnumerator AcceptOrder()
    {
        acceptDenyChoice.SetActive(false);
        currentCustomer.GetComponent<DialogueTrigger>().whichMessages = 1;
        currentCustomer.GetComponent<DialogueTrigger>().dialoguePaths();
        OpenDialogue(currentCustomer.GetComponent<DialogueTrigger>().messages, currentCustomer.GetComponent<DialogueTrigger>().actors);
        //dialogueBox.gameObject.SetActive(false);
        //isActive = false;
        yield return new WaitForSeconds(2f);
        
    }

    public IEnumerator DenyOrder()
    {
        acceptDenyChoice.SetActive(false);
        currentCustomer.GetComponent<DialogueTrigger>().whichMessages = 2;
        currentCustomer.GetComponent<DialogueTrigger>().dialoguePaths();
        OpenDialogue(currentCustomer.GetComponent<DialogueTrigger>().messages, currentCustomer.GetComponent<DialogueTrigger>().actors);
        //dialogueBox.gameObject.SetActive(false);
        //isActive = false;
        yield return new WaitForSeconds(2f);
        
    }

    public void DenyCustomer()
    {
        StartCoroutine(DenyOrder());
    }

    public void orderSuccess()
    {
        currentCustomer.GetComponent<DialogueTrigger>().whichMessages = 3;
        currentCustomer.GetComponent<DialogueTrigger>().dialoguePaths();
        OpenDialogue(currentCustomer.GetComponent<DialogueTrigger>().messages, currentCustomer.GetComponent<DialogueTrigger>().actors);
    }

    public void orderFail()
    {
        currentCustomer.GetComponent<DialogueTrigger>().whichMessages = 4;
        currentCustomer.GetComponent<DialogueTrigger>().dialoguePaths();
        OpenDialogue(currentCustomer.GetComponent<DialogueTrigger>().messages, currentCustomer.GetComponent<DialogueTrigger>().actors);
    }

    
}
