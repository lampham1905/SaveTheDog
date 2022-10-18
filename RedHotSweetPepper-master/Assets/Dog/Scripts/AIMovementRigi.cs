
using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIMovementRigi : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Collider2D collider2D;
    private MeshRenderer mesh;
    private float angle;
    private SpriteRenderer sprite;
    public bool isAttackAuto = false;
    public bool isStartAttack = false;
    float dist ;
     

    #region 
    public float minDistance;
    public  int speedAttack;
    public int speedTurnOut;
    public float timeTurnOut;
    public float timeDelay;
    public Transform target;
    #endregion
    private bool isEndGame = false;
   
    private void Start() {
        target = transform.parent.parent.GetComponent<ChuongCho>().target;
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        //LookAtTarget(target.transform);
        //sprite = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        GameManager.ins.listAgentNgu.Add(this);
        CheckDirection();
    }
    private void CheckDirection(){
        if(transform.parent.parent.GetComponent<ChuongCho>().isLeft){
            transform.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else{
            transform.transform.eulerAngles = new Vector3(0, 0, -90);
        }
    }
    private void Update() {
        if(target != null){
            dist = Vector3.Distance(target.transform.position, transform.position);
        }
        // else{
        //     Target.ins.Idle();
        // }
        if(isAttackAuto && !isEndGame){
            MoveToTarget();
        }
         if(isStartAttack){
            LookAtTarget(target.transform);
         }
         if(startAttackBool){
            if(transform.parent.parent.GetComponent<ChuongCho>().isLeft){
                float speedStartGameRandom = Random.Range(.5f, 1f);
                rb.velocity = new Vector2(-speedStartGameRandom, 0);
            }
            else{
                float speedStartGameRandom = Random.Range(.5f, 1);
                rb.velocity = new Vector2(speedStartGameRandom, 0);
            }
         }

         // Check button mouse up
        //  if(GameManager.ins.isAttack){
        //     StartGame();
        //  }
    }
    private void LookAtTarget(Transform targetEnd){
        Vector2 tXFollow = targetEnd.position - transform.position;
        Vector2 dire= (new Vector2(tXFollow.x, tXFollow.y)).normalized;
        angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90)); 
        
    }
    
    public void MoveToTarget(){
        float dis = Vector3.Distance(target.transform.position, transform.position);
        //UnityEngine.Debug.Log(dis);
        Vector3 force = target.transform.position  - transform.position;
        float speedAttackRandom = Random.Range(speedAttack, speedAttack + 10);
        rb.AddForce(force.normalized   * speedAttackRandom);
       
        //rb.AddForce(new Vector2(0, -2) * speedAttack);
    }
    private bool canTurnOut = true;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Line")){
            int randomAttack = Random.Range(0,3);
            if(randomAttack == 0){
                 float timeDelayAttackRandom = Random.Range(0f,.5f);
                 if(canTurnOut){
                StartCoroutine(TurnOut(timeDelayAttackRandom));
                canTurnOut = false;
                }   
            }
            else{
                 //UnityEngine.Debug.Log(other.gameObject.name);
                if(canTurnOut){
                StartCoroutine(TurnOut(0));
                canTurnOut = false;
                }    
            }  
        }
        if(other.gameObject.CompareTag("Target")){
            if(canTurnOut){
                StartCoroutine(TurnOutTarget());
                canTurnOut = false;
                }    
        }
        if(other.gameObject.CompareTag(Variable.trap)){
            DestroySelf();
        }
    }
   private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.CompareTag(Variable.dieArea)){
            DestroySelf();
       } 
   }
    IEnumerator TurnOut(float timeDelayAttack){
        yield return new WaitForSeconds(timeDelayAttack);
        isAttackAuto = false;
        //  Vector2 tXFollow = target.transform.position - transform.position;
        // Vector2 dire= (new Vector2(tXFollow.x, tXFollow.y)).normalized;
        // angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
        float angleRandom = Random.Range((angle - 50)/10 , (angle + 50) / 10);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRandom * 10)); 
        Vector3 force = target.transform.position - transform.position;
        float forceRandomX = Random.Range(force.x - 50f, force.x + 50f);
        float forceRandomY = Random.Range(force.y - 10f, force.y + 10f);
        //rb.AddForce(-transform.up * 1000);
        //rb.AddForce( -force.normalized * 1000);
        int speedTurnOutRandom =  Random.Range(speedTurnOut, speedTurnOut + 100);
        rb.AddForce(new Vector2(forceRandomX, - forceRandomY).normalized * speedTurnOutRandom);
        float timeTurnOutRandom = Random.Range(timeTurnOut, timeTurnOut + .1f);
        yield return new WaitForSeconds(timeTurnOutRandom);
        rb.velocity = Vector2.zero;
        float timeDelayRandom = Random.Range(timeDelay, timeDelay + .1f);
        //yield return new WaitForSeconds(timeDelayRandom);
        isAttackAuto = true;
        canTurnOut = true;
    }
    IEnumerator TurnOutTarget(){
        isAttackAuto = false;
        float angleRandom = Random.Range((angle - 50)/10 , (angle + 50) / 10);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRandom * 10)); 
        Vector3 force = target.transform.position - transform.position;
        float forceRandomX = Random.Range(force.x - 50f, force.x + 50f);
        float forceRandomY = Random.Range(force.y - 10f, force.y + 10f);
        rb.AddForce(new Vector2(forceRandomX, - forceRandomY).normalized * speedTurnOut);
        yield return new WaitForSeconds(.1f);
        isAttackAuto = true;
        canTurnOut = true;
    }
    public void StartGame(){
         collider2D.isTrigger = false;
        StartCoroutine(StartGameCor());
        //UnityEngine.Debug.Log("StartGame");
    }
    private bool startAttackBool = false;
    IEnumerator StartGameCor(){
        float timeDelayStartGame = Random.Range(0, 1f);
        yield return  new WaitForSeconds(timeDelayStartGame);
        startAttackBool = true;
        yield return new WaitForSeconds(.2f + timeDelayStartGame);
        mesh.sortingOrder = 2;
        float timeStartGameRandom = Random.Range(0f, .5f);
        yield return new WaitForSeconds(timeStartGameRandom);
        startAttackBool = false;
        rb.AddForce(new Vector2(0, 5));
        yield return new WaitForSeconds(.3f);
        //coll.isTrigger = false;
        isAttackAuto = true;
        isStartAttack = true;   
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("TargerArea")){
            //Debug.Log("Scare");
             //Target.ins.Scare();
             other.transform.parent.GetComponent<Target>().Scare();
        }
   }
   public void DestroySelf(){
        this.gameObject.SetActive(false);
   }
   public void StopEndGame(){
        isEndGame = true;
        rb.bodyType = RigidbodyType2D.Static;
        collider2D.isTrigger = true;
   }
}
    

