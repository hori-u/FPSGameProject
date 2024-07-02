using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // 最大HP.
    [SerializeField] int maxHp = 10;

    // 現在のHP.
    int hp = 0;
    // 攻撃を受けるフラグ.
    public bool canHit = true;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    // ---------------------------------------------------------------
    /// <summary>
    /// 初期化処理.
    /// </summary>
    // ---------------------------------------------------------------
    public void Init()
    {
        hp = maxHp;
    }

    // ---------------------------------------------------------------
    /// <summary>
    /// コライダーエンター処理.
    /// </summary>
    /// <param name="col"></param>
    // ---------------------------------------------------------------
    //public void OnColliderEnter(Collision col)
    public void OnCollisionEnter(Collision col)
    {

        if( col.gameObject.tag == "Ball" /*&& canHit == true*/ )
        {
            // Arrowを取得して「Arrow」の敵にヒットした時の処理を実行.
            var ball = col.gameObject.GetComponent<Ball>();
            //ball.OnEnemyHit();
            Destroy(col.gameObject);
            // HPを矢の攻撃力分マイナス.
            hp -= ball.Attack;

            string objectName = col.gameObject.name;
            Debug.Log("Collided with: " + objectName);

            if( hp <= 0 )
            {
                // 死亡時処理.
                OnDead();
            }
            else
            {
                Debug.Log( gameObject.name + " に攻撃がヒット。残りHP " + hp );
                // 次回ヒットまでの待機時間.
                StartCoroutine( HitWait() );
            }
        }else{
            Debug.Log("not Ball");

        }
    }

    // ---------------------------------------------------------------
    /// <summary>
    /// 死亡時処理.
    /// </summary>
    // ---------------------------------------------------------------
    void OnDead()
    {
        Debug.Log( gameObject.name + "を倒しました" );
        Destroy( gameObject );
    }

    // ---------------------------------------------------------------
    /// <summary>
    /// 攻撃ヒット後次の攻撃が当たるまでの待機処理.
    /// </summary>
    // ---------------------------------------------------------------
    IEnumerator HitWait()
    {
        // 指定時間待機してフラグを戻す.
        canHit = false;
        yield return new WaitForSeconds( 0.5f );
        canHit = true;
    }
}