using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class SceneMamager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeBowlingScene()
    {
        Common.ChangeScene("GameScene");
    }
    public void ChangeRegisterScene()
    {
        Common.ChangeScene("POS");
    }
    public void ChangePinballScene()
    {
        Common.ChangeScene("PinballScene");
    }
    public void ChangeFishingScene()
    {
        Common.ChangeScene("GameSelectScene");
    }
    public void ChangeSenbonturiScene()
    {
        Common.ChangeScene("SenbonhikiScene");
    }
}
