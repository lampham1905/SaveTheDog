using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour
{
   public Image[] StartArray;

   public void SetStar(int StartAmount)
   {
    switch(StartAmount){
        case 0:
            StartArray[2].gameObject.SetActive(false);
            StartArray[1].gameObject.SetActive(false);
            StartArray[0].gameObject.SetActive(false);
            break;
        case 1:
            StartArray[2].gameObject.SetActive(false);
            StartArray[1].gameObject.SetActive(false);
            StartArray[0].gameObject.SetActive(true);
            break;
        case 2:
            StartArray[2].gameObject.SetActive(false);
            StartArray[1].gameObject.SetActive(true);
            StartArray[0].gameObject.SetActive(true);
            break;
        case 3:
            StartArray[2].gameObject.SetActive(true);
            StartArray[1].gameObject.SetActive(true);
            StartArray[0].gameObject.SetActive(true);
            break;
    }
   }
}
