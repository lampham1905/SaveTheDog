using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject grid;
    public AIMovementRigi[] aIMovementRigi;
    public int maxPoint = 50;
    public int hp = 0;
    public List<AIMovementRigi> listAgentNgu = new List<AIMovementRigi>();
    public List<AIThongMinh> listAgentThongMinh = new List<AIThongMinh>();
    public List<Target> listTarget = new List<Target>();
    public List<Platform> listPlatform = new List<Platform>();
    public bool isAttack = false;
    public static GameManager ins;
    private object yeild;
    public bool isDie = false;

    private void Awake() {
         ins = this;
    }
    void Start()
    {
      Time.timeScale = 1;
       //RandomAIBot();
       hp = 0;
       Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    public void CheckDie(){
      if(hp == 20){
          Target.ins.Die();
          StartCoroutine(ShowReplayGUICor()); 
        }
    }
    void Update()
    {
        
    }
    public void Die(){

    }
    public void StopEndGame(){
      foreach(AIMovementRigi c in listAgentNgu){
        c.StopEndGame();
      }
      foreach(AIThongMinh c in listAgentThongMinh){
        c.StopEndGame();
      }
      foreach(Target c in listTarget){
            c.EndGameStop();
      }
      foreach(Platform c in listPlatform){
            c.EndGameStop();  
      }
    }
    public void ShowRaplay(){
      StartCoroutine(ShowReplayGUICor());
    }
    IEnumerator ShowReplayGUICor(){
        isDie = true;
        yield return new WaitForSeconds(.5f);
        UIManager.ins.ShowReplayGUI();
    }
    public void StartAttack(){
      StartCoroutine(StartAttackCor());
      //isAttack = true;

      
    }
    IEnumerator StartAttackCor(){
       foreach(Target c in listTarget){
          c.StartGame();
        }   
        foreach(Platform c in listPlatform){
          c.DynamicRigiBody();
        }
        yield return new WaitForSeconds(1f);
        // foreach (AIMovement c in listAgent){
        //     c.gameObject.SetActive(true);
        //     c.isStart = true;
        //     c.timeDelay = Random.Range(0.01f,.1f);
        //     c.speedStart.x = Random.Range(-2f, -1f);
        //     c.speedTurnOut.x = Random.Range(-1f, -0.5f);
        //     c.speedAttack = Random.Range(2f, 3f);
        //     //c.speedAttack = 2f;
        // }
        isAttack = true;
        CountTime.ins.startCountTime = true;
        CountTime.ins.CurrentTime();
        //aIMovementRigi.StartGame();
        foreach (AIMovementRigi c in listAgentNgu){
           c.gameObject.SetActive(true);
            c.StartGame();  
           
        }
        foreach(AIThongMinh c in listAgentThongMinh){
            c.gameObject.SetActive(true);
            c.StartGame();
        }     
    }
  public void Replay(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    PlayerPrefs.SetInt(Variable.playResume, 4);
    PlayerPrefs.Save();
  }
  public void Victory(){
    // Check Star current vs Star old
    CheckNewStar(UIManager.ins.star);
    int levelCurrent = PlayerPrefs.GetInt(Variable.levelCurrent);
    int levelPlaying = PlayerPrefs.GetInt(Variable.levelPlaying);
    //Debug.Log(levelCurrent);
    //Debug.Log(levelPlaying);
    if(levelPlaying > levelCurrent){
      PlayerPrefs.SetInt(Variable.levelCurrent,levelCurrent + 1);
      PlayerPrefs.Save();
      UIManager.ins.ShowCointCounterAdd();
      //CointManager.ins.AddCoint(UIManager.ins.star);
    }
    //Target.ins.Victory();
    foreach(Target c in listTarget){
      c.Victory();
    }
    foreach(AIMovementRigi c in listAgentNgu){
      c.StopEndGame();
    }
    foreach(AIThongMinh c in listAgentThongMinh){
      c.StopEndGame();
    }
    foreach(Target c in listTarget){
            c.EndGameStop();
        }
    foreach(Platform c in listPlatform){
            c.EndGameStop();  
        }
    StartCoroutine(VictoryCor());
     //UIManager.ins.ShowVictoryGUI();
  }
  
  IEnumerator VictoryCor(){
    yield return new WaitForSeconds(1f);
     UIManager.ins.ShowVictoryGUI();
  }
  private void CheckNewStar(int star){
    if(star > GetStar(PlayerPrefs.GetInt(Variable.levelPlaying))){
      SetStar(PlayerPrefs.GetInt(Variable.levelPlaying), star);
    }
  } 
  private string GetKey(int level){
    return Variable.levelStar + level;
  }
  private int GetStar(int level){
    return PlayerPrefs.GetInt(GetKey(level), 0);
  }
  private void SetStar(int level, int starAmount){
    PlayerPrefs.SetInt(GetKey(level), starAmount);
     PlayerPrefs.Save();
  }

}
