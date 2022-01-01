using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager_multi : Photon.MonoBehaviour
{
    public GameObject PlayerPrefab;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    public GameObject WaitingCanvas;

    private void Start()
    {
        this.customProperties["done"] = 0;
        PhotonNetwork.player.SetCustomProperties(this.customProperties);
    }

    private void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        float x = -7;
        float y = -3;

        this.HideCanvas(false);

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(UnityEngine.Random.Range(x - 0.5f, x + 0.5f), UnityEngine.Random.Range(y - 0.5f, y + 0.5f)), Quaternion.identity, 0);
    }

    public void HideCanvas(bool hideornot)
    {
        GameObject can = GameObject.Find("Start");
        can.SetActive(hideornot);
    }



    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
