using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Target : MonoBehaviour
{
    [Header("Animation")]
    [SpineAnimation] public string normalAnimationName;
    [SpineAnimation] public string hurtAnimationName;
    [SpineAnimation] public string dieAnimationName;
    [SpineAnimation] public string scareAnimationName;
    [SpineAnimation] public string winAnimationName;
     SkeletonAnimation skeletonAnimation;
     public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    

    public static Target ins;
    public ParticleSystem smokeDie;
     private Rigidbody2D rb;
     private CircleCollider2D coll;
     private MeshRenderer mesh;
     private bool isHurt = false;
    private void Awake() {
        ins = this;
    }
    public int hp;
    public int maxHp;
    // Start is called before the first frame update
    void Start()
    {
        //Spine animation
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        
        GameManager.ins.listTarget.Add(this);
        rb = GetComponent<Rigidbody2D>();  
        coll = GetComponent<CircleCollider2D>();
        mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(){
        rb.gravityScale = 1f;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("dog")){
            hp++;
            CheckDie();
            Hurt();
        }
        if(other.gameObject.CompareTag(Variable.trap)){
            Die();
        }
        if(other.gameObject.CompareTag(Variable.wallUnder)){
            GameManager.ins.ShowRaplay();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
         if(other.gameObject.CompareTag(Variable.dieArea)){
            DieSmoke();
        }
    }
    private void CheckDie(){
        if(hp == maxHp){
            Die();
           
    }      
    }
    public void Scare(){
       if(!GameManager.ins.isDie && !isHurt){
            spineAnimationState.AddAnimation(0, scareAnimationName, false, 0);
    }}
    public void Idle(){
        //anim.SetTrigger("Idle");
    }
    public void Victory(){
        spineAnimationState.SetAnimation(0, winAnimationName, true);
    }
    public void Die(){
         //Debug.Log("die");
        StartCoroutine(DieCor());
    }
    IEnumerator DieCor(){
        GameManager.ins.StopEndGame();
        coll.isTrigger = true;
        GameManager.ins.isDie = true;
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        spineAnimationState.SetAnimation(0, dieAnimationName, true);
        yield return new WaitForSeconds(1f);
        //this.gameObject.SetActive(false);
        GameManager.ins.ShowRaplay();
    }
    // private void OnTriggerExit2D(Collider2D other) {
    //     if(other.gameObject.CompareTag(Variable.wallUnder)){
    //         GameManager.ins.ShowRaplay();
    //     }
    // }
    public void EndGameStop(){
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void Hurt(){
        if(!GameManager.ins.isDie){
            //StartCoroutine(HurtCor());
             isHurt = true;
            spineAnimationState.SetAnimation(0, hurtAnimationName, false);
        }
    }
    
    public void DieSmoke(){
        StartCoroutine(DieSmokeCor());
    }
    IEnumerator DieSmokeCor(){
        skeletonAnimation.enabled = false;
        mesh.enabled = false;
        GameManager.ins.StopEndGame();
        coll.isTrigger = true;
        GameManager.ins.isDie = true;
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        smokeDie.Play();
        yield return new WaitForSeconds(1f);
        //this.gameObject.SetActive(false);
        GameManager.ins.ShowRaplay();
    }
}
