using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnNextLevel : MonoBehaviour
{

    public GameObject btnNext;

    public GameObject hostdisconnected;

    public string levelName;

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

        PhotonNetwork.LoadLevel(levelName);
    }
}
