using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public Transform[] spawnPoint;
    public int index;

    public bool _sp1;
    public bool _sp2;
    private GameObject Player;
    public static PUN2_RoomController instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
			//Debug.LogError("==null");
            //Debug.Log("Is not in the room, returning back to Lobby");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuOne");
            return;
        }
        if(PhotonNetwork.PlayerList.Length < 2)
        {/*
			Debug.LogError(Player);
			Player.GetComponent<PUN2_PlayerSync>().isMine = true;
            Destroy(spawnPoint[index].gameObject);
            Camera.main.gameObject.GetComponent<CameraFollow>().Ready(Player.transform);*/
        }
        if(PhotonNetwork.PlayerList.Length == 2)
        {
			Debug.LogError("==2");
            TryAgain();
        }
    }
	public GameObject SpawnPlayer(GameObject playerPrefab)
	{
        //index = Random.Range(0, 2);
        Player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint[0].position, Quaternion.identity, 0);
		return Player;
	}
    void Update()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
        {
            StartCoroutine(Wait(3));
        }
    }
    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }

        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }
    void TryAgain()
    {
        index = Random.Range(0, 2);
        if (spawnPoint[index] != null)
        {
            Player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint[index].position, Quaternion.identity, 0);
        //    Camera.main.gameObject.GetComponent<CameraFollow>().Ready(Player.transform);
        //    Player.gameObject.GetComponent<PlayerControl>().enabled = false;
        //    Player.gameObject.GetComponent<TrajectoryLine>().enabled = false;
            Destroy(spawnPoint[index].gameObject);
            Camera.main.gameObject.GetComponent<CameraFollow>().Ready(Player.transform);
            StartCoroutine(Wait(3));
        }
        else if (spawnPoint[index] == null)
        {
            TryAgain();
        }
    }
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Player.GetComponent<PlayerControl>().enabled = true;
        Player.GetComponent<TrajectoryLine>().enabled = true;
    }
}
