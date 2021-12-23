using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class multimanager : MonoBehaviour
{

    private string VersionName = "0.1";

    public InputField UsernameInput;
    public InputField CodeInput;

    public Button btnHost;
    public Button btnJoin;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("GameUI"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void SetButton()
    {
        btnHost.gameObject.SetActive(UsernameInput.text.Length >= 3 && CodeInput.text.Length == 5);
        btnJoin.gameObject.SetActive(UsernameInput.text.Length >= 3 && CodeInput.text.Length == 5);
    }

    public void SetUsername()
    {
        string name = UsernameInput.text;
        PhotonNetwork.playerName = name;
    }

    public void HostGame()
    {
        this.SetUsername();

        Debug.Log("Hosting a game");

        PhotonNetwork.CreateRoom(CodeInput.text, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        this.SetUsername();

        Debug.Log("Joining a game");

        RoomOptions options = new RoomOptions();
        options.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(CodeInput.text, options, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
