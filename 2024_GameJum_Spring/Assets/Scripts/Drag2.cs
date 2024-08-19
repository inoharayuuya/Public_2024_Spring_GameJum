using Const;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.SocialPlatforms;

public class Drag2 : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPos;
    Vector3 acc, vel, pos;
    GameObject clickedGameObject;
    bool isDrag;
    Vector3 startPos;
    Vector3 endPos;
    bool isPosUpdate;
    Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        acc = vel = Vector3.zero;
        pos = targetPos;
        isDrag = false;
        isPosUpdate = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //this.pos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            pos = Common.GetMousePosition(Input.mousePosition);
            pos.x = targetPos.x;
            pos.z = targetPos.z;
            //print("pos:" + pos);
            isDrag = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var speed = 75f;
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        }

        if (isDrag)
        {
            var minLimit = targetPos.y - 0.65f;
            var maxLimit = targetPos.y + 0.8f;
            if (pos.y <= minLimit)
            {
                print("tmp:" + minLimit);
                pos.y = minLimit;
                return;
            }
            else if (pos.y >= maxLimit)
            {
                print("tmp:" + maxLimit);
                pos.y = maxLimit;
                return;
            }

            //var tmp = new Vector3(pos.x, pos.y + 10f, pos.z);
            transform.position = pos;
        }

        //// �������u��
        //if (Input.GetMouseButtonDown(0))
        //{
        //    startPos = Common.GetMousePosition(Input.mousePosition);  // �N���b�N�������W���擾
        //    isDrag = true;
        //}

        //// �������u��
        //if (Input.GetMouseButtonUp(0))
        //{
        //    //endPos = Common.GetMousePosition(Input.mousePosition);  // ���������W���擾

        //    //ball.AddForceNum = new Vector3(-endPos.x, 0, -endPos.y);  // �����x�N�g����3�����ɕϊ�
        //    transform.position = new Vector3(-force.x, 0, -force.y);  // �����x�N�g����3�����ɕϊ�
        //}

        //// �����Ă����
        //if (isDrag)
        //{
        //    if (isPosUpdate)
        //    {
        //        StartCoroutine(GetEndPos());
        //    }

        //    var tmp = Vector3.Distance(startPos, endPos);

        //    if (tmp >= Common.MAX_DISTANCE)
        //    {
        //        tmp = Common.MAX_DISTANCE;
        //    }

        //    if (tmp <= Common.MIN_DISTANCE)
        //    {
        //        tmp = 0f;
        //    }

        //    var vector = (endPos - startPos).normalized;  // �x�N�g�����v�Z


        //}
    }

    /// <summary>
    /// �ݒ肵�����Ԃ��Ƃ�endPos���X�V����
    /// </summary>
    private IEnumerator GetEndPos()
    {
        isPosUpdate = false;
        endPos = Common.GetMousePosition(Input.mousePosition);  // endPos���X�V����
        yield return new WaitForSeconds(0.25f);
        isPosUpdate = true;
    }
}
