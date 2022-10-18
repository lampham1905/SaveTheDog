
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLine : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 selfPos;
  

    // Start is called before the first frame update
    void Start()
    {
         selfPos = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        // if(other.gameObject.CompareTag("dog")){
        //     Debug.Log("cham");
        //     Vector2 dogPos = new Vector2(other.transform.position.x, other.transform.position.y);
        //      //Vector2 force = new Vector2((other.contacts[0].point - dogPos).x, (other.contacts[0].point - dogPos).y);
        //      Vector2 force = new Vector2(( dogPos - other.gameObject.GetComponent<AIMovement>().startPos).x, ( dogPos - other.gameObject.GetComponent<AIMovement>().startPos).y);
            
        //     //Debug.DrawLine(other.contacts[0].point, force.normalized*500, Color.blue, 5 );
        //     //Debug.Log(force.normalized);
        //     rb.AddForceAtPosition(force.normalized * 200, other.contacts[other.contacts.Count()/2].point); 
        //     Debug.Log(other.contacts.Count());
            
        // }
    }
}
