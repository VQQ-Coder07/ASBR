using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkAnimator : MonoBehaviourPun
{
    public Animator animator;

    [PunRPC]
    public void SendTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void SetTrigger(string name)
    {
        //Debug.LogError("SDE");
        //animator.SetTrigger(name);
        photonView.RPC("SendTrigger", RpcTarget.All, name);
    }
}
