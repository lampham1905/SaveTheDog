using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager ins;
    public GameObject grid;
    public int levelCurrent;
    private void Awake() {
        ins = this;
    }
    public List<GameObject> listTileMap = new List<GameObject>();
    public List<GameObject> listPlatdform = new List<GameObject>();

   private void Start() {
     levelCurrent = PlayerPrefs.GetInt(Variable.levelCurrent);
        if(PlayerPrefs.GetInt(Variable.playResume) == 0){
             int levelCurrent = PlayerPrefs.GetInt(Variable.levelCurrent);
             //Debug.LogWarning(levelCurrent);
            SpawnLevel(levelCurrent);
            PlayerPrefs.SetInt(Variable.levelPlaying, levelCurrent + 1);
            PlayerPrefs.SetInt(Variable.LevelNext, levelCurrent + 2);
            PlayerPrefs.Save();
        }
        else if(PlayerPrefs.GetInt(Variable.playResume) == 1){
            int levelSelect = PlayerPrefs.GetInt(Variable.levelSelect);
            SpawnLevel(levelSelect -1);
            PlayerPrefs.SetInt(Variable.levelPlaying, levelSelect);
            PlayerPrefs.SetInt(Variable.LevelNext, levelSelect + 1);
            PlayerPrefs.Save();
        }
        else if(PlayerPrefs.GetInt(Variable.playResume) == 2){
            int levelNext = PlayerPrefs.GetInt(Variable.LevelNext);
            SpawnLevel(levelNext -1);
            PlayerPrefs.SetInt(Variable.levelPlaying, levelNext);
            PlayerPrefs.SetInt(Variable.LevelNext, levelNext + 1);
            PlayerPrefs.Save();
        }
        else{
          int levelPlaying  = PlayerPrefs.GetInt(Variable.levelPlaying);
          SpawnLevel(levelPlaying -1);
        }
        //Debug.Log(PlayerPrefs.GetInt(Variable.levelPlaying));
   } 
   public void SpawnLevel(int levelCurrent){
        //Spawn Tile
        Instantiate(listTileMap[levelCurrent], new Vector2(0, 0), listTileMap[levelCurrent].transform.rotation, grid.transform);
        //Spawn platform
        Instantiate(listPlatdform [levelCurrent], new Vector2(0, 0), Quaternion.identity);
   }
}




