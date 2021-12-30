using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        PhotonNetwork.automaticallySyncScene = true;
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
        options.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(CodeInput.text, options, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        /*ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

        Color color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        customProperties["color"] = ColorUtility.ToHtmlStringRGB(color);
        customProperties["cl"] = new Vector3(color.r, color.g, color.b);

        PhotonNetwork.player.SetCustomProperties(customProperties);

        Debug.Log(PhotonNetwork.player.NickName + " : " + PhotonNetwork.player.CustomProperties["cl"]);*/

        PhotonNetwork.LoadLevel("Lobby");
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
