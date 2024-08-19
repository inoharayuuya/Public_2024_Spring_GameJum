using UnityEngine;

public class Pinball : MonoBehaviour
{
    public float upwardVelocity = 5f; // ������̑��x

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
        // �����ʒu��ݒ�
        initialPosition = new Vector3(-3f, -2.5f, -0.5f);
        transform.position = initialPosition; // �����ʒu��ݒ肷��

        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // �����ʒu��ۑ�
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

            // �{�[���ɏ�����̑��x��^����
            Vector3 upwardForce = new Vector3(0, upwardVelocity, 0);
            rb.velocity += upwardForce;
        }

        if (isDragging)
        {
            // ���[���h���W���X�N���[�����W�ɕϊ�����
            objPos = Camera.main.WorldToScreenPoint(transform.position);
            // �}�E�X�̈ʒu���擾����
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objPos.z);
            // �X�N���[�����W����I�u�W�F�N�g���W�ւƖ߂�
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePos);

            // X���W�������ʒu��X���W�ɌŒ肷��
             newPosition.x = initialPosition.x;
            // Z���W�������ʒu��Z���W�ɌŒ肷��
            newPosition.z = initialPosition.z;
            // Y���W�������ʒu��Y���W��菬�����l�ɐ�������i�������ɂ����ړ��ł��Ȃ��j
            newPosition.y = Mathf.Min(newPosition.y, initialPosition.y);

            // �I�u�W�F�N�g�̈ʒu���X�V����
            transform.position = newPosition;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �Փ˕��̖@�������ɔ��˂���
        Vector3 reflection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.velocity = reflection;
    }
}
