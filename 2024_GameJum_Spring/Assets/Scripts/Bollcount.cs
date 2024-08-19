using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Bollcount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Boll_text;        // text用のゲームオブジェクト
    public int Restboll = 5;                           // 残りの球の数を保存するための関数
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Boll_text.text = "RestBoll : 0" + Restboll;
        // デバッグ用としてスペースキーで変わるようにしている
        // Restbollが0の時は動かなくする
        if (Input.GetKeyDown(KeyCode.Space) && !(Restboll == 0))
        {
            Restboll -= 1; // とりあえず1減算してみる
        }
    }
}
