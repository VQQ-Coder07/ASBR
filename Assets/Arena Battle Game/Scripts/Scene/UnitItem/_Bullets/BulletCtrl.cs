using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class BulletCtrl : MonoBehaviourPun, IPunObservable
{

    private bool _isOurTeam;
    private float _hitPoints;
    private float _velocity;
    private Vector3 _moveDirection;
    private float _maxDistance;

    private bool _isStarted, isMoving;
    Vector3 latestPos;
    Quaternion latestRot;
    private void Start()
    {
        this.gameObject.tag = "Bullet";
    }
    [PunRPC]
    public void SetData(Vector3 moveDirection, float velocity, float maxDistance, float hitPoints, bool isOurTeam){

        this._moveDirection = moveDirection;
        this._velocity = velocity;
        this._maxDistance = maxDistance;
        this._hitPoints = hitPoints;
        this._isOurTeam = isOurTeam;

        this._isStarted = true; 
        Destroy(this.gameObject, maxDistance / velocity);
    }

    private void Update(){
        //if(isMoving){
        //    this.transform.Translate(transform.forward * 1f * Time.deltaTime);
        //}
        if (!this._isStarted)
            return;

        this.transform.Translate(this._moveDirection * this._velocity * Time.deltaTime);
        if (!photonView.IsMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
        }
    }
    public void ForwardPoint(){
        isMoving = true;
    }
    private void OnTriggerEnter(Collider other){
        UnitItemBaseCtrl unit = other.GetComponent<UnitItemBaseCtrl>();
        if(unit != null && unit.isOurTeam != _isOurTeam)
        {
            unit.OnReceiveHit(this._hitPoints);
            Destroy(this.gameObject);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
