using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenbonhikiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject animatorObj;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text[] panelTexts;

    [SerializeField]
    Gacha gacha;
    private Animator animator;
    private bool isFirst;

    public int Rare { get; set; }
    public float Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        KeyCheck();
    }

    private void Init()
    {
        animator = animatorObj.GetComponent<Animator>();
        isFirst = true;
        panel.SetActive(false);
    }

    private void KeyCheck()
    {
        if (Input.GetMouseButtonDown(0) && isFirst)
        {
            animator.SetTrigger("Move");
            gacha.lotteryTypeOne();
            isFirst = false;
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        panelTexts[0].text = Rare.ToString();
        panelTexts[1].text = Score.ToString();
        panel.SetActive(true);
        yield return new WaitForSeconds(5f);
        Common.ChangeScene(Common.GAME_SELECT_SCENE_NAME);
        panel.SetActive(false);
    }
}
