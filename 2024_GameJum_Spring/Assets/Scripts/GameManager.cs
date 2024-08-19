using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class GameManager : MonoBehaviour
{
    [Tooltip("�s���̏������W���Z�b�g")]
    [SerializeField] private Transform[] pinsPos;

    [Tooltip("�s���̐����Ǘ��I�u�W�F�N�g���Z�b�g")]
    [SerializeField] private Transform pinSpawn;

    [Tooltip("�{�[���̏������W���Z�b�g")]
    [SerializeField] private Vector3 ballPos;

    [Tooltip("�{�[���I�u�W�F�N�g���Z�b�g")]
    [SerializeField] private GameObject ballObj;

    [Tooltip("�p�l�����Z�b�g")]
    [SerializeField] private GameObject panelObj;

    [Tooltip("�X�R�A�e�L�X�g���Z�b�g")]
    [SerializeField] private Text[] scoreTexts;

    private GameObject pinPrefab;
    private GameObject[] pinsObj;
    private GameObject dragObj;
    private HitCheck hitCheck;
    private Drag drag;
    private int throwCnt;  // �c��̋���
    [SerializeField]
    private int count;
    [SerializeField]
    private int countSum;
    private bool isFirst;
    private Ball ball;
    private float countDownTime;
    private Text text;

    public Phase phase;  // ���݂̃t�F�[�Y���Z�b�g
    public AudioSource StrikeSound; // �X�g���C�N���̃I�[�f�B�I�\�[�X
    public AudioSource PinSound; // �s���|�����̃I�[�f�B�I�\�[�X

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();  // �t�F�[�Y�`�F�b�N
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void Init()
    {
        throwCnt = 0;  // ��������񐔂�ݒ�
        phase = Phase.COUNT_DOWN;  // �t�F�[�Y�J�n
        dragObj = GameObject.Find(Common.DRAG_OBJ_NAME);
        drag = dragObj.GetComponent<Drag>();
        pinPrefab = (GameObject)Resources.Load(Common.PIN_PATH_NAME);
        pinsObj = new GameObject[pinsPos.Length];
        hitCheck = GameObject.Find(Common.HIT_CHECK_OBJ_NAME).GetComponent<HitCheck>();
        count = 0;
        countSum = 0;
        isFirst = true;
        ball = ballObj.GetComponent<Ball>();
        countDownTime = Common.COUNT_DOWN_TIME;
        text = panelObj.GetComponentInChildren<Text>();
        scoreTexts[0].text = Common.INIT_THROW_NUM.ToString();
        scoreTexts[1].text = "" + 0;
    }

    /// <summary>
    /// �t�F�[�Y�Ǘ�
    /// </summary>
    private void CheckPhase()
    {
        switch (phase)
        {
            case Phase.COUNT_DOWN:
                panelObj.SetActive(true);
                countDownTime -= Time.deltaTime;

                text.text = ((int)countDownTime + 1).ToString();
                if (countDownTime <= 0)
                {
                    countDownTime = Common.COUNT_DOWN_TIME;
                    panelObj.SetActive(false);
                    phase = Phase.START;
                }
                break;

            case Phase.START:
                print("�X�^�[�g");

                StartCoroutine(DispThrowCount());

                for (int i = 0; i < pinsPos.Length; i++)
                {
                    pinsObj[i] = Instantiate(pinPrefab, pinsPos[i].position, Quaternion.identity, pinSpawn);
                }
                
                ball.Reset();
                ballObj.transform.position = ballPos;
                phase = Phase.GAME_PLAY;
                break;

            case Phase.GAME_PLAY:
                print("�Q�[����");
                if (drag.isLocked && isFirst && drag.isClick)
                {
                    print("�t�F�[�Y��ύX���܂�");
                    StartCoroutine(ChangePhase());
                    isFirst = false;
                }

                break;

            case Phase.END:
                print("�I��");
                StartCoroutine(DispResult());
                phase = Phase.NONE;
                break;
        }
    }

    public void Count()
    {
        count++;
        if (count == 10)
        {
            StrikeSound.Play();

        }
        else if (count == 1)
        {
            PinSound.Play();
        }
    }

    /// <summary>
    /// �t�F�[�Y��ύX
    /// </summary>
    private IEnumerator ChangePhase()
    {
        //isFirst = true;
        yield return new WaitForSeconds(Common.THROW_TIMER);
        foreach (var pinObj in pinsObj)
        {
            Destroy(pinObj);
        }
        
        StartCoroutine(DispScore());
    }

    /// <summary>
    /// �����ڂ���\������
    /// </summary>
    private IEnumerator DispThrowCount()
    {
        panelObj.SetActive(true);
        text.text = (throwCnt + 1) + Common.DISP_TEXT;
        yield return new WaitForSeconds(2);
        panelObj.SetActive(false);
        drag.isLocked = false;
    }

    /// <summary>
    /// ���ʂ�\������
    /// </summary>
    private IEnumerator DispScore()
    {
        panelObj.SetActive(true);

        if (count >= 10)
        {
            text.text = "�X�g���C�N�I";
        }
        else
        {
            text.text = count + "�s��";
        }

        yield return new WaitForSeconds(2);
        panelObj.SetActive(false);

        countSum += count;
        count = 0;
        drag.isClick = false;
        isFirst = true;
        throwCnt++;

        scoreTexts[0].text = (Common.INIT_THROW_NUM - throwCnt).ToString();
        scoreTexts[1].text = countSum.ToString();

        if (throwCnt >= Common.INIT_THROW_NUM)
        {
            phase = Phase.END;
        }
        else
        {
            phase = Phase.START;
        }
    }

    private IEnumerator DispResult()
    {
        panelObj.SetActive(true);
        text.text = "�X�R�A�F" + countSum.ToString();
        yield return new WaitForSeconds(2);
        panelObj.SetActive(false);
        Common.ChangeScene(Common.GAME_SELECT_SCENE_NAME);
    }
    
    void PlayCorrectSound()
    {
        if (StrikeSound != null)
        {
            StrikeSound.Play();
        }
    }

    void PlayIncorrectSound()
    {
        if (PinSound != null)
        {
            PinSound.Play();
        }
    }
}
