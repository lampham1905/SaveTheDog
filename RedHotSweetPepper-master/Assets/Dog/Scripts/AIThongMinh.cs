
using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
public class AIThongMinh : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private AIPath aIPath;
    private Collider2D collider2D;
    private MeshRenderer mesh;
    public Transform target; 
    public bool canTurnOut = true;
    public bool isAttackAuto = false;
    public bool isStartAttack = false;
    public int speedTurnOut;
    public float timeTurnOut;
    public float timeDelay;
    public float minDistance;
    float dist;
    private float angle;
    private bool isEndGame = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AIDestinationSetter>().target = transform.parent.parent.GetComponent<ChuongCho>().target;
        //target = Target.ins.gameObject.transform;
        target = transform.parent.parent.GetComponent<ChuongCho>().target;
        rb = GetComponent<Rigidbody2D>();
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        aIPath = GetComponent<AIPath>();
       collider2D = GetComponent<Collider2D>();
       GameManager.ins.listAgentThongMinh.Add(this);
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

    // Update is called once per frame
    void Update()
    {
         if(isAttackAuto && !isEndGame){
            aIPath.canMove = true;
        }
        else if(!isAttackAuto || isEndGame){
            aIPath.canMove = false;
        }
        if(isStartAttack){
            transform.GetChild(0).GetComponent<FollowTarger>().LookAtTarget();
        }
        dist = Vector3.Distance(target.transform.position, transform.position);
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


         // Check mouse button up
        //  if(GameManager.ins.isAttack){
        //     StartGame();
        //  }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Line")){
            if(canTurnOut){
                StartCoroutine(TurnOut(0f));
                canTurnOut = false;
            }
        }
        if(other.gameObject.CompareTag("Target")){
            if(canTurnOut){
                StartCoroutine(TurnOutTarget());
                canTurnOut = false;
        }}
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
        // float timeDelayAttackRandom = Random.Range(timeDelayAttack, timeDelayAttack + .5f);
        // yield return new WaitForSeconds(timeDelayAttackRandom);
        isAttackAuto = false;
        // float angleRandom = Random.Range((angle - 50)/10 , (angle + 50) / 10);
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRandom * 10)); 
        Vector3 force = target.transform.position - transform.position;
        float forceRandomX = Random.Range(force.x - 5f, force.x + 5f);
        int speedTurnOutRandom =  Random.Range(speedTurnOut, speedTurnOut + 100);
        rb.AddForce(new Vector2(forceRandomX, -force.y).normalized * speedTurnOutRandom);
        //Debug.Log("turn out");
        float timeTurnOutRandom = Random.Range(timeTurnOut, timeTurnOut + .1f);
        yield return new WaitForSeconds(timeTurnOutRandom);
        rb.velocity = Vector2.zero;
        // float timeDelayRandom = Random.Range(timeDelay, timeDelay + .1f);
        // yield return new WaitForSeconds(timeDelayRandom);
        isAttackAuto = true;
        canTurnOut = true;
   }
   IEnumerator TurnOutTarget(){
        Debug.Log("attack target");
        isAttackAuto = false;
        float angleRandom = Random.Range((angle - 50)/10 , (angle + 50) / 10);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleRandom * 10)); 
        Vector3 force = target.transform.position - transform.position;
        float forceRandomX = Random.Range(force.x - 50f, force.x + 50f);
        float forceRandomY = Random.Range(force.y - 10f, force.y + 10f);
        rb.AddForce(new Vector2(forceRandomX, - forceRandomY).normalized * speedTurnOut * 2);
        yield return new WaitForSeconds(.5f);
        isAttackAuto = true;
        canTurnOut = true;
   }
   IEnumerator TurnOutCor(){
        isAttackAuto = false;
        Vector3 force = target.transform.position - transform.position;
        rb.AddForce(new Vector2(force.x, - force.y).normalized * 1000);
        Debug.Log("turn out");
        yield return new WaitForSeconds(1f);
        isAttackAuto = true;
        canTurnOut = true;
   }
   public void StartGame(){
       collider2D.isTrigger = false;
        StartCoroutine(StartGameCor());
   }
   private bool startAttackBool = false;
   IEnumerator StartGameCor(){

        float timeDelayStartGame = Random.Range(0, .3f);
        yield return  new WaitForSeconds(timeDelayStartGame);
        startAttackBool = true;
        yield return new WaitForSeconds(.2f + timeDelayStartGame);
        mesh.sortingOrder = 2; 
        float timeStartGameRandom = Random.Range(0f, .3f);
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
