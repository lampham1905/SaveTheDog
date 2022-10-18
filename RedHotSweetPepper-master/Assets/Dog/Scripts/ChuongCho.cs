using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuongCho : MonoBehaviour
{
    public static ChuongCho ins;
    private void Awake() {
        ins = this;
    }
    public bool isLeft;
    public GameObject changeLayerLeft;
    public GameObject changeLayerRight;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        changeLayerLeft.SetActive(false);
        if(isLeft){
            Vector3 localScaleEnemy = transform.GetChild(0).transform.localScale;
            transform.GetChild(0).transform.localScale = new Vector3(-localScaleEnemy.x, localScaleEnemy.y, localScaleEnemy.z);
            changeLayerLeft.SetActive(true);
            changeLayerRight.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
