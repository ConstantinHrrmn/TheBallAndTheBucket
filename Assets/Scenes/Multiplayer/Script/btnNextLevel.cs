using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnNextLevel : MonoBehaviour
{

    public GameObject btnNext;

    public GameObject hostdisconnected;

    //public string levelName;

    bool isEnd;

    bool canCheck = false;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            //btnNext.SetActive(true);
            this.customProperties = PhotonNetwork.room.CustomProperties;
            this.customProperties["actualLevel"] = (int)this.customProperties["actualLevel"] + 1;

            Debug.Log("Actual : " + this.customProperties["actualLevel"] + " | Max : " + this.customProperties["levels"]);

            this.isEnd = (int)this.customProperties["levels"] == (int)this.customProperties["actualLevel"];

            StartCoroutine(this.StartChecking());
        }
    }

    IEnumerator StartChecking()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Start checking");
        this.canCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.room.MasterClientId != 1)
        {
            hostdisconnected.SetActive(true);
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MainMenu");
        }

        if (this.IsFinished() && this.canCheck)
        {
            this.LoadNext();
        }
    }

    bool IsFinished()
    {
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.CustomProperties.ContainsKey("done"))
            {
                if ((int)player.CustomProperties["done"] == 0)
                    return false;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void NextLevel(int failpoints)
    {
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((int)player.CustomProperties["done"] == 0)
            {
                player.SetScore(player.GetScore() + failpoints);
            }
        }

        this.LoadNext();
    }

    public void LoadNext()
    {
        if (this.isEnd)
        {
            PhotonNetwork.LoadLevel("EndGame");
        }
        else
        {
            PhotonNetwork.LoadLevel("multi" + ((int)this.customProperties["actualLevel"] + 1).ToString());
        }
    }
}
