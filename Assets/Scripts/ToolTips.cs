using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    public bool toolTips;
    public TextMesh currentKey;
    // Start is called before the first frame update
    void Start()
    {
        toolTips = true;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (toolTips)
        {
            //currentKey = collision.GetComponent<Key>
            gameObject.SetActive(true);
            

        }
    }
}
