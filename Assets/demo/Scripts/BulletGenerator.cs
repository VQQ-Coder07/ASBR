using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bullet;
    public Vector2 force;
    public float waitTime;
    public Quaternion rotation;
    public Vector3 offset;
    public float difference;
    private void Start()
    {
        Invoke("Shoot", waitTime);
    }
    public void Shoot()
    {
        GameObject GO = Instantiate(bullet, this.transform.position + offset, new Quaternion(bullet.transform.rotation.x + rotation.x, bullet.transform.rotation.y + rotation.y, bullet.transform.rotation.z + rotation.z, bullet.transform.rotation.w + rotation.w));//, this.transform);
        GO.transform.localScale = bullet.transform.localScale;
        GO.GetComponent<Rigidbody2D>().AddForce(force);
        Invoke("Shoot", difference);
    }
}
