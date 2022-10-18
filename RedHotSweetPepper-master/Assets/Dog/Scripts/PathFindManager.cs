using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PathFindManager : MonoBehaviour
{
   private void Start() {
        InvokeRepeating("ScanUpdate", 0f, .5f);
   }
   void ScanUpdate(){
    AstarPath.FindAstarPath();

    AstarPath.active.Scan();        
   }
}
