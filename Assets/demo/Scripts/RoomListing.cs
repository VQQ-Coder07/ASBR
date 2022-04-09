using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviour
{
    
    public Text RoomNameText;
    public Text RoomPlayers;
    public RoomInfo cRoom;
    
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        if(roomInfo.MaxPlayers <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    	RoomNameText.text = roomInfo.Name;
    	RoomPlayers.text = roomInfo.PlayerCount.ToString() + "/" + roomInfo.MaxPlayers.ToString();
    	cRoom = roomInfo;
    }
    public void Join()
    {
	    PhotonNetwork.JoinRoom(cRoom.Name);
    }

}
