using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform target;
    private NavMeshAgent agent;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;

        //agent.isStopped = false;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Line")){
            rb.AddForce(new Vector2(0, -1000).normalized );
            agent.isStopped = true;
            Debug.Log("turnout");
        }
    }
}
