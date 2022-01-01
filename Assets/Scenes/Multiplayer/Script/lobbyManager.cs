using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lobbyManager : Photon.MonoBehaviour
{

    public Text lblPlayerList;
    public Text lblRoomCode;
    public Text lblNbLevels;

    public string FirstLevel;

    public GameObject buttons;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(PhotonNetwork.playerList.Length);
        string roomname = PhotonNetwork.room.Name;
        this.lblRoomCode.text = "Room code : " + roomname;

        PhotonNetwork.player.SetScore(0);

        Debug.Log("Score of player : " + PhotonNetwork.player.GetScore());

        //Debug.Log(PhotonNetwork.isMasterClient);

        if (PhotonNetwork.isMasterClient)
        {
            this.buttons.SetActive(true);
            this.UpdateNbLevels(3);
        }
    }

    void UpdateNbLevels(int nb)
    {
        this.customProperties["actualLevel"] = 0;
        this.customProperties["levels"] = nb;
        PhotonNetwork.room.SetCustomProperties(this.customProperties);
    }

    // Update is called once per frame
    void Update()
    {
        string str = "";

        PhotonPlayer[] players = PhotonNetwork.playerList;
        Array.Sort(players);

        foreach (PhotonPlayer item in players)
        {
            str += item.NickName + "\n";
        }

        this.lblNbLevels.text = "Number of levels : " + PhotonNetwork.room.CustomProperties["levels"];

        this.lblPlayerList.text = str;
    }

    public void StartGame()
    {
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.LoadLevel("multi"+this.FirstLevel);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MultiplayerMenu");
    }

    public void Press3()
    {
        this.UpdateNbLevels(3);
    }

    public void Press5()
    {
        this.UpdateNbLevels(5);
    }

    public void Press10()
    {
        this.UpdateNbLevels(10);
    }

}
