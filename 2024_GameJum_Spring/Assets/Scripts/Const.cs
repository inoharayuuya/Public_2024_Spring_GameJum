using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Const
{
    public enum Phase
    {
        COUNT_DOWN,
        START,
        GAME_PLAY,
        END,
        NONE,
    }

    public static class Common
    {
        public const string BALL_OBJ_NAME = "Ball";
        public const string HASSYA = "pinball_HASSYA";
        public const string DRAG_OBJ_NAME = "GameManager";
        public const string HIT_CHECK_OBJ_NAME = "SetPinPosition";
        public const string ARROW_PATH_NAME = "Prefabs/Arrow";
        public const string PIN_PATH_NAME = "Prefabs/Pin";
        public const string DISP_TEXT = "���ځI";
        public const string TITLE_SCENE_NAME = "TitleScene";
        public const string GAME_SELECT_SCENE_NAME = "GameSelectScene";
        public const string POS_SCENE_NAME = "POS";
        public const string SENBONHIKI_SCENE_NAME = "SenbonhikiScene";

        public const float MAX_DISTANCE = 3.0f;  // �ő勗��
        public const float MIN_DISTANCE = 1.0f;  // �ŏ�����
        public const float ADD_FORCE_NUM = 30.0f;  // �擾�����x�N�g���ɂ�����l
        public const float MIN_SPEED = 0.25f;  // �Œᑬ�x
        public const int INIT_THROW_NUM = 5;  // �{�[���𓊂������
        public const float THROW_TIMER = 7.5f;  // �{�[���𓊂�����̐�������
        public const float MAX_BALL_DISTANCE = 4f;
        public const float COUNT_DOWN_TIME = 3f;  // �J�E���g�_�E���̕b��
        public const float MAX_PULL_DISTANCE = 1.0f;

        public static readonly int[] SCORE_NUMS = {
            10,
            25,
            50,
            75,
            100,
            200,
            300,
            400,
            500,
        };

        /// <summary>
        /// �X�N���[�����W�����[���h���W�ɕϊ�
        /// </summary>
        public static Vector3 GetMousePosition(Vector3 mousePos)
        {
            //�}�E�X���W�̎擾
            mousePos = Input.mousePosition;
            //�X�N���[�����W�����[���h���W�ɕϊ�
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            //���[���h���W�����g�̍��W�ɐݒ�
            return worldPos;
        }

        /// <summary>
        /// �V�[���`�F���W
        /// </summary>
        public static void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}