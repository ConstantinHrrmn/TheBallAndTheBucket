using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager_multi : Photon.MonoBehaviour
{
    public GameObject PlayerPrefab;

    private void Start()
    {
        //this.SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        float x = -8;
        float y = -3;

        this.HideCanvas(false);

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(x, y), Quaternion.identity, 0);
    }

    public void HideCanvas(bool hideornot)
    {
        GameObject can = GameObject.Find("Canvas");
        can.SetActive(hideornot);
    }
}
