using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallManager : MonoBehaviour
{
    [Tooltip("�{�[����Transform���Z�b�g")]
    [SerializeField] private Transform ballPos;

    //[Tooltip("�{�[����Transform���Z�b�g")]
    //[SerializeField] private Transform ballPos;

    private Vector3 startBallPos;

    // Start is called before the first frame update
    void Start()
    {
        Init();  // ������
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        startBallPos = ballPos.position;  // �{�[���̏������W���Z�b�g
        
    }
}
