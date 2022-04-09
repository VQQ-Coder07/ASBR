using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class pmanager : MonoBehaviour
{
    public Text ping;
    private void FixedUpdate()
    {
        ping.text = "PING: " + PhotonNetwork.GetPing().ToString();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenuOne");
    }
}
