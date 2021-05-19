using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public GameObject PortalIn;
    public GameObject PortalOut;

    private bool cooldown;
    private float coolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        this.cooldown = false;
        this.coolDownTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.cooldown)
        {
            this.coolDownTime += Time.deltaTime;
            if (this.coolDownTime > 0.08f)
            {
                this.coolDownTime = 0;
                this.cooldown = false;
                Debug.Log("COOL DOWN RESET");
            }
        }
        
    }

    public void Portal_In_touched(GameObject obj)
    {
        if (!this.cooldown)
        {
            this.cooldown = true;
            obj.gameObject.transform.position = this.PortalOut.transform.position;
        }
        
    }

    public void Portal_Out_Touched(GameObject obj)
    {
        if (!this.cooldown)
        {
            this.cooldown = true;
            obj.gameObject.transform.position = this.PortalIn.transform.position;
        }
        
    }
}
