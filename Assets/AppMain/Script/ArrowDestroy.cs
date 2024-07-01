using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestroy : MonoBehaviour {

    [SerializeField] GameObject rigid;

    void Start (){
        rigid = GameObject.Find("Player");
    }

    void Update () {
        float dist = Vector3.Distance (rigid.transform.position, transform.position);
        if (dist > 100.0f) {
            Destroy(gameObject);
        }
    }
}