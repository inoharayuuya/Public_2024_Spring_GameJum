using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scorecount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Score_text;        // text�p�̃Q�[���I�u�W�F�N�g
    public int score = 0;                               // �X�R�A��ۑ����邽�߂̊֐�
    // ������
    void Start()
    {

    }

    // �X�V
    void Update()
    {
        // �e�L�X�g�̕\�������ւ���
        Score_text.text = "Score : 0" + score;
        //�f�o�b�O�p�Ƃ��ăX�y�[�X�L�[�ŕς��悤�ɂ��Ă���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 1; // �Ƃ肠����1���Z�������Ă݂�
        }
    }
}