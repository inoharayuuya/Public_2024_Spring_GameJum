using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollFishing : MonoBehaviour
{
    public float upwardVelocity = 5f; // 上向きの速度

    float objPosZ;
    Vector3 initialPosition;
    Vector3 newPosition;
    Vector3 objPos;
    Vector3 mousePos;

    private Rigidbody rb;
    private Vector3 dragStartPosition;
    private bool isDragging = false;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // 初期位置を保存
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // 竿に上向きの速度を与える
            Vector3 upwardForce = new Vector3(0, upwardVelocity, 0);
            rb.velocity += upwardForce;
        }

        if (isDragging)
        {
            // ワールド座標をスクリーン座標に変換する
            objPos = Camera.main.WorldToScreenPoint(transform.position);
            // マウスの位置を取得する
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objPos.z);
            // スクリーン座標からオブジェクト座標へと戻す
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePos);

            // オブジェクトの位置を更新する
            transform.position = newPosition;
        }
    }
}
