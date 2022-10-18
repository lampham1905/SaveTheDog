using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    void Start()
    {
        GameManager.ins.listPlatform.Add(this);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DynamicRigiBody(){
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void EndGameStop(){
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
