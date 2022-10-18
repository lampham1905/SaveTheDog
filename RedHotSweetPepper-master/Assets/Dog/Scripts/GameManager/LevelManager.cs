using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public int ID{ get; set;}
    public string LevelName{ get; set;}
    public bool Completed{ get; set;}
    public int Starts{ get; set;}
    public bool Locked{ get; set;}

    public LevelManager(int id, string levelName, bool completed, int starts, bool locked){
        this.ID = id;
        this.LevelName = levelName;
        this.Completed = completed;
        this.Starts = starts;
        this.Locked = locked; 
    }
    public void Complete(){
        this.Completed = true;
    }
    public void Complete(int starts){
        this.Completed = true;
        this.Starts = starts;
    }
    public void Lock(){
        this.Locked = true;
    }
    public void UnLock(){
        this.Locked = false;   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextLevel(){
        CointManager.ins.AddCoint(UIManager.ins.star);
        StartCoroutine(nextLevelCor());
    }
    IEnumerator nextLevelCor(){
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(Variable.nameSceneGamePlay);
        PlayerPrefs.SetInt(Variable.playResume, 2);
        PlayerPrefs.Save();
    }
}
