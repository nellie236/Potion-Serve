using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    public bool toolTips;
    public TextMesh currentKey;
    public GameObject me;
    // Start is called before the first frame update
    void Start()
    {
        toolTips = true;
        me = this.gameObject;
        me.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (toolTips && collision.CompareTag("ToolTips"))
        {
            //currentKey = collision.GetComponent<Key>
            me.SetActive(true);
        }
    }


}
