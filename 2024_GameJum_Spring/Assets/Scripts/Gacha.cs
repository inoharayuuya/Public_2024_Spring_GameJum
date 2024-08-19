using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    float[] RarityInfo = new float[] {30, 25, 20, 10, 5, 4, 3, 2, 1};
    int rollNum = 0;

    [SerializeField]
    SenbonhikiManager manager;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < rollNum; i++)
        {
            GetDropItem();
        }
        rollNum = 0;
    }

    void GetDropItem()
    {
        //レア度の抽選
        int itemRarity = ChooseRarity() + 1;

        print("score:" + Common.SCORE_NUMS[itemRarity]);

        manager.Score = Common.SCORE_NUMS[itemRarity];
        manager.Rare = itemRarity;

        print(manager.Score);
        print(manager.Rare);
    }

    int ChooseRarity()
    {
        //確率の合計値を格納
        float total = 0;

        //確率を合計する
        for (int i = 0; i < RarityInfo.GetLength(0); i++)
            total += RarityInfo[i];

        //Random.valueでは0から1までのfloat値を返すので
        //そこにドロップ率の合計を掛ける
        float randomPoint = Random.value * total;

        //randomPointの位置に該当するキーを返す
        for (int i = 0; i < RarityInfo.GetLength(0); i++)
        {
            if (randomPoint < RarityInfo[i])
            {
                return i;
            }
            else
            {
                randomPoint -= RarityInfo[i];
            }
        }
        return 0;
    }

    //ボタン用
    public void lotteryTypeOne()
    {
        rollNum = 1;
    }
    //public void lotteryTypeTen()
    //{
    //    rollNum = 10;
    //}
}
