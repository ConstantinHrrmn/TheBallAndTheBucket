using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameui = GameObject.FindGameObjectWithTag("ui");
        gameui.GetComponent<ui>().EndGame();

        float time = gameui.GetComponent<ui>().time;
        int trys = gameui.GetComponent<ui>().trys;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
