using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInfo : MonoBehaviour
{
    [SerializeField]
    public string characterName;
    [SerializeField]
    public Image characterSprite;
    [SerializeField]
    public Image dialogueProfile;

    [SerializeField]
    public float timeWaiting;
    [SerializeField]
    public string request;
    [SerializeField]
    public int amountPay;

}
