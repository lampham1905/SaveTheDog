using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountTime : MonoBehaviour
{
    public static CountTime ins;
    private void Awake() {
        ins = this;
    }
    public float TimeLeft;
    public bool TimerOn = false;
    public Text timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float currentTime;
    public void CurrentTime(){
        currentTime =Time.time;
    }
    // Update is called once per frame
    void Update()
    {
       if(startCountTime){
            //updateTImer(LinesDrawer.ins.currTime);
            updateTImer(currentTime);
       }
    }
    public bool startCountTime = false;
    public bool boolTime = false;
    IEnumerator UpdateTimeCor(float startTime){
        while(boolTime){
            updateTImer(startTime);
        }
        yield return new WaitForSeconds(10f);
        boolTime = false;
    }
    private bool boolVar = true;
    void updateTImer(float startTime){
    
        float t = 7 - (Time.time - startTime);
         if(t < 0){
            t = 0;}
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        string miliseconds = ((int)((t - (int)t) * 100)).ToString("00");
        timer.text = seconds;
        //Debug.Log(t);
        if(t == 0){
            
            if(boolVar && !GameManager.ins.isDie){
                //Debug.Log("het gio");
                GameManager.ins.Victory();
                boolVar = false;
            }
        }
        UIManager.ins.CircleCountDown(t%60);
    }
    public void StartUpdateTime(float startTime){
        boolTime = true;
        StartCoroutine(UpdateTimeCor(startTime));
    }
}
