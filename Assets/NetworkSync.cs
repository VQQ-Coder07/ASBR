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
    public bool ab10;
    [PunRPC]

    private void Awake()
    {
        //Debug.LogError(gameObject);
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
                MilkShake.IShakeParameters shakeParams = new MilkShake.ShakeParameters();
                shakeParams.ShakeType = MilkShake.ShakeType.OneShot;
                shakeParams.Strength = 0.1f;
                shakeParams.Roughness = 8f;
                shakeParams.FadeIn = 0.05f;
                shakeParams.FadeOut = 0.5f;
                shakeParams.PositionInfluence = new Vector3(0.5f, 0.5f, 0.5f);
                shakeParams.RotationInfluence = Vector3.one * 5;
                MilkShake.Shaker.ShakeAllSeparate(shakeParams, null, 0);
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
        //if(this.GetComponent<PhotonView>().ViewID == id)
        if(ab10 && value < 0)
        {
            health += value/ 2;
        }
        if(!ab10)
        {
            health += value;
        }
        UpdateHealthBar();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            MilkShake.IShakeParameters shakeParams = new MilkShake.ShakeParameters();
            shakeParams.ShakeType = MilkShake.ShakeType.OneShot;
            shakeParams.Strength = 0.05f;
            shakeParams.Roughness = 8f;
            shakeParams.FadeIn = 0.05f;
            shakeParams.FadeOut = 0.3f;
            shakeParams.PositionInfluence = new Vector3(0.5f, 0.5f, 0.5f);
            shakeParams.RotationInfluence = Vector3.one * 5;
            MilkShake.Shaker.ShakeAllSeparate(shakeParams, null, 0);
            health += -10;
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
