using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float groundDrag;

    public float playerheight;
    public LayerMask Ground;
    bool grounded;

    public Transform orientation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public float upForce = 6;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

   
    void Update()
    {
        //地面と接しているかを判断
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, Ground);

        //接している場合は、設定した減速値を代入しプレイヤーを滑りにくくする
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        ProcessInput();
        SpeedControl();

        if(Input.GetKeyDown("space") && grounded) {
            Debug.Log("space");
            //rb.AddForce(new Vector3(0, upForce, 0));
            rb.velocity = Vector3.up * upForce;
        }
    }


    private void FixedUpdate()
    {
        movePlayer();
    }


    private void ProcessInput()
    {
        //入力を取得
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }


    private void movePlayer()
    {
        //向いている方向に進む
        moveDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }


    private void SpeedControl()
    {
        //プレイヤーのスピードを制限
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}