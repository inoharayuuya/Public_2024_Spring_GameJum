using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scorecount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Score_text;        // text用のゲームオブジェクト
    public int score = 0;                               // スコアを保存するための関数
    // 初期化
    void Start()
    {

    }

    // 更新
    void Update()
    {
        // テキストの表示を入れ替える
        Score_text.text = "Score : 0" + score;
        //デバッグ用としてスペースキーで変わるようにしている
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 1; // とりあえず1加算し続けてみる
        }
    }
}