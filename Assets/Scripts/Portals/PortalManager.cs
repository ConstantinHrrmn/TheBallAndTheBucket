using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public GameObject PortalIn;
    public GameObject PortalOut;

    private bool cooldown;
    private float coolDownTime;

    public bool OUTup, OUTdown, OUTleft, OUTright;
    public bool INup, INdown, INleft, INright;



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

            Vector3 offset;
            if (OUTup)
            {
                offset = new Vector3(0, 0.3f);
            }
            else if (OUTdown)
            {
                offset = new Vector3(0, -0.3f);
            }
            else if (OUTleft)
            {
                offset = new Vector3(-0.3f, 0);
            }
            else
            {
                offset = new Vector3(0, -0.3f);
            }
            


            obj.gameObject.transform.position = this.PortalOut.transform.position + offset;
        }
        
    }

    public void Portal_Out_Touched(GameObject obj)
    {
        if (!this.cooldown)
        {
            this.cooldown = true;

            //Vector2 position = this.PortalOut.transform.position;
            //position.x = position.x + 0.3f;

            Vector3 offset;
            if (INup)
            {
                offset = new Vector3(0, 0.3f);
            }
            else if (INdown)
            {
                offset = new Vector3(0, -0.3f);
            }
            else if (INleft)
            {
                offset = new Vector3(-0.3f, 0);
            }
            else
            {
                offset = new Vector3(0, -0.3f);
            }


            obj.gameObject.transform.position = this.PortalIn.transform.position + offset;
        }
        
    }
}
