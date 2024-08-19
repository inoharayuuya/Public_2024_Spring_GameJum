using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class GameManager : MonoBehaviour
{
    [Tooltip("ピンの初期座標をセット")]
    [SerializeField] private Transform[] pinsPos;

    [Tooltip("ピンの生成管理オブジェクトをセット")]
    [SerializeField] private Transform pinSpawn;

    [Tooltip("ボールの初期座標をセット")]
    [SerializeField] private Vector3 ballPos;

    [Tooltip("ボールオブジェクトをセット")]
    [SerializeField] private GameObject ballObj;

    [Tooltip("パネルをセット")]
    [SerializeField] private GameObject panelObj;

    [Tooltip("スコアテキストをセット")]
    [SerializeField] private Text[] scoreTexts;

    private GameObject pinPrefab;
    private GameObject[] pinsObj;
    private GameObject dragObj;
    private HitCheck hitCheck;
    private Drag drag;
    private int throwCnt;  // 残りの球数
    [SerializeField]
    private int count;
    [SerializeField]
    private int countSum;
    private bool isFirst;
    private Ball ball;
    private float countDownTime;
    private Text text;

    public Phase phase;  // 現在のフェーズをセット
    public AudioSource StrikeSound; // ストライク音のオーディオソース
    public AudioSource PinSound; // ピン倒し音のオーディオソース

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();  // フェーズチェック
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        throwCnt = 0;  // 投げられる回数を設定
        phase = Phase.COUNT_DOWN;  // フェーズ開始
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
    /// フェーズ管理
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
                print("スタート");

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
                print("ゲーム中");
                if (drag.isLocked && isFirst && drag.isClick)
                {
                    print("フェーズを変更します");
                    StartCoroutine(ChangePhase());
                    isFirst = false;
                }

                break;

            case Phase.END:
                print("終了");
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
    /// フェーズを変更
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
    /// 何投目かを表示する
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
    /// 結果を表示する
    /// </summary>
    private IEnumerator DispScore()
    {
        panelObj.SetActive(true);

        if (count >= 10)
        {
            text.text = "ストライク！";
        }
        else
        {
            text.text = count + "ピン";
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
        text.text = "スコア：" + countSum.ToString();
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
