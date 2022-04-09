using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    public enum BotState{
        IDLE, RUN, ATTACK, DEATH
    }

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody _mybody;

    [SerializeField] BotState botState;

    [SerializeField] GameObject target;

    [SerializeField] bool targetFound;
    [SerializeField] bool attackMode;
    [SerializeField] bool FreeRoaming;
    [SerializeField] float dist;

    public GameObject Model;
    public SphereCollider GrassDetectingCollider;

    [HideInInspector]
    public bool isInsideGrass;
    public bool isOurTeam;
    public bool InWater;



    public float destPoint;

    [HideInInspector]
    private int randPos;
    public NavMeshAgent agent;

    void Start(){
        _mybody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag("Player");

        agent.autoBraking = false;

        GotoNextPoint();
    }
    void Update(){
        AI_Movement();
    }

    void SetState(BotState state){
        if (state == botState)
            return;

        if(state == BotState.IDLE){
            animator.SetTrigger("Idle");
        }
        else if(state == BotState.RUN){
            animator.SetTrigger("Run");
        }

        botState = state;
    }

    void AI_Movement(){
        if(target != null){
            dist = Vector3.Distance(transform.position, target.transform.position);
        }

        if(!targetFound && !attackMode || target == null && !targetFound && !attackMode){
            GotoNextPoint();
        }


        if(dist > 5 && attackMode){
            attackMode = false;
        }
        else if(dist <= 5 && !targetFound && !attackMode){
            targetFound = true;
        }
        else if(target != null && targetFound && !attackMode){
            transform.LookAt(target.transform.position);
            agent.destination = target.transform.position;
        }

        StopMove();
    }

    void StopMove(){
        if(dist <= agent.stoppingDistance){
            SetState(BotState.IDLE);
            targetFound = false;
            attackMode = true;
        }
    }
    void GotoNextPoint(){
        if (SceneController.instance.Stage.patrolPoint.Length == 0)
            return;

        if(!FreeRoaming){
            randPos = Random.Range(0, SceneController.instance.Stage.patrolPoint.Length);
            FreeRoaming = true;
            StartCoroutine(ReFindPos());
        }

       destPoint = Vector3.Distance(transform.position, SceneController.instance.Stage.patrolPoint[randPos].position);
        if(destPoint > 5){
            agent.destination = SceneController.instance.Stage.patrolPoint[randPos].position;
            SetState(BotState.RUN);
        }
        else if(destPoint <= 2){
            SetState(BotState.IDLE);
        }
    }

    IEnumerator ReFindPos(){
        yield return new WaitForSeconds(5f);
        FreeRoaming = false;
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
            }else{
                this.HideUnit(false);
            }
        }
    }
    public void HideUnit(bool isTrue){
        if(isTrue){
            this.Model.SetActive(false);
            //this.UIContainer.SetActive(false);
        }else{
            this.Model.SetActive(true);
            // this.UIContainer.SetActive(true);
            //this.SetState(Common.State.IDLE);
            //this.SetState(Common.State.RUN);
        }
    }
}
