using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //[Tooltip("ボールに加える力")]
    //[SerializeField] private Vector3 addForceNum;  // デバック用

    public Vector3 AddForceNum { get; set; }

    private Rigidbody rb;
    [SerializeField]
    public bool isMove;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbodyを取得
        isMove = false;
        //AddForceNum = Vector3.forward + addForceNum;
        //BallAddForce();
    }

    private void Update()
    {
        //if (isMove)
        //{
        //    //rb.AddForce(AddForceNum);  // 設定された力を加える


        //}
        //else
        //{
        //    //rb.velocity = Vector3.zero;
        //}

        //if (!isMove)
        //{
        //    return;
        //}

        if (rb.velocity.magnitude <= Common.MIN_SPEED)
        {
            print("動きを止めました");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            isMove = false;
            //Destroy(gameObject, 5.0f);
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    /// <summary>
    /// ボールに力を加える
    /// </summary>
    public void BallNormalAddForce()
    {
        var tmp = AddForceNum;
        //var tmp = AddForceNum;
        tmp *= Common.ADD_FORCE_NUM;
        //print("tmp:" + tmp);
        rb.AddForce(tmp, ForceMode.Impulse);  // 設定された力を加える
        isMove = true;
    }

    /// <summary>
    /// 右回転
    /// </summary>
    public void BallRightCurveAddForce()
    {
        //var tmp = AddForceNum;
        var tmp = AddForceNum;
        //tmp *= Common.ADD_FORCE_NUM;
        //print("tmp:" + tmp);
        rb.AddForce(tmp, ForceMode.Impulse);  // 設定された力を加える
    }

    /// <summary>
    /// 左回転
    /// </summary>
    public void BallLeftCurveAddForce()
    {
        //var tmp = AddForceNum;
        var tmp = AddForceNum;
        //tmp *= Common.ADD_FORCE_NUM;
        //print("tmp:" + tmp);
        rb.AddForce(tmp, ForceMode.Impulse);  // 設定された力を加える
    }

    public void Reset()
    {
        print("リセット");
        rb.velocity = Vector3.zero;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    
}