using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class endGameScript : MonoBehaviour
{

    public Text txtClassement;
    public GameObject btnRestart;
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();


    // Start is called before the first frame update
    void Start()
    {
        

        if (PhotonNetwork.isMasterClient)
        {
            btnRestart.SetActive(true);
        }

        List<PhotonPlayer>players = PhotonNetwork.playerList.OfType<PhotonPlayer>().ToList().OrderBy(x => x.GetScore()).ToList();
        
        int rank = 1;
        string txt = "";
        string back = "\n";

        foreach (PhotonPlayer player in players)
        {
            string str = string.Format("{0} | {1} ({2})", rank, player.NickName, player.GetScore());

            if (rank == 1)
                txt += "<color=\"#FFD700\">" + str + "</color>" + back;
            else if (rank == 2)
                txt += "<color=\"#C0C0C0\">" + str + "</color>" + back;
            else if (rank == 2)
                txt += "<color=\"#b08d57\">" + str + "</color>" + back;
            else
                txt += str;
            
            rank++;
        }

        this.txtClassement.text = txt;

        PhotonNetwork.room.IsOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOver()
    {
        this.ResetAllPlayers();
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void ResetAllPlayers()
    {
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            player.SetScore(0);
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
