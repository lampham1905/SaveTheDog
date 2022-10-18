using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIHomeManager : MonoBehaviour
{   
    public TextMeshProUGUI cointCounterText;
    public void LoadSelectlevel(){
        SceneManager.LoadScene(Variable.nameSceneSelectLevel);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        int cointCounter = PlayerPrefs.GetInt(Variable.cointCounter, 0);
        cointCounterText.text = cointCounter.ToString();
    }
    

}
