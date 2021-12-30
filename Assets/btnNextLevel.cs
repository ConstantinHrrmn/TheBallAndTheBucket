using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnNextLevel : MonoBehaviour
{

    public GameObject btnNext;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            btnNext.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
