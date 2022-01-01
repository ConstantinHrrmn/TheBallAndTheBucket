using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnNextLevel : MonoBehaviour
{

    public GameObject btnNext;

    public GameObject hostdisconnected;

    //public string levelName;

    bool isEnd;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            btnNext.SetActive(true);
            this.customProperties = PhotonNetwork.room.CustomProperties;
            this.customProperties["actualLevel"] = (int)this.customProperties["actualLevel"] + 1;

            Debug.Log("Actual : " + this.customProperties["actualLevel"] + " | Max : " + this.customProperties["levels"]);

            this.isEnd = (int)this.customProperties["levels"] == (int)this.customProperties["actualLevel"];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.room.MasterClientId != 1)
        {
            hostdisconnected.SetActive(true);
        }
    }

    public void NextLevel()
    {
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((int)player.CustomProperties["done"] == 0)
            {
                player.SetScore(player.GetScore() + 5);
            }
        }

        if (this.isEnd)
        {
            PhotonNetwork.LoadLevel("EndGame");
        }
        else
        {
            PhotonNetwork.LoadLevel("multi"+ ((int)this.customProperties["actualLevel"] + 1).ToString());
        }
        
    }
}
