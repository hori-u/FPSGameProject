using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] public int Attack = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*public void OnColliderEnter( Collision other )
    {
        if(other.gameObject.tag == "Enemy") 
        {
            //var enemy = other.gameObject.GetComponent<Enemy1>();
            //enemy.canHit = true;
            other.gameObject.canHit = true;
        }
    }*/
    /*public void OnEnemyHit()
    {
        OnDead();
    }*/
}
