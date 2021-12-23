using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lobbyManager : Photon.MonoBehaviour
{

    public Text lblPlayerList;
    public Text lblRoomCode;

    public Button btnStart;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.playerList.Length);
        string roomname = PhotonNetwork.room.Name;
        this.lblRoomCode.text = "Room code : " + roomname;

        Debug.Log(PhotonNetwork.isMasterClient);

        if (PhotonNetwork.isMasterClient)
        {
            this.btnStart.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string str = "";
        foreach (var item in PhotonNetwork.playerList)
        {
            str += item.NickName + "\n";
        }

        this.lblPlayerList.text = str;
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("multi1");
    }
}
