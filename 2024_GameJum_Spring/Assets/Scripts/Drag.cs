using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    [Tooltip("�L�����o�X���Z�b�g")]
    [SerializeField] private GameObject canvasObj;

    private Vector3 startPos;
    [SerializeField]
    private Vector3 endPos;
    private bool isPosUpdate;  // endPos���X�V����Ă��邩�ǂ���
    private bool isMove;
    private GameObject clickedGameObject;
    private GameObject ballObj;
    private Ball ball;
    private Vector3 force;
    private GameObject arrow;
    private GameObject arrowObj;

    public bool isLocked;  // ����s�\�ɂȂ��Ă��邩�ǂ���
    public bool isDrag;  // �h���b�O�����ǂ���
    public bool isClick;  // �N���b�N�������ǂ���

    // Start is called before the first frame update
    void Start()
    {
        Init();  // ������
    }

    // Update is called once per frame
    void Update()
    {
        KeyCheck();  // �L�[���̓`�F�b�N
    }

    /// <summary>
    /// ����������
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
    /// �L�[���͂̃`�F�b�N
    /// </summary>
    private void KeyCheck()
    {
        if (!isLocked)
        {
            // �������u��
            if (Input.GetMouseButtonDown(0))
            {
                // �N���b�N�������W�Ƀ{�[�������邩�m�F
                if (StartPosCheck())
                {
                    startPos = Common.GetMousePosition(Input.mousePosition);  // �N���b�N�������W���擾
                    arrowObj = Instantiate(arrow, canvasObj.transform);
                }
                else
                {
                    startPos = Vector3.zero;
                }
                //print(arrowObj.transform.rotation.x * Mathf.Rad2Deg);
            }

            // �������u��
            if (Input.GetMouseButtonUp(0))
            {
                isDrag = false;
                if (arrowObj != null)
                {
                    Destroy(arrowObj);

                    if (!isMove)
                    {
                        print("�L�����Z��");
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

                //endPos = Common.GetMousePosition(Input.mousePosition);  // ���������W���擾

                //ball.AddForceNum = new Vector3(-endPos.x, 0, -endPos.y);  // �����x�N�g����3�����ɕϊ�
                ball.AddForceNum = new Vector3(-force.x, 0, -force.y);  // �����x�N�g����3�����ɕϊ�
                ball.BallNormalAddForce();

                if (!isLocked && isMove)
                {
                    isLocked = true;
                }
            }

            // �����Ă����
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

                var vector = (endPos - startPos).normalized;  // �x�N�g�����v�Z
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
    /// �N���b�N���ꂽ���W�Ƀ{�[�������邩�ǂ���
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
                print("�����\");
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
    /// �ݒ肵�����Ԃ��Ƃ�endPos���X�V����
    /// </summary>
    private IEnumerator GetEndPos()
    {
        isPosUpdate = false;
        endPos = Common.GetMousePosition(Input.mousePosition);  // endPos���X�V����
        yield return new WaitForSeconds(0.25f);
        isPosUpdate = true;
    }
}
