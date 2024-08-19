using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallManager : MonoBehaviour
{
    [Tooltip("ボールのTransformをセット")]
    [SerializeField] private Transform ballPos;

    //[Tooltip("ボールのTransformをセット")]
    //[SerializeField] private Transform ballPos;

    private Vector3 startBallPos;

    // Start is called before the first frame update
    void Start()
    {
        Init();  // 初期化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        startBallPos = ballPos.position;  // ボールの初期座標をセット
        
    }
}
