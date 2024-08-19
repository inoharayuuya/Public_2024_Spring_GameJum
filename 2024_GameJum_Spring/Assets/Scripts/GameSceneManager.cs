using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Const;

public class GameSceneManager : MonoBehaviour
{

    // 切り替えたいシーンの名前を指定します
    public string targetSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 左クリックが押された場合
        if (Input.GetMouseButtonDown(0))
        {
            Common.ChangeScene(targetSceneName);
        }
    }
}

