using UnityEngine;

public class Pinball : MonoBehaviour
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

    void Start()
    {
        // 初期位置を設定
        initialPosition = new Vector3(-3f, -2.5f, -0.5f);
        transform.position = initialPosition; // 初期位置を設定する

        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // 初期位置を保存
    }

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

            // ボールに上向きの速度を与える
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

            // X座標を初期位置のX座標に固定する
             newPosition.x = initialPosition.x;
            // Z座標を初期位置のZ座標に固定する
            newPosition.z = initialPosition.z;
            // Y座標を初期位置のY座標より小さい値に制限する（下方向にしか移動できない）
            newPosition.y = Mathf.Min(newPosition.y, initialPosition.y);

            // オブジェクトの位置を更新する
            transform.position = newPosition;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 衝突物の法線方向に反射する
        Vector3 reflection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.velocity = reflection;
    }
}
