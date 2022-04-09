using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Ping : MonoBehaviour
{
	public Text PingText;
	private int PingValue;
	private string PingString;
	
	public void Start()
	{
		PingText.text = ("Ping:" + PingString);
		StartCoroutine(Wait(1f));
	}
	public void Repeat()
	{
		StartCoroutine(Wait(1f));
	}
	private IEnumerator Wait(float Time)
	{
		yield return new WaitForSeconds(Time);
		PingValue = PhotonNetwork.GetPing();
		PingString = PingValue.ToString();
		PingText.text = ("Ping:" + PingString);
		Repeat();
	}
}
