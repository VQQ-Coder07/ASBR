using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PUN2_GameLobby : MonoBehaviourPunCallbacks
{
    public GameObject enterName;
    public Text firstname;
    public Text newname;
    public GameObject loadingParty;
    public Text hostName;
    public GameObject hostControls;
    //public GameObject guestControls;
    public GameObject LoadingScreen;
    public Text PartyStatus;
    public Text loadingText;
    public LoadingBarLerp loadingBar;
    public GameObject leftParty;
    public GameObject partyJoined;
	public GameManager gameManager;
    public Transform[] places;
	const string glyphs= "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	public Text joincode_text;
	public InputField joincode;
    //Our player name
    string playerName = "Player 1";
    public string gameVersion = "0.9";
    List<RoomInfo> createdRooms = new List<RoomInfo>();
    string roomName = "Room 1";
    Vector2 roomListScroll = Vector2.zero;
    bool joiningRoom = false;
	private bool inRoom;
	public bool ready;
    private bool inParty;
    private bool quickjoin;
    private bool tutorial;
    public static PUN2_GameLobby instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        PartyStatus.text = "Status: " + PhotonNetwork.NetworkClientState;
        loadingText.text = "Status: " + PhotonNetwork.NetworkClientState;
            switch(PhotonNetwork.NetworkClientState.ToString())
            {
            case "Disconnected":
                loadingBar.SetVal(0);
                break;
            case "ConnectingToNameServer":
                loadingBar.SetVal(10);
                break;
            case "ConnectedToNameServer":
                loadingBar.SetVal(30);
                break;
            case "Authenticating":
                loadingBar.SetVal(50);
                break;
            case "ConnectingToMasterServer":
                loadingBar.SetVal(70);
                break;
            case "JoiningLobby":
                loadingBar.SetVal(90);
                break;
            case "JoinedLobby":

                leftParty.SetActive(true);
                loadingParty.SetActive(false);
                leavingparty = false;
                loadingBar.SetVal(100);
                //Invoke("hide", 1f);
                break;
            case "Joined":
            loadingBar.SetVal(100);
            break;
        }
    }
    private void UpdateHost()
    {
        hostName.text = string.Format("Host: {0}", PhotonNetwork.IsMasterClient);
        if(PhotonNetwork.IsMasterClient)
        {
            hostControls.SetActive(true);
            //guestControls.SetActive(false);
        }
        else
        {
            hostControls.SetActive(false);
            //guestControls.SetActive(true);
        }
    }
    private void hide()
    {
        LoadingScreen.SetActive(false);
    }
    private void joinRandom()
    {
        quickjoin = true;
        loadingBar.joininggame = true;
        LoadingScreen.SetActive(true);
        LoadingScreen.GetComponent<Animator>().SetTrigger("swipein");
        bool empty = true;
        if(createdRooms.Count != 0)
        {
            for (int i = 0; i < createdRooms.Count; i++)
            {
                if(createdRooms[i].PlayerCount > 0)
                {
                    empty = false;
                    joiningRoom = true;

                    PhotonNetwork.NickName = playerName;

                    PhotonNetwork.JoinRoom(createdRooms[i].Name);
                }
            }
        }
        if (createdRooms.Count == 0 || !empty )
        {
		    CreateRoom("public", 3);
        }
    }
    private int playerID()
    {
        int ID = PlayerPrefs.GetInt("PlayerID");
        if (ID == 0)
        {
            ID = Random.Range(1, 9999999);
        }

        return ID;
    }
	public void LoadPlayers()
	{
		for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
		{
			Character character = (Character)PhotonNetwork.PlayerList[i].CustomProperties["character"];
		}
	}
	public void CreateParty()
	{
        loadingParty.SetActive(true);
		string randomID = "";
		for(int i=0; i<5; i++)
		{			
 			randomID += glyphs[Random.Range(0, glyphs.Length)];
		}
		joincode_text.text = randomID;
        leftParty.SetActive(false);
		if(PhotonNetwork.InRoom)
		{
			PhotonNetwork.LeaveRoom();
		}
		CreateRoom("private", 3);
        inParty = true;
	}
	public void JoinParty()
	{
		foreach (RoomInfo roomInfo in createdRooms)
		{
			if(roomInfo.Name == (joincode.text).ToUpper())
			{
				if(PhotonNetwork.InRoom)
				{
					PhotonNetwork.LeaveRoom();
				}
				PhotonNetwork.JoinRoom(roomInfo.Name);
				
			}
		}
        inParty = true;
	}
    private void OnPlayerEnteredRoom(Player newPlayer)
    {
        int id = (int)newPlayer.CustomProperties["characterID"];
        string name = newPlayer.NickName;
    }
    private bool leavingparty;
	public void LeaveParty(bool removed)
	{
        Chat.instance.enable(false);
		partyJoined.SetActive(false);
        //leftParty.SetActive(true);
        leavingparty = true;
        loadingParty.SetActive(true);
        Chat.instance.photonView.RPC("LeftPartyMember", RpcTarget.All, PhotonNetwork.NickName, removed);
        PhotonNetwork.LeaveRoom();
        inParty = false;
	}
    public void CreateRoom(string type, int maxplayers)
    {
        if (type == "public")
        {
            joiningRoom = true;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = (byte)maxplayers;

            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        }
        else if (type == "private")
        {
            joiningRoom = true;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = false;
            roomOptions.MaxPlayers = (byte)10;

            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        }
    }
    void Start()
    {
        playerName = PlayerPrefs.GetString("nickname");
        if(playerName == "")
        {
            enterName.SetActive(true);
        }
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + cause.ToString() + " ServerAddress: " + PhotonNetwork.ServerAddress);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        //After we connected to Master server, join the Lobby
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        //Debug.LogError(PhotonNetwork.CloudRegion);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("We have received the Room list");
        //After this callback, update the room list
        createdRooms = roomList;
    }

    void OnGUI()
    {
        //GUI.Window(0, new Rect(Screen.width / 2 - 450, Screen.height / 2 - 200, 900, 400), LobbyWindow, "Lobby");
    }

    void LobbyWindow(int index)
    {
        //Connection Status and Room creation Button
        GUILayout.BeginHorizontal();

        GUILayout.Label("Status: " + PhotonNetwork.NetworkClientState);

        if (joiningRoom || !PhotonNetwork.IsConnected || PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
        {
            GUI.enabled = false;
        }

        GUILayout.FlexibleSpace();

        //Room name text field
        roomName = GUILayout.TextField(roomName, GUILayout.Width(250));

        if (GUILayout.Button("Create Room", GUILayout.Width(125)))
        {
            if (roomName != "")
            {
                joiningRoom = true;

                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsOpen = true;
                roomOptions.IsVisible = true;
                roomOptions.MaxPlayers = (byte)10; //Set any number

                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            }
        }

        GUILayout.EndHorizontal();

        //Scroll through available rooms
        roomListScroll = GUILayout.BeginScrollView(roomListScroll, true, true);

        if (createdRooms.Count == 0)
        {
            GUILayout.Label("No Rooms were created yet...");
        }
        else
        {
            for (int i = 0; i < createdRooms.Count; i++)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(createdRooms[i].Name, GUILayout.Width(400));
                GUILayout.Label(createdRooms[i].PlayerCount + "/" + createdRooms[i].MaxPlayers);

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Join Room"))
                {
                    joiningRoom = true;

                    //Set our Player name
                    PhotonNetwork.NickName = playerName;

                    //Join the Room
                    PhotonNetwork.JoinRoom(createdRooms[i].Name);
                }
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.EndScrollView();

        //Set player name and Refresh Room button
        GUILayout.BeginHorizontal();

        GUILayout.Label("Player Name: ", GUILayout.Width(85));
        //Player name text field
        playerName = GUILayout.TextField(playerName, GUILayout.Width(250));

        GUILayout.FlexibleSpace();

        GUI.enabled = (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby || PhotonNetwork.NetworkClientState == ClientState.Disconnected) && !joiningRoom;
        if (GUILayout.Button("Refresh", GUILayout.Width(100)))
        {
            if (PhotonNetwork.IsConnected)
            {
                //Re-join Lobby to get the latest Room list
                PhotonNetwork.JoinLobby(TypedLobby.Default);
            }
            else
            {
                //We are not connected, estabilish a new connection
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        GUILayout.EndHorizontal();

        if (joiningRoom)
        {
            GUI.enabled = true;
            GUI.Label(new Rect(900 / 2 - 50, 400 / 2 - 10, 100, 20), "Connecting...");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        StartGame();
        quickjoin = false;
        Debug.Log("OnCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
        joiningRoom = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        Debug.Log("OnJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed got called. This can happen if the room is not existing or full or closed.");
        joiningRoom = false;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        //Set our player name
        PhotonNetwork.NickName = playerName;
        Chat.instance.photonView.RPC("PartyCreated", RpcTarget.All, PhotonNetwork.NickName);
		inRoom = true;
        partyJoined.SetActive(true);
        loadingParty.SetActive(false);
        UpdateHost();
        //Load the Scene called GameLevel (Make sure it's added to build settings)
    }
	public void QuickStart()
	{
        if(!inParty)
        {
            joinRandom();
        }
        else
        {
            loadingBar.joininggame = true;
            LoadingScreen.SetActive(true);
            LoadingScreen.GetComponent<Animator>().SetTrigger("swipein");
		    StartGame();
        }
	}
    public void StartTutorial()
    {
        loadingBar.joininggame = true;
        LoadingScreen.SetActive(true);
        LoadingScreen.GetComponent<Animator>().SetTrigger("swipein");
        tutorial = true;
		CreateRoom("private", 1);
    }
	public void StartGame()
	{
		ready = true;
		if(inRoom) //&&others ready
		{
        	PhotonNetwork.LoadLevel("GameScene");
        	GameManager.instance.InMain = false;
		}
	}	

    public override void OnJoinedRoom()
    {/*
		Hashtable hash = new Hashtable();
		hash.Add("characterID", gameManager.m_character.m_id);
		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Debug.Log("OnJoinedRoom");
        */
        Chat.instance.enable(true);
        PhotonNetwork.NickName = playerName;
        Chat.instance.photonView.RPC("NewPartyMember", RpcTarget.All, PhotonNetwork.NickName);
        UpdateHost();
        if(quickjoin)
        {
            StartGame();
            quickjoin = false;
        }
        if(tutorial)
        {
            tutorial = false;
            PhotonNetwork.LoadLevel("Tutorial");
        	GameManager.instance.InMain = false;
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeName(bool first)
    {
        if(newname.text == "")
        {
            PlayerPrefs.SetString("nickname", firstname.text);
        }
        else
        {
            PlayerPrefs.SetString("nickname", newname.text);
        }
        if(!first)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}