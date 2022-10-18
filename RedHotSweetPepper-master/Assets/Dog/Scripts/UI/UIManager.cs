using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public Image pointCount;
    public Image circleDownCountImg;
    public Image[] startArray;
    public Image[] startEndArray;  
    public static UIManager ins;
    public int star = 3;
    public GameObject cointCounterAddGUI;
    public TextMeshProUGUI cointAddText;
    public TextMeshProUGUI cointCounterText;
    private void Awake() {
        ins = this;
    }
    public GameObject RePlayGUI;
    public GameObject VictoryGUI;
    // Start is called before the first frame update
    void Start()
    {
        pointCount.fillAmount = 1;
        UpdateCoinsWhenChanged(PlayerPrefs.GetInt(Variable.cointCounter));
    }

    public void PointUpdate(float pointCurr,  float maxPoint){
        pointCount.fillAmount = pointCurr / maxPoint;
        CheckStar(pointCurr, maxPoint);
    }
    public void CheckStar(float pointCurr, float maxPoint){
        float raito = (maxPoint - pointCurr) / maxPoint;
        if(raito >= .33f && raito < 66f){
            startArray[2].gameObject.SetActive(false);
            star = 2;
        }
         if(raito >= .66f){
            startArray[1].gameObject.SetActive(false);
            star = 1;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowReplayGUI(){
        RePlayGUI.SetActive(true);
        //Time.timeScale = 0;
    }
    public void ShowVictoryGUI(){
        VictoryGUI.SetActive(true);
        CheckStarEnd(star);
        //Time.timeScale = 0;
    }
    bool boolObj = false;
    public void CircleCountDown(float t){
        circleDownCountImg.fillAmount = t/7;
    }
    IEnumerator CircleCountDownCor(float t){
        while(boolObj){
            
        }
        yield return new WaitForSeconds(10f);
        boolObj = false;
    }
    public void BackHome(){
        SceneManager.LoadScene(Variable.nameSceneHome);
    }
    private void CheckStarEnd(int star){
        if(star == 2){
            startEndArray[2].gameObject.SetActive(false);
            Debug.Log("2 sao");
        }
        else if(star == 1){
            startEndArray[2].gameObject.SetActive(false);
            startEndArray[1].gameObject.SetActive(false);
        }
    }
    public void ShowCointCounterAdd(){
        cointCounterAddGUI.SetActive(true);
        if(star == 3){
            cointAddText.text = "+5";
        }
        else if(star == 2){
            cointAddText.text = "+3";
        }
        else{
            cointAddText.text = "+1";
        }
    }
    public void UpdateCoinsWhenChanged(int Coints){
        cointCounterText.text = Coints.ToString();
        PlayerPrefs.SetInt(Variable.cointCounter, Coints);
        PlayerPrefs.Save();
    }
}
