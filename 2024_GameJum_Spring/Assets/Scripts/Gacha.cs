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
        //���A�x�̒��I
        int itemRarity = ChooseRarity() + 1;

        print("score:" + Common.SCORE_NUMS[itemRarity]);

        manager.Score = Common.SCORE_NUMS[itemRarity];
        manager.Rare = itemRarity;

        print(manager.Score);
        print(manager.Rare);
    }

    int ChooseRarity()
    {
        //�m���̍��v�l���i�[
        float total = 0;

        //�m�������v����
        for (int i = 0; i < RarityInfo.GetLength(0); i++)
            total += RarityInfo[i];

        //Random.value�ł�0����1�܂ł�float�l��Ԃ��̂�
        //�����Ƀh���b�v���̍��v���|����
        float randomPoint = Random.value * total;

        //randomPoint�̈ʒu�ɊY������L�[��Ԃ�
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

    //�{�^���p
    public void lotteryTypeOne()
    {
        rollNum = 1;
    }
    //public void lotteryTypeTen()
    //{
    //    rollNum = 10;
    //}
}
