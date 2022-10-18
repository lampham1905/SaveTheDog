using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Mesh lineBakedMesh;
    public MeshFilter mesh;
    // Start is called before the first frame update
    void Start()
    {
        lineBakedMesh = new Mesh(); //Create a new Mesh (Empty at the moment)
        lineRenderer.BakeMesh(lineBakedMesh, true);
        //lineRenderer
        mesh.mesh = lineBakedMesh;
        Destroy(lineRenderer);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
