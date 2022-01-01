using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{
    bool IamHost = false;

    private Text main;
    private Text secondary;

    public GameObject btnNextLevel;
    public GameObject Waiting;

    private int secondsBeforeEnd = 20;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        this.main = GameObject.Find("MainTimer").GetComponent<Text>();
        this.secondary = GameObject.Find("SecondaryTimer").GetComponent<Text>();
        this.Waiting = GameObject.Find("Waiting");

        StartCoroutine(this.StartHosting());
        this.customProperties["timer"] = -1;
        PhotonNetwork.room.SetCustomProperties(this.customProperties);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IamHost)
        {
            if (this.IsFinished())
            {
                this.IamHost = false;
                StartCoroutine(this.StartCountdown());
            }
        }

        int timer = (int)PhotonNetwork.room.CustomProperties["timer"];
        if (timer != -1)
        {
            this.UpdateLabels(timer);
        }
    }

    IEnumerator StartCountdown()
    {
        Debug.LogWarning("Player finished. Starting timer.");

        while (this.secondsBeforeEnd >= 0)
        {
            this.customProperties["timer"] = this.secondsBeforeEnd;
            PhotonNetwork.room.SetCustomProperties(this.customProperties);
            this.secondsBeforeEnd--;
            yield return new WaitForSeconds(1);
        }

        if (PhotonNetwork.isMasterClient)
        {
            //this.btnNextLevel.SetActive(true);
            this.Waiting.GetComponent<btnNextLevel>().NextLevel(10);
        }
    }

    void UpdateLabels(int timer)
    {
        this.main.text = timer.ToString();
        this.secondary.text = timer.ToString();
    }

    IEnumerator StartHosting()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Start Timer if i am the host");
        this.IamHost = PhotonNetwork.isMasterClient;
    }

    bool IsFinished()
    {
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.CustomProperties.ContainsKey("done"))
            {
                if ((int)player.CustomProperties["done"] == 1)
                    return true;
            }
        }

        return false;
    }
}
