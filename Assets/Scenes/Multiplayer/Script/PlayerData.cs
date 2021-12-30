using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Photon.MonoBehaviour
{

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    public int shotsCount = 0;
    public bool finishedLevel = false;
    public Color color = Color.white;

    private Text txtShots;

    private Text w;
    private Text f;
    private Text players;



    // Start is called before the first frame update
    void Start()
    {
        this.txtShots = GameObject.Find("txtShots").GetComponent<Text>();
        this.players = GameObject.Find("txtplayers").GetComponent<Text>();
        this.f = GameObject.Find("txtFinished").GetComponent<Text>();
        this.w = GameObject.Find("txtWaiting").GetComponent<Text>();

        this.NewLevel();
    }

    public void WaitingFor()
    {
        this.w.text = "Waiting for : \n";
        this.f.text = "Finished : \n";

        //Debug.Log("Showing finished and waitings");

        foreach (PhotonPlayer item in PhotonNetwork.playerList)
        {
            try
            {
                if ((int)item.CustomProperties["done"] == 1)
                {
                    this.f.text += item.NickName + "\n";
                }
                else
                {
                    this.w.text += item.NickName + "\n";
                }
            }
            catch (System.Exception)
            {

                this.w.text += item.NickName + "\n";
            }
            


        }
    }

    // Update is called once per frame
    void Update()
    {
        this.WaitingFor();
        
    }

    public void SetColor()
    {
        this.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        this.customProperties["color"] = ColorUtility.ToHtmlStringRGB(this.color);
        this.customProperties["cl"] = new Vector3(this.color.r, this.color.g, this.color.b);

        this.SaveChanges();
    }

    public void DidAShot()
    {

        int score = PhotonNetwork.player.GetScore() + 1;

        PhotonNetwork.player.SetScore(score);

        this.txtShots.text = "Shots : " + score.ToString();

        //this.SaveChanges();
    }

    public void FinishedLevel()
    {
        //Debug.LogWarning("F1 --> " + PhotonNetwork.player.CustomProperties["done"]);

        if (!this.finishedLevel)
        {
            this.customProperties["done"] = 1;

            Debug.LogWarning("Set player turn to 1");

            this.finishedLevel = true;

            this.SaveChanges();
        }

        //Debug.LogWarning("F2 --> " + PhotonNetwork.player.CustomProperties["done"]);

    }

    public void NewLevel()
    {
        this.customProperties["done"] = 0;

        Debug.LogWarning("Set player turn to 0");

        this.finishedLevel = false;

        this.SaveChanges();
    }

    public void SaveChanges()
    {
        PhotonNetwork.player.SetCustomProperties(this.customProperties);
    }

    public void InstantiatePlayerList()
    {
        string text = "";
        PhotonPlayer[] players = PhotonNetwork.playerList;
        Array.Sort(players);

        foreach (PhotonPlayer item in players)
        {
            string c;

            try
            {
                c = (string)item.CustomProperties["color"];
            }
            catch (System.Exception)
            {
                c = "000000";
            }

            //string player = "<color=\"#" + c + "\">" + item.NickName + " : " + item.GetScore() + "</color>";
            string player = item.NickName + " : " + item.GetScore();

            text += player + "\n";
        }

        this.players.text = text;
    }
}
