using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppPlayerController : MonoBehaviour
{
    //移動///////////////////////////////////////////////////////////////////
    // 移動速度.
    [SerializeField] float moveSpeed = 300f;
    // 速度制限値.
    [SerializeField] float speedLimit = 100f;

    // リジッドボディ.
    Rigidbody rigid = null;

    Rigidbody arrowrigid = null;

    Vector3 moveDirection = Vector3.zero;
 
    Vector3 startPos;

    //x軸方向の入力を保存
    private float _input_x;
    //z軸方向の入力を保存
    private float _input_z;

/*
    //カメラ///////////////////////////////////////////////////////////////////

    // X軸周りのカメラ回転速度.
    [SerializeField] float xRotationSpeed = 5f;
    // Y軸周りのカメラ回転速度.
    [SerializeField] float yRotationSpeed = 5f;

    // マウスクリックを開始した位置.
    Vector3 startMousePosition = Vector3.zero;
    // クリック開始時点でのカメラの角度.
    Vector3 startCameraRotation = Vector3.zero;
    */
    //ジャンプ//////////////////////////////////////////////////////////////////

    //通所のジャンプ
    [SerializeField] private float jumpPower = 5f;
    //走っているときのジャンプ
    //[SerializeField] private float dashJumpPower = 5.6f;

    //弓矢//////////////////////////////////////////////////////////////////////

    [SerializeField] public GameObject arrow;

    [SerializeField] public GameObject arrowPos;

    [SerializeField] private float arrowSpeed = 250;

    [SerializeField] public GameObject ArrowPoint;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletPos;

    bool flag = false;

    void Start()
    {
        // リジッドボディの取得.
        rigid = GetComponent<Rigidbody>();

        //arrowPos = transform.GetChild(0).gameObject;

        ArrowPoint = GameObject.Find("ArrowPoint");

        //GameObject ArrowObject = (GameObject)Instantiate(arrow, arrowPos.transform.position, Quaternion.identity);
    
        //ArrowObject.transform.parent = ArrowPoint.transform;

        bulletPos = transform.GetChild(0).gameObject;

        /*flag = true;
        GameObject ArrowObject = (GameObject)Instantiate(arrow, ArrowPoint.transform.position, Quaternion.identity);
        ArrowObject.transform.parent = ArrowPoint.transform;
        Rigidbody arrowrigid = ArrowObject.GetComponent<Rigidbody>();
        arrowrigid.isKinematic = true;*/
    }

    void Update() //1フレーム毎，時間が一定でない
    {
        // クリックの開始.
        /*if( Input.GetMouseButtonDown( 0 ) == true ) 
        {
            // マウスの位置とカメラの角度を保管.
            startMousePosition = Input.mousePosition;
            startCameraRotation = Camera.main.gameObject.transform.localRotation.eulerAngles;
        }
 
        // クリック中（ドラッグ）.
        if( Input.GetMouseButton( 0 ) == true )
        {
            // 現時点のマウス位置を取得.
            var currentMousePosition = Input.mousePosition;
            // クリック開始位置からの差分を算出.
            var def = ( currentMousePosition - startMousePosition );
            // 現在のカメラ角度.
            var currentCameraRotation = Camera.main.transform.localRotation.eulerAngles;
            // 回転角度を算出.
            currentCameraRotation.x = startCameraRotation.x + ( def.y * xRotationSpeed * 0.01f );
            currentCameraRotation.y = startCameraRotation.y + ( -def.x * yRotationSpeed * 0.01f );
            // カメラに適用.
            Camera.main.transform.localRotation = Quaternion.Euler( currentCameraRotation );
        
            //rigid.transform.localRotation = Quaternion.Euler( currentCameraRotation );
            //rigid.MoveRotation = Quaternion.AngleAxis(Vector3.Angle(currentCameraRotation,startCameraRotation), rigid.position);
        }
 
        // クリック終了.
        if( Input.GetMouseButtonUp( 0 ) == true )
        {
            // 保管した値をリセット.
            startMousePosition = Vector3.zero; 
            startCameraRotation = Vector3.zero;
        }*/

        //移動//////////////////////////////////////////////////////////////////////////////////////////////////

        //x軸方向、z軸方向の入力を取得
        //Horizontal、水平、横方向のイメージ
        _input_x = Input.GetAxis("Horizontal");
        //Vertical、垂直、縦方向のイメージ
        _input_z = Input.GetAxis("Vertical");

        //カメラの角度を算出
        Quaternion cameraForward = Camera.main.transform.localRotation;

        //移動の向きなど座標関連はVector3で扱う
        Vector3 velocity = new Vector3(_input_x, 0, _input_z);
        //ベクトルの向きを取得
        Vector3 direction = cameraForward * velocity.normalized;

        //移動距離を計算
        float speed;
        if(moveSpeed > speedLimit) speed = speedLimit;
        else speed = moveSpeed;
        float distance = speed * Time.deltaTime;
        
        //var radian = rigid.rotation * (Mathf.PI / 180);
        //Vector3 RDrotation = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)).normalized;
        //Vector3 RDrotation = rigid.rotation.eulerAngles;

        //float distance = RDrotation * Sdistance;

        //移動先を計算
        Vector3 destination = rigid.position + direction * distance;

        //移動先に向けて回転
        //rigid.transform.localRotation = Quaternion.Euler( destination );
        //移動先の座標を設定
        rigid.position = destination;

        //ジャンプ///////////////////////////////////////////////////////////////////////////
        if(Input.GetButtonDown("Jump")) {
	    //　走って移動している時はジャンプ力を上げる
	        //if(runFlag && velocity.magnitude > 0f) {
		    //    velocity.y += dashJumpPower;
	        //} else {
		    rigid.velocity = new Vector3(0, jumpPower, 0);
	        //}
        }

        //Shot();

        /*if(!flag) {
            flag = true;
            GameObject ArrowObject = (GameObject)Instantiate(arrow, ArrowPoint.transform.position, Quaternion.identity);
            ArrowObject.transform.parent = ArrowPoint.transform;
            Rigidbody arrowrigid = ArrowObject.GetComponent<Rigidbody>();
            arrowrigid.isKinematic = true;
        }*/

        if (Input.GetKeyDown(KeyCode.Return))
        {

            //ArrowPoint = GameObject.Find("ArrowPoint");
            arrowPos = transform.GetChild(0).gameObject;
            GameObject ArrowObject = (GameObject)Instantiate(arrow, arrowPos.transform.position, Quaternion.identity);
            ArrowObject.transform.parent = ArrowPoint.transform;
            Rigidbody arrowrigid = ArrowObject.GetComponent<Rigidbody>();
            
            arrowrigid.isKinematic = false;
            
            arrowrigid.AddForce(cameraForward.eulerAngles/*transform.forward*/* arrowSpeed);
            //GameObject ArrowObject = (GameObject)Instantiate(arrow, arrowPos.transform.position, Quaternion.identity);
            //arrowPos = transform.GetChild(0).gameObject;

            /*Vector3 position = new Vector3(bulletPos.transform.position.x, bulletPos.transform.position.y, bulletPos.transform.position.z);
            GameObject ball = (GameObject)Instantiate(bullet, position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(transform.forward * arrowSpeed);*/
            flag = false;
        }

    }


    void FixedUpdate() //一定時間毎
    {
        /*// カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
 
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * _input_z + Camera.main.transform.right * _input_x;
    
        float speed;
        if(moveSpeed > speedLimit) speed = speedLimit;
        else speed = moveSpeed;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rigid.velocity = moveForward * speed + new Vector3(0, rigid.velocity.y, 0);
 
        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }*/
    }

    /*void Shot() 
    {
        if (Input.GetKeyDown("Fire1"))
        {
            GameObject Arrow = (GameObject)Instantiate(arrow, arrowPos.transform.position, Quaternion.identity);
            Rigidbody arrowrigid = Arrow.GetComponent<Rigidbody>();
            arrowrigid.AddForce(transform.forward * arrowSpeed);
        }

    }*/

    void MoveResistance()
    {
    }

    void StopForce()
    {
    }

}