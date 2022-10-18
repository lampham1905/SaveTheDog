using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public int totalLevel = 0;
    public int unlockedLevel = 1;
    private int totalPage = 10;
    private int page = 0;
    private int pageItem = 10;
    private LevelButton[] levelButtons; 
    public GameObject nextButton;
    public GameObject backButton;
    private void Start() {
        unlockedLevel = PlayerPrefs.GetInt(Variable.levelCurrent) + 1;
        Refresh();
    }
    private void OnEnable() {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }
    public void StartLevel(int level){
        // show player alevel info or start level
        if(level == unlockedLevel){
            unlockedLevel += 1;
            Refresh();
        }
    }
    public void ClickNext(){
        page += 1;
        Refresh();
    }
    public void ClickBack(){
        page -= 1;
        Refresh();
    }
    public void Refresh(){
        totalPage = totalLevel / pageItem-1;
        int index = page*pageItem;
        for(int i = 0; i < levelButtons.Length; i++){
            int level = index + i + 1;
            if(level <= totalLevel){
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level, GetStar(level), level <= unlockedLevel);
            }
            else{
                levelButtons[i].gameObject.SetActive(false);
            }
        }
        CheckButton();
    }
    private void CheckButton(){
        backButton.SetActive(page>0);
        nextButton.SetActive(page<totalPage);
    }
    private void SetStar(int level, int starAmount){
        PlayerPrefs.SetInt(GetKey(level), starAmount);
        PlayerPrefs.Save();
    }
    private int GetStar(int level){
        return PlayerPrefs.GetInt(GetKey(level));
    }
    private string GetKey(int level){
        return Variable.levelStar + level;
    }
}
