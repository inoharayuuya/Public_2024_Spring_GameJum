using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bollcount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Boll_text;        // text�p�̃Q�[���I�u�W�F�N�g
    public int Restboll = 5;                           // �c��̋��̐���ۑ����邽�߂̊֐�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Boll_text.text = "RestBoll : 0" + Restboll;
        // �f�o�b�O�p�Ƃ��ăX�y�[�X�L�[�ŕς��悤�ɂ��Ă���
        // Restboll��0�̎��͓����Ȃ�����
        if (Input.GetKeyDown(KeyCode.Space) && !(Restboll == 0))
        {
            Restboll -= 1; // �Ƃ肠����1���Z���Ă݂�
        }
    }
}
