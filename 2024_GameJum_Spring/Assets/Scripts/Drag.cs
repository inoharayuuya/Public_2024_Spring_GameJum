using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    [Tooltip("キャンバスをセット")]
    [SerializeField] private GameObject canvasObj;

    private Vector3 startPos;
    [SerializeField]
    private Vector3 endPos;
    private bool isPosUpdate;  // endPosが更新されているかどうか
    private bool isMove;
    private GameObject clickedGameObject;
    private GameObject ballObj;
    private Ball ball;
    private Vector3 force;
    private GameObject arrow;
    private GameObject arrowObj;

    public bool isLocked;  // 操作不能になっているかどうか
    public bool isDrag;  // ドラッグ中かどうか
    public bool isClick;  // クリックしたかどうか

    // Start is called before the first frame update
    void Start()
    {
        Init();  // 初期化
    }

    // Update is called once per frame
    void Update()
    {
        KeyCheck();  // キー入力チェック
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        isDrag = false;
        isPosUpdate = true;
        isLocked = true;
        isMove = false;
        isClick = false;
        ballObj = GameObject.Find(Common.BALL_OBJ_NAME);
        ball = ballObj.GetComponent<Ball>();
        arrow = (GameObject)Resources.Load(Common.ARROW_PATH_NAME);
    }

    /// <summary>
    /// キー入力のチェック
    /// </summary>
    private void KeyCheck()
    {
        if (!isLocked)
        {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0))
            {
                // クリックした座標にボールがあるか確認
                if (StartPosCheck())
                {
                    startPos = Common.GetMousePosition(Input.mousePosition);  // クリックした座標を取得
                    arrowObj = Instantiate(arrow, canvasObj.transform);
                }
                else
                {
                    startPos = Vector3.zero;
                }
                //print(arrowObj.transform.rotation.x * Mathf.Rad2Deg);
            }

            // 離した瞬間
            if (Input.GetMouseButtonUp(0))
            {
                isDrag = false;
                if (arrowObj != null)
                {
                    Destroy(arrowObj);

                    if (!isMove)
                    {
                        print("キャンセル");
                        isClick = false;
                        //isDrag = false;
                        return;
                    }
                    else
                    {
                        isClick = true;
                    }
                }
                else
                {
                    return;
                }

                //endPos = Common.GetMousePosition(Input.mousePosition);  // 離した座標を取得

                //ball.AddForceNum = new Vector3(-endPos.x, 0, -endPos.y);  // 方向ベクトルを3次元に変換
                ball.AddForceNum = new Vector3(-force.x, 0, -force.y);  // 方向ベクトルを3次元に変換
                ball.BallNormalAddForce();

                if (!isLocked && isMove)
                {
                    isLocked = true;
                }
            }

            // 押している間
            if (isDrag)
            {
                isMove = true;
                if (isPosUpdate)
                {
                    StartCoroutine(GetEndPos());
                }

                var tmp = Vector3.Distance(startPos, endPos);

                if (tmp >= Common.MAX_DISTANCE)
                {
                    tmp = Common.MAX_DISTANCE;
                }

                if (tmp <= Common.MIN_DISTANCE)
                {
                    tmp = 0f;
                    isMove = false;
                    //return;
                }

                var vector = (endPos - startPos).normalized;  // ベクトルを計算
                //print("vector:" + tmp);

                force = vector * tmp;

                //var rot = arrowObj.transform.rotation;
                //print(rot);
                //arrowObj.transform.rotation = new Quaternion(0,0,Mathf.Atan2(vector.y, vector.x) * Mathf.Deg2Rad, 0);
                var num = new Vector3(0, 0, 180 - (Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg));
                arrowObj.transform.rotation = Quaternion.Euler(num);
                //print("Atan2:" + -(Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg));
            }
        }
    }

    /// <summary>
    /// クリックされた座標にボールがあるかどうか
    /// </summary>
    private bool StartPosCheck()
    {
        clickedGameObject = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;

            if (clickedGameObject.name == Common.BALL_OBJ_NAME)
            {
                print("投球可能");
                isDrag = true;
            }
            else
            {
                isDrag = false;
            }
        }

        //print(clickedGameObject);

        return isDrag;
    }

    /// <summary>
    /// 設定した時間ごとにendPosを更新する
    /// </summary>
    private IEnumerator GetEndPos()
    {
        isPosUpdate = false;
        endPos = Common.GetMousePosition(Input.mousePosition);  // endPosを更新する
        yield return new WaitForSeconds(0.25f);
        isPosUpdate = true;
    }
}
