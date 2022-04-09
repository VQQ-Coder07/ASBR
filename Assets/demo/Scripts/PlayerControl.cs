using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject spawnParticles;
    public TrajectoryLine tl;
    public float power = 10f;
    public Rigidbody2D rb;
    public float rotationSpeed = 5;
    public Vector2 minPower;
    public Vector2 maxPower;
    public bool Ready = true;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Vector2 force;
    [SerializeField]
    Vector3 startPoint;
    [SerializeField]
    Vector3 endPoint;
    public bool rotation = false;
    private float CamSize;
    public float smoothLerp = 5f;
    private float yVelocity = 0f;
    public float velocityDivision = 3f;
    public GameObject[] objectsToDisable;
    public MonoBehaviour[] scriptsToDisable;
    public List<GameObject> usedSpawnPoints;
    public GameObject explosion;
    private bool dead;

    public void AddSpawnPoint(GameObject obToAdd)
    {
        usedSpawnPoints.Add(obToAdd);
    }
    private void Start()
    {
        Instantiate(spawnParticles, this.transform.position, Quaternion.identity);
    }
    private void Awake()
    {
        tl = this.gameObject.GetComponent<TrajectoryLine>();
        cam = Camera.main;
        CamSize = cam.orthographicSize;
        this.GetComponent<LineRenderer>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
        foreach (MonoBehaviour m in scriptsToDisable)
        {
            m.enabled = true;
        }
        foreach (GameObject g in objectsToDisable)
        {
            g.SetActive(true);
        }
        if(this.GetComponent<Rigidbody2D>() != null)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void Update()
    {
        //Debug.LogError(rb.velocity);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, CamSize + Mathf.Abs((rb.velocity.x + rb.velocity.y)/ velocityDivision), ref yVelocity, smoothLerp);
        if (Input.GetMouseButton(0) && Ready)
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            tl.RenderLine(this.gameObject.transform.position, currentPoint);
        }
        if (Input.GetMouseButtonDown(0) && Ready)
        {
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }
        if (Input.GetMouseButtonUp(0) && Ready)
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            rotation = true;
            force = new Vector2(Mathf.Clamp(endPoint.x - startPoint.x, minPower.x, maxPower.x), Mathf.Clamp(endPoint.y - startPoint.y, minPower.y, maxPower.y));
            if(force.x < 0)
            {
                this.transform.localScale = new Vector3(-0.3f, 0.3f);
            }
            else if(force.x > 0)
            {
                this.transform.localScale = new Vector3(0.3f, 0.3f);
            }
            rb.AddForce(force * power, ForceMode2D.Impulse);
            tl.EndLine();
            Ready = false;
        }
        if (rotation == true)
        {
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cam.ScreenToWorldPoint(Input.mousePosition)), Time.deltaTime * rotationSpeed);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.LogError("up");
        if(this.transform.GetComponent<Rigidbody2D>() != null)
        {
            this.transform.GetComponent<Rigidbody2D>().velocity = Vector3.Reflect(other.relativeVelocity*-0.4f, other.contacts[0].normal);
        }
        rotation = false;
        if(other.gameObject.CompareTag("Wall"))
        {
            transform.rotation = Quaternion.identity;
            if(force.x < 0)
            {
                this.transform.localScale = new Vector3(0.3f, 0.3f);
            }
            else if(force.x > 0)
            {
                this.transform.localScale = new Vector3(-0.3f, 0.3f);
            }
            Ready = true;
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            transform.rotation = Quaternion.identity;
            Ready = true;
        }
        if(other.gameObject.CompareTag("obstacle"))
        {
            if(this.GetComponent<Rigidbody2D>() != null)
            {
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            this.GetComponent<LineRenderer>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            foreach (MonoBehaviour m in scriptsToDisable)
            {
                m.enabled = false;
            }
            foreach (GameObject g in objectsToDisable)
            {
                g.SetActive(false);
            }
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Invoke("DestroyOb", 0.5f);
        }
    }
    public void OnDownCollisionEnter2D(Collision2D other)
    {
        //Debug.LogError("down");
        /*if(this.transform.GetComponent<Rigidbody2D>() != null)
        {
            this.transform.GetComponent<Rigidbody2D>().velocity = Vector3.Reflect(other.relativeVelocity*-0.4f, other.contacts[0].normal);
        }*/

        rotation = false;
        if(other.gameObject.CompareTag("Ground"))
        {
            transform.rotation = Quaternion.identity;
            Ready = true;
        }
        if(other.gameObject.CompareTag("obstacle"))
        {
            if(this.GetComponent<Rigidbody2D>() != null)
            {
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            this.GetComponent<LineRenderer>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            foreach (MonoBehaviour m in scriptsToDisable)
            {
                m.enabled = false;
            }
            foreach (GameObject g in objectsToDisable)
            {
                g.SetActive(false);
            }
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Invoke("DestroyOb", 0.5f);
        }
    }
    void DestroyOb()
    {
        //Debug.LogError(usedSpawnPoints.Count);
        if(!dead)
        {
            GameObject GO = Instantiate(this.gameObject, usedSpawnPoints[usedSpawnPoints.Count - 1].transform.GetChild(0).position, Quaternion.identity);
            GO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            cam.gameObject.GetComponent<CameraFollow>().Reset();
            Destroy(this.gameObject);
            dead = true;
        }
    }
    public void OnCollisionStay2D(Collision2D other)
    {
        Ready = true;
    }
}