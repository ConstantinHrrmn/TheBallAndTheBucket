using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalOutManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string s = collision.gameObject.tag;


        if (s == "Player")
        {
            this.gameObject.GetComponentInParent<PortalManager>().Portal_Out_Touched(collision.gameObject);
        }
    }
}
