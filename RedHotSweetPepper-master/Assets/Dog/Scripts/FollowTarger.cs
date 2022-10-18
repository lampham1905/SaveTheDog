using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarger : MonoBehaviour
{
    public Transform targer;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
       targer = transform.parent.parent.parent.GetComponent<ChuongCho>().target;                                                           
    }

    // Update is called once per frame
    void Update()
    {
    }
      public void LookAtTarget(){
        Vector2 tXFollow = targer.position - transform.position;
        Vector2 dire= (new Vector2(tXFollow.x, tXFollow.y)).normalized;
        angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90)); 
    }
}
