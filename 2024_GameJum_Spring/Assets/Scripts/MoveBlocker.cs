using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocker : MonoBehaviour
{
    [Tooltip("�n�_���W�ƏI�_���W���Z�b�g")]
    [SerializeField] private Vector3[] movePositions;

    [Tooltip("�X�s�[�h���Z�b�g")]
    [SerializeField] private float speed;

    private float tmpSpeed;
    private Rigidbody rb;
    private bool isMoveVector;  // �n�߂ɓ�����������ɂ��ĕ����x�N�g����ύX����
    private Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        Init();  // ������
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        tmpSpeed = speed;
        //rb = GetComponent<Rigidbody>();
        isMoveVector = true;
    }

    private void Move()
    {

        if (isMoveVector)
        {
            //vector = (movePositions[0] - movePositions[1]).normalized;  // �����x�N�g���̌v�Z
            transform.position = Vector3.MoveTowards(transform.position, movePositions[1], speed * Time.deltaTime);

            if (transform.position == movePositions[1])
            {
                isMoveVector = false;
            }
        }
        else
        {
            //vector = (movePositions[1] - movePositions[0]).normalized;  // �����x�N�g���̌v�Z
            transform.position = Vector3.MoveTowards(transform.position, movePositions[0], speed * Time.deltaTime);

            if (transform.position == movePositions[0])
            {
                isMoveVector = true;
            }
        }

        //print(movePositions[1]);
    }
}
