using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : EnemyBase
{
    void Start()
    {
        Init();
    }
 
    void Update()
    {

    }

    public override void OnDead()
    {
        GameObject enemy = GameObject.Find ("EnemyBat");
        Debug.Log( enemy.name + "を倒しました" );
        Destroy( enemy );

        GameObject UI = GameObject.Find ("PlayerUICanvas");
        Manager codeUI = UI.GetComponent<Manager>();
        codeUI.RemainEnemies += -1;
    }
}