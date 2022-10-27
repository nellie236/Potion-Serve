using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkToNPC : MonoBehaviour
{
    public bool canTalkToNPC;

    public Image DialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        //DialogueBox.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            canTalkToNPC = true;
            if ((Input.GetKey(KeyCode.R)))
            {
                collision.gameObject.GetComponent<DialogueTrigger>().StartDialogue();
                //DialogueBox.gameObject.SetActive(true);
                Debug.Log("Hey there NPC!");
            }
        }
        
    }
}
