using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameHomeManager : MonoBehaviour
{
    public void StartGame(){
        PlayerPrefs.SetInt(Variable.playResume, 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }                                                                                   
}                                       
