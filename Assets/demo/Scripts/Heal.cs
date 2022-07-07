using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    public float hp = 0;
    public Slider HealBar;
    public float Life = 100;
    public float hpToTake = 20;
    public GameObject bloodEffect;
    public GameObject explosion;
    public MonoBehaviour[] scriptsToDisable;
    public GameObject[] objectsToDisable;
    [HideInInspector]
    public bool isMine;
    public bool AI;
    public float waitTillExplode;
    public void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.LogError("COLLIISION");
        if(other.gameObject.CompareTag("weapon") && (isMine || AI))
        {
            //Debug.LogError("COLLIISIONBYPLAYER");
            if(hp < Life)
            {
                if(isMine)Camera.main.gameObject.GetComponent<CameraShake>().ShakeCamera(3f, 0.2f);
                hp += hpToTake;
                HealBar.value = hp;
                Instantiate(bloodEffect, this.transform.position, Quaternion.identity, this.gameObject.transform);
            }
            if(hp >= Life)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        if(this.GetComponent<Rigidbody2D>() != null)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        if(this.GetComponent<LineRenderer>() != null) this.GetComponent<LineRenderer>().enabled = false;
        if(this.GetComponent<SpriteRenderer>().enabled != null)this.GetComponent<SpriteRenderer>().enabled = false;
        if(this.GetComponent<SpriteRenderer>().enabled != null)this.GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Invoke("DestroyH", waitTillExplode);
        foreach (MonoBehaviour m in scriptsToDisable)
        {
            m.enabled = false;
        }
        foreach (GameObject g in objectsToDisable)
        {
            g.SetActive(false);
        }
    }
    public void DestroyH()
    {
        Destroy(this.gameObject);
    }
    public void SetEnemyHp(float newhp)
    {
        if(hp < Life)
        {
            //hpToTake = newhp;
            hp = Life - newhp;
            HealBar.value = hp;
            Instantiate(bloodEffect, this.transform.position, Quaternion.identity, this.gameObject.transform);
        }
        else if(hp >= Life)
        {
            Die();
        }
    }
}
