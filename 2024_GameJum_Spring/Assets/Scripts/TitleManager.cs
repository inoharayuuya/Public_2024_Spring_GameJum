using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ¶ƒNƒŠƒbƒN‚ª‰Ÿ‚³‚ê‚½ê‡
        if (Input.GetMouseButtonDown(0))
        {
            Common.ChangeScene("GameSelectScene");
        }
    }
}
