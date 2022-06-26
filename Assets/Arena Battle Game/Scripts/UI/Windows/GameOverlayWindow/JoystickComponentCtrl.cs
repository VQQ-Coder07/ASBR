using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JoystickComponentCtrl : JoystickCtrl {

    private bool reloading;
    public bool attack;
    public bool hide;
    /* component refs */
    [SerializeField] public RectTransform Container;

    /* private vars */
    private Vector2 _joystickCenter = Vector2.zero;
    private Vector3 _containerDefaultPosition;

    public UnityEvent OnTap;

    void Start(){
        this._containerDefaultPosition = this.Container.position;
    }

    public override void OnDrag(PointerEventData eventData){
        if(!reloading)
        {
            Vector2 direction = eventData.position - _joystickCenter;
            inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
            handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        }
    }

    private void reload()
    {
        if(attack)
        {
            this.GetComponent<Animator>().SetTrigger("cooldown");
            reloading = true;
            this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("reloading");
            Invoke("reloaded", 2.1f);
        }
    }
    private void reloaded()
    {
        reloading = false;
    }
    public override void OnPointerDown(PointerEventData eventData){
        if(!reloading)
        {
            Container.position = eventData.position;
            handle.anchoredPosition = Vector2.zero;
            _joystickCenter = eventData.position;
            if(attack)
            {
                this.GetComponent<Image>().enabled = false;
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
            if(sprite != null)
            {
                Background.SetActive(true);
                Handle.SetActive(true);
                if(sprite.sprite.name == "Soccer"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.SoccerWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().rightLeg.position, SceneController.instance.Instance.GetComponent<PlayerController>().rightLeg.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightLeg;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isSoccer = true;
                }
                else if(sprite.sprite.name == "Football"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.FootballWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isFootball = true;
                }
                else if(sprite.sprite.name == "basketball"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.BasketWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isBasketball = true;
                }
                else if(sprite.sprite.name == "Airsoft"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.AirosftWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().gunhand.position, SceneController.instance.Instance.GetComponent<PlayerController>().gunhand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().gunhand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isAirsoft = true;
                }
                else if(sprite.sprite.name == "Archery"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.ArrowWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().arrowHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().arrowHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().arrowHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isArrow = true;
                }
                else if(sprite.sprite.name == "Frisbee"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.FrisbeeWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isFrisbee = true;
                }
                else if(sprite.sprite.name == "Hockey"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.HockeyWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().hockeyHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().hockeyHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().hockeyHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isHockey = true;
                }
                else if(sprite.sprite.name == "Golf"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.GolfWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().golfhand.position, SceneController.instance.Instance.GetComponent<PlayerController>().golfhand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().golfhand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isGolf = true;
                }
                else if(sprite.sprite.name == "Tabletennis"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.TableTennisWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().tableTennis_Hand.position, SceneController.instance.Instance.GetComponent<PlayerController>().tableTennis_Hand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isTableTennis = true;
                }
                else if(sprite.sprite.name == "Tennis"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.TennisWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().tennisHolder.position, SceneController.instance.Instance.GetComponent<PlayerController>().tennisHolder.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().tennisHolder;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isTennis = true;
                }
                else if(sprite.sprite.name == "Baseball"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.BaseballWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().tableTennis_Hand.position, SceneController.instance.Instance.GetComponent<PlayerController>().tableTennis_Hand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().tableTennis_Hand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isBaseball = true;
                }
                else if(sprite.sprite.name == "Volleyball"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.VolleyWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isVolleyball = true;
                }
                else if(sprite.sprite.name == "Bowling"){
                    SceneController.instance.WeaponInstance = (GameObject)Instantiate(SceneController.instance.BowlingWeapon, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.position, SceneController.instance.Instance.GetComponent<PlayerController>().rightHand.rotation) as GameObject;
                    SceneController.instance.WeaponInstance.transform.parent = SceneController.instance.Instance.GetComponent<PlayerController>().rightHand;
                    SceneController.instance.Instance.GetComponent<PlayerController>().isBowling = true;
                }
            }else{
                Debug.Log("There is no sprite");
            }
        }
    }

    public override void OnPointerUp(PointerEventData eventData){
        if(!reloading)
        {
            if(attack)
            {
                this.GetComponent<Image>().enabled = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }
            if(sprite != null && hide){
                Background.SetActive(false);
                Handle.SetActive(false);
            }
            else{
                Debug.Log("There is no sprite");
            }
            if (SceneController.instance.Instance.GetComponent<PlayerController>().isFootball){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isSoccer){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isBasketball){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isBowling){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isFrisbee){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if(SceneController.instance.Instance.GetComponent<PlayerController>().isAirsoft){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isArrow){
                Destroy(SceneController.instance.WeaponInstance);
            }
            else if(SceneController.instance.Instance.GetComponent<PlayerController>().isHockey){
                StartCoroutine(enumerator());
            }
            else if(SceneController.instance.Instance.GetComponent<PlayerController>().isGolf){
                StartCoroutine(enumerator());
            }
            else if(SceneController.instance.Instance.GetComponent<PlayerController>().isBaseball){
                StartCoroutine(enumerator());
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isTableTennis){
                StartCoroutine(enumerator());
            }
            else if (SceneController.instance.Instance.GetComponent<PlayerController>().isTennis){
                StartCoroutine(enumerator());
            }
            if(Mathf.Abs(inputVector.x) > 0.2 || Mathf.Abs(inputVector.y) > 0.2)
            {
                //Debug.LogError("!!!");
                this.OnTap.Invoke();
            }
            Container.position = this._containerDefaultPosition;
            handle.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
            reload();
        }
    }

    IEnumerator enumerator() {
        yield return new WaitForSeconds(0.9f);
        Destroy(SceneController.instance.WeaponInstance);
    }
}
