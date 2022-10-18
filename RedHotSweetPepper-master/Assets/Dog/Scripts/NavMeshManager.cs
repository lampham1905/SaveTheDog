using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
     public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("UpdateNavmesh", 0f, .5f);
    }
    void UpdateNavmesh(){
         for (int i = 0; i < surfaces.Length; i++) 
        {
          
            surfaces [i].BuildNavMesh ();    
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
    }
}
