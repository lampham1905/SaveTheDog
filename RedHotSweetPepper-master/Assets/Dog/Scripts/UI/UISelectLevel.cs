using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UISelectLevel : MonoBehaviour
{
   public void ReturnHome(){
        SceneManager.LoadScene(Variable.nameSceneHome);
   }
}
