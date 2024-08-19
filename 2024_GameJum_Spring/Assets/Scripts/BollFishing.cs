using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollFishing : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // �����ʒu��ۑ�
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

            // �Ƃɏ�����̑��x��^����
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

            // �I�u�W�F�N�g�̈ʒu���X�V����
            transform.position = newPosition;
        }
    }
}
