using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupLevelManager : MonoBehaviour
{
    private ui UI;
    public int choosenMode;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        this.UI = GameObject.Find("GameUI").GetComponent<ui>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Choosen()
    {
        this.UI.SelectGameMode(this.choosenMode, this.amount);
    }



}
