using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerController : MonoBehaviour{

    private Common.State _currentState;

    #region Prefabs
    public GameObject arrowProjectile;
    public GameObject basketBall_Projectile;
    public GameObject bowlingProjectile;
    public GameObject football_Projectile;
    public GameObject frisbee_Projectile;
    public GameObject golfball_Projectile;
    public GameObject puckProjectile;
    public GameObject rubberProjectile;
    public GameObject baseball_Projectile;
    public GameObject soccer_Projectile;
    public GameObject tennisBall_Projectile;
    public GameObject volleyBall_Projectile;
    public GameObject Bullet;
    #endregion

    #region Component
    [SerializeField] NetworkAnimator animator;
    [SerializeField]private Rigidbody mybody;
    [SerializeField] GameObject Instance;

    public GameObject Model;
    public Transform AttackRangeComponent;
    public SphereCollider GrassDetectingCollider;
    public Transform rightHand, rightLeg, firePoint, hockeyHand, 
                     tableTennis_Hand, tennisHolder, arrowHand, gunhand, golfhand;
    public Transform gunsetter, arrowsetter;
    public GameObject weaponInstance;
    public GameObject AttackParticle;
    public GameObject KillParticle;
    #endregion

    [HideInInspector]
    public bool isInsideGrass;

    /* public vars */
    [HideInInspector]
    public int instanceId;
    public bool isOurTeam;
    public bool isKilled;
    public bool InWater;

    public string playerName;
    public float speed = 2f;
    public float attackRange = 4f;

    public float init_speed;
    public float init_attackRange;

    public float healthPoints = 10;
    public float hitPoints = 2;
    public float reloadTime = 1;
    [HideInInspector]
    public float currentHealth;
    public int waterColliders;
    [SerializeField] private bool _isCollidingWithObstacle;
    [SerializeField] private Vector3 _collisionVector;
    [SerializeField] private bool _isMoving;
    [SerializeField] public bool isSoccer, isBasketball, isFootball, isAirsoft, isArrow, isFrisbee,
                                 isHockey, isGolf, isTableTennis, isTennis, isBaseball, isVolleyball, isBowling;

    void Start(){
        mybody = GetComponent<Rigidbody>();

        CameraController.instance.target = transform;
        init_attackRange = attackRange;
        init_speed = speed;
    }

    public void SetState(Common.State state){
        if(this.animator == null)
            return;

        if (state == _currentState)
            return;

        if (state == Common.State.IDLE)
            this.animator.SetTrigger("idle");
        else if (state == Common.State.RUN)
            this.animator.SetTrigger("run");
        else if (state == Common.State.ATTACK){
            if (isSoccer){
                animator.SetTrigger("AttackFive");
            }
            else if (isFootball){
                animator.SetTrigger("AttackSeven");
            }
            else if (isBasketball){
                animator.SetTrigger("AttackFour");
            }
            else if (isArrow){
                animator.SetTrigger("AttackOne");
            }
            else if (isAirsoft){
                animator.SetTrigger("Shot");
            }
            else if (isFrisbee){
                animator.SetTrigger("AttackThree");
            }
            else if (isHockey){
                animator.SetTrigger("AttackSix");
            }
            else if (isGolf){
                animator.SetTrigger("AttackSix");
            }
            else if (isTableTennis){
                animator.SetTrigger("AttackFour");
            }
            else if (isTennis){
                animator.SetTrigger("AttackSeven");
            }
            else if (isBaseball){
                animator.SetTrigger("AttackTwo");
            }
            else if (isVolleyball){
                animator.SetTrigger("AttackFour");
            }
            else if (isBowling){
                animator.SetTrigger("AttackEight");
            }
        }

        this._currentState = state;

        //Debug.Log(state);
    }
    public void LookAt(Vector3 direction){
        transform.rotation = Quaternion.LookRotation(direction);
    }
    public void Move(Vector3 moveVector){
        if (_isCollidingWithObstacle){
            float angleBetweenCollisionAndVector = Vector3.Angle(_collisionVector, moveVector);
            if (angleBetweenCollisionAndVector >120){
                if(_collisionVector.x == 0){
                    moveVector.z = 0;
                    moveVector.x = Mathf.Sign(moveVector.x) * 1f;
                }else{
                    moveVector.z = Mathf.Sign(moveVector.z) * 1f;
                    moveVector.x = 0;
                }
            }
        }

        if (!aiming)
        {
            LookAt(moveVector);
        }
        transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

        if (!aiming)
        {
            this._aimDirectionVector = this.transform.forward;
        }

        this.SetState(Common.State.RUN);
        _isMoving = true;
    }

    [SerializeField]
    public bool aiming;
    public void StopMove(){
        if(this._isMoving){
            this._isMoving = false;
            this.SetState(Common.State.IDLE);
        }
    }
    [SerializeField]
    public Vector3 _aimDirectionVector = new Vector3(0, 0, 1);
    public void Aim(Vector3 directionVector){
        if(!this.InWater)
        {
            aiming = true;
            this.AttackRangeComponent.gameObject.SetActive(true);
            this.AttackRangeComponent.transform.GetChild(0).gameObject.SetActive(true);
            _aimDirectionVector = directionVector;
            this.AttackRangeComponent.rotation = Quaternion.LookRotation(_aimDirectionVector);
            transform.rotation = Quaternion.LookRotation(_aimDirectionVector);

            float distanceToObstacle = attackRange;
            RaycastHit hit;
            Vector3 unitCenterPos = transform.position + new Vector3(0, 0.5f, 0);
            Ray ray = new Ray(unitCenterPos, _aimDirectionVector);
            if(Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Obstacle")){
                float distance = Vector3.Distance(hit.point, unitCenterPos);
                if (distance < distanceToObstacle)
                    distanceToObstacle = distance; 
            }

            this.AttackRangeComponent.transform.localScale = new Vector3(1, 1, distanceToObstacle);
        }
    }
    public void StopAim()
    {
        aiming = false;
        this.AttackRangeComponent.gameObject.SetActive(false);
        this.AttackRangeComponent.transform.GetChild(0).gameObject.SetActive(false);
    }

    private float _lastShootedTime;
    public void Attack(){
        if (!InWater){

            this.SetState(Common.State.ATTACK);
            if(Time.time - _lastShootedTime > reloadTime)
            {
                Shoot();
                this._lastShootedTime = Time.time;
                StartCoroutine(_StopAttack());
            }
        }
    }
    private void DoDamage()
    {
        this.GetComponent<NetworkSync>().Damage(transform.GetChild(1).GetChild(1).GetComponent<InTrigger>().targets[0]);
    }

    private IEnumerator _StopAttack(){
        yield return new WaitForSeconds(0.9f);
        this.SetState(Common.State.IDLE);
        Destroy(weaponInstance);
    }
    public void Shoot(){
        MilkShake.IShakeParameters shakeParams = new MilkShake.ShakeParameters();
        shakeParams.ShakeType = MilkShake.ShakeType.OneShot;
        shakeParams.Strength = 0.2f;
        shakeParams.Roughness = 10f;
        shakeParams.FadeIn = 0.05f;
        shakeParams.FadeOut = 0.2f;
        shakeParams.PositionInfluence = new Vector3(0.1f, 0.1f, 0.1f);
        shakeParams.RotationInfluence = Vector3.one * 2;
        MilkShake.Shaker.ShakeAllSeparate(shakeParams, null, 0);
        float distanceToObstacle = attackRange;
        RaycastHit hit;
        Vector3 unitCenterPos = transform.position + new Vector3(0, 0.5f, 0);
        Ray ray = new Ray(unitCenterPos, _aimDirectionVector);
        if(Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Obstacle")){
            float distance = Vector3.Distance(hit.point, unitCenterPos);
            if(distance < distanceToObstacle)
                distanceToObstacle = distance;
        }
        if (isSoccer){
            GameObject soccer = (GameObject)PhotonNetwork.Instantiate(this.soccer_Projectile.name, rightLeg.position, Quaternion.identity) as GameObject;
            soccer.GetComponent<BulletCtrl>().photonView.RPC("BulletCtrl", RpcTarget.All, _aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            soccer.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isFootball){
            GameObject football = (GameObject)Instantiate(this.football_Projectile, rightHand.position, Quaternion.identity) as GameObject;
            football.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isBasketball){
            GameObject basketball =(GameObject)Instantiate(this.basketBall_Projectile, rightHand.position, Quaternion.identity)as GameObject;
            basketball.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isBowling){
            GameObject bowling = (GameObject)Instantiate(this.bowlingProjectile, rightHand.position, Quaternion.identity) as GameObject;
            bowling.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isFrisbee){
            GameObject frisbee = (GameObject)Instantiate(this.frisbee_Projectile, rightHand.position, Quaternion.identity) as GameObject;
            frisbee.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isHockey){
            GameObject hockey = (GameObject)Instantiate(this.puckProjectile, rightLeg.position, Quaternion.identity) as GameObject;
            hockey.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isGolf){
            GameObject golf = (GameObject)Instantiate(this.golfball_Projectile, rightLeg.position, Quaternion.identity) as GameObject;
            golf.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isBaseball){
            GameObject baseball = (GameObject)Instantiate(this.baseball_Projectile, rightHand.position, Quaternion.identity) as GameObject;
            baseball.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isTennis){
            GameObject tennisball = (GameObject)Instantiate(this.tennisBall_Projectile, rightHand.position, Quaternion.identity) as GameObject;
            tennisball.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isTableTennis){
            GameObject tableTennis = (GameObject)Instantiate(this.rubberProjectile, firePoint.position, Quaternion.identity) as GameObject;
            tableTennis.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);
            Debug.Log("Spawn");
        }
        else if (isArrow){
            weaponInstance = (GameObject)Instantiate(SceneController.instance.ArrowWeapon, arrowsetter.position, arrowsetter.rotation) as GameObject;
            weaponInstance.transform.parent = arrowsetter;
            GameObject arrow = (GameObject)Instantiate(this.Bullet, arrowsetter.position, Quaternion.identity) as GameObject;
            arrow.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 2f, distanceToObstacle, this.hitPoints, this.isOurTeam);

            GameObject particle = (GameObject)Instantiate(AttackParticle, firePoint.position, Quaternion.identity) as GameObject;
            particle.transform.rotation = Quaternion.LookRotation(_aimDirectionVector);
            Destroy(particle, distanceToObstacle / 30);


            Debug.Log("Spawn");
        }
        else if (isAirsoft){
            weaponInstance = (GameObject)Instantiate(SceneController.instance.AirosftWeapon, gunsetter.position, gunsetter.rotation) as GameObject;
            weaponInstance.transform.parent = gunsetter;
            GameObject bullet = (GameObject)Instantiate(this.Bullet, rightHand.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<BulletCtrl>().SetData(_aimDirectionVector.normalized, 30f, distanceToObstacle, this.hitPoints, this.isOurTeam);

            GameObject particle = (GameObject)Instantiate(AttackParticle, firePoint.position, Quaternion.identity) as GameObject;
            particle.transform.rotation = Quaternion.LookRotation(_aimDirectionVector);
            Destroy(particle, distanceToObstacle / 30);

            Debug.Log("Spawn");
        }
    }
    private int _grassEnterCounter;
    public void OnUnitEnterGrass(){
        this._grassEnterCounter++;
        if (this._grassEnterCounter > 0)
            this.isInsideGrass = true;

        this.Refresh();
    }

    public void OnUnitExitGrass(){
        this._grassEnterCounter--;
        if (this._grassEnterCounter <= 0)
            this.isInsideGrass = false;

        this.Refresh();

    }

    public void Refresh(){
        if(!this.isOurTeam){
            if(isInsideGrass){
                this.HideUnit(true);
            }
            else{
                this.HideUnit(false);
            }
        }
    }
    public void HideUnit(bool isTrue){
        if(isTrue){
            this.Model.SetActive(false);
            //this.UIContainer.SetActive(false);
        }
        else{
            this.Model.SetActive(true);
           // this.UIContainer.SetActive(true);
            //this.SetState(Common.State.IDLE);
            //this.SetState(Common.State.RUN);
        }
    }
    public void Kill(){
       // this.DropItem();
        this.isKilled = true;
        //this.PlayKillParticle();
        //this.Respawn();
    }
    public void OnReceiveHit(float hitPoints){
        this.currentHealth -= hitPoints;
        if (this.currentHealth <= 0)
            this.Kill();

        //this._itemUI.SetEnergyBarProgress(this.currentHealth / this.healthPoints);
    }

    #region Triggers And Collisions

    void OnCollisionExit(Collision collision){
        this._isCollidingWithObstacle = false;
    }

    void OnCollisionStay(Collision collision){
        this._isCollidingWithObstacle = true;
        this._collisionVector = collision.contacts[0].normal;
    }
    private void OnTriggerEnter(Collider other){
        //if (other.gameObject.tag == "Soccer"){
        //    if(SceneController.instance.currentWeapon == null){
        //        Instance = (GameObject)Instantiate(SceneController.instance.SoccerWeapon, rightHand.position, rightHand.rotation);
        //        Instance.transform.SetParent(rightHand);
        //        SceneController.instance.currentWeapon = Instance;
        //        if(other.gameObject.tag == GameManager.instance.weaponOne_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        //        }
        //        else if(other.gameObject.tag == GameManager.instance.weaponTwo_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        //        }
        //        if(other.gameObject.tag == GameManager.instance.weaponThree_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        //        }
        //        Destroy(other.gameObject);
        //    }
        //    else if(SceneController.instance.currentWeapon != null){
        //        Destroy(Instance);
        //        Instance = (GameObject)Instantiate(SceneController.instance.SoccerWeapon, rightHand.position, rightHand.rotation);
        //        Instance.transform.SetParent(rightHand);
        //        SceneController.instance.currentWeapon = Instance;
        //        if(other.gameObject.tag == GameManager.instance.weaponOne_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        //        }
        //        else if(other.gameObject.tag == GameManager.instance.weaponTwo_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        //        }
        //        if(other.gameObject.tag == GameManager.instance.weaponThree_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        //        }
        //        Destroy(other.gameObject);
        //    }
        //}
        //else if(other.gameObject.tag == "Football"){
        //    if(SceneController.instance.currentWeapon == null){
        //        Instance = (GameObject)Instantiate(SceneController.instance.FootballWeapon, rightHand.position, rightHand.rotation);
        //        Instance.transform.SetParent(rightHand);
        //        SceneController.instance.currentWeapon = Instance;
        //        if(other.gameObject.tag == GameManager.instance.weaponOne_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        //        }
        //        else if(other.gameObject.tag == GameManager.instance.weaponTwo_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        //        }
        //        if(other.gameObject.tag == GameManager.instance.weaponThree_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        //        }
        //        Destroy(other.gameObject);
        //    }
        //    else if(SceneController.instance.currentWeapon != null){
        //        Destroy(Instance);
        //        Instance = (GameObject)Instantiate(SceneController.instance.FootballWeapon, rightHand.position, rightHand.rotation);
        //        Instance.transform.SetParent(rightHand);
        //        SceneController.instance.currentWeapon = Instance;
        //        if(other.gameObject.tag == GameManager.instance.weaponOne_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        //        }
        //        else if(other.gameObject.tag == GameManager.instance.weaponTwo_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        //        }
        //        if(other.gameObject.tag == GameManager.instance.weaponThree_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        //        }
        //        Destroy(other.gameObject);
        //    }
        //}
        //if(other.gameObject.tag == "BasketBall"){
        //    if(SceneController.instance.currentWeapon == null){
        //        Instance = (GameObject)Instantiate(SceneController.instance.BasketWeapon, rightHand.position, rightHand.rotation);
        //        Instance.transform.SetParent(rightHand);
        //        SceneController.instance.currentWeapon = Instance;
        //        if(other.gameObject.tag == GameManager.instance.weaponOne_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        //        }
        //        else if(other.gameObject.tag == GameManager.instance.weaponTwo_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        //        }
        //        if(other.gameObject.tag == GameManager.instance.weaponThree_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        //        }
        //        Destroy(other.gameObject);
        //    }
        //    else if(SceneController.instance.currentWeapon != null){
        //        SceneController.instance.currentWeapon = Instance;
        //        if(other.gameObject.tag == GameManager.instance.weaponOne_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        //        }
        //        else if(other.gameObject.tag == GameManager.instance.weaponTwo_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        //        }
        //        if(other.gameObject.tag == GameManager.instance.weaponThree_Name){
        //            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        //        }
        //        Destroy(other.gameObject);
        //    }
        //}
    }
    private void OnTriggerStay(Collider other){
        
    }
    private void OnTriggerExit(Collider other){
        
    }
    #endregion
}
