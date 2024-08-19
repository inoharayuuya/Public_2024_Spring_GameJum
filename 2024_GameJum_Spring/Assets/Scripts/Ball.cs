using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //[Tooltip("�{�[���ɉ������")]
    //[SerializeField] private Vector3 addForceNum;  // �f�o�b�N�p

    public Vector3 AddForceNum { get; set; }

    private Rigidbody rb;
    [SerializeField]
    public bool isMove;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbody���擾
        isMove = false;
        //AddForceNum = Vector3.forward + addForceNum;
        //BallAddForce();
    }

    private void Update()
    {
        //if (isMove)
        //{
        //    //rb.AddForce(AddForceNum);  // �ݒ肳�ꂽ�͂�������


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
            print("�������~�߂܂���");
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
    /// �{�[���ɗ͂�������
    /// </summary>
    public void BallNormalAddForce()
    {
        var tmp = AddForceNum;
        //var tmp = AddForceNum;
        tmp *= Common.ADD_FORCE_NUM;
        //print("tmp:" + tmp);
        rb.AddForce(tmp, ForceMode.Impulse);  // �ݒ肳�ꂽ�͂�������
        isMove = true;
    }

    /// <summary>
    /// �E��]
    /// </summary>
    public void BallRightCurveAddForce()
    {
        //var tmp = AddForceNum;
        var tmp = AddForceNum;
        //tmp *= Common.ADD_FORCE_NUM;
        //print("tmp:" + tmp);
        rb.AddForce(tmp, ForceMode.Impulse);  // �ݒ肳�ꂽ�͂�������
    }

    /// <summary>
    /// ����]
    /// </summary>
    public void BallLeftCurveAddForce()
    {
        //var tmp = AddForceNum;
        var tmp = AddForceNum;
        //tmp *= Common.ADD_FORCE_NUM;
        //print("tmp:" + tmp);
        rb.AddForce(tmp, ForceMode.Impulse);  // �ݒ肳�ꂽ�͂�������
    }

    public void Reset()
    {
        print("���Z�b�g");
        rb.velocity = Vector3.zero;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    
}