using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class NetworkSync : MonoBehaviourPun
{
    public Slider bar;
    public int health;
    public bool dead;
    [PunRPC]

    private void Awake()
    {
        health = 200;
    }
    private void Update()
    {
        if(health <= 0 && !dead)
        {
            Instantiate(GameVariables.refs.deathparts, this.transform.position, Quaternion.identity);
            if(this.GetComponent<PhotonView>().IsMine)
            {
                    for(int i = 0; i < this.transform.childCount; i++)
                    {
                        this.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    GameVariables.refs.mainscr.SetActive(false);
                    StartCoroutine(die());
            }
            dead = true;
        }
    }
    private IEnumerator die()
    {
        yield return new WaitForSeconds(1f);
        GameVariables.refs.endscr.SetActive(true);
    }
    public void ModifyHealth(int id, int value)
    {
        if(this.GetComponent<PhotonView>().ViewID == id)
        {
            health += value;
            UpdateHealthBar();        
        }

    }
    private void UpdateHealthBar()
    {
        bar.value = health;
    }
    public void Damage(GameObject player)
    {
        int dmg = -50;
        int id = this.GetComponent<PhotonView>().ViewID;
        photonView.RPC("ModifyHealth", RpcTarget.All, id, dmg);
    }
}
