using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Const;

public class GameSceneManager : MonoBehaviour
{

    // �؂�ւ������V�[���̖��O���w�肵�܂�
    public string targetSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���N���b�N�������ꂽ�ꍇ
        if (Input.GetMouseButtonDown(0))
        {
            Common.ChangeScene(targetSceneName);
        }
    }
}

