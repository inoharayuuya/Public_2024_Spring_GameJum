using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocker : MonoBehaviour
{
    [Tooltip("始点座標と終点座標をセット")]
    [SerializeField] private Vector3[] movePositions;

    [Tooltip("スピードをセット")]
    [SerializeField] private float speed;

    private float tmpSpeed;
    private Rigidbody rb;
    private bool isMoveVector;  // 始めに動く方向を基準にして方向ベクトルを変更する
    private Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        Init();  // 初期化
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// 初期化処理
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
            //vector = (movePositions[0] - movePositions[1]).normalized;  // 方向ベクトルの計算
            transform.position = Vector3.MoveTowards(transform.position, movePositions[1], speed * Time.deltaTime);

            if (transform.position == movePositions[1])
            {
                isMoveVector = false;
            }
        }
        else
        {
            //vector = (movePositions[1] - movePositions[0]).normalized;  // 方向ベクトルの計算
            transform.position = Vector3.MoveTowards(transform.position, movePositions[0], speed * Time.deltaTime);

            if (transform.position == movePositions[0])
            {
                isMoveVector = true;
            }
        }

        //print(movePositions[1]);
    }
}
