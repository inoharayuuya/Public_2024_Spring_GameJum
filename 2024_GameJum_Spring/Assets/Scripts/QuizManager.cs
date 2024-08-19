using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using Const;

public class QuizManager : MonoBehaviour
{
    public Text QuizText;
    public InputField inputField;
    public Text feedbackText;
    public Text timerText;
    public AudioSource correctSound; // �������̃I�[�f�B�I�\�[�X
    public AudioSource incorrectSound; // �s�������̃I�[�f�B�I�\�[�X

    private List<Question> questions = new List<Question>();
    private List<Question> usedQuestions = new List<Question>(); // �o��ς݂̖����Ǘ����郊�X�g
    private int currentQuestionIndex = 0;
    private int solvedCount = 0;
    private const int maxQuestions = 5;
    private const float timeLimit = 30f;
    private float currentTime;
    private bool gameEnded = false;

    private Coroutine timerCoroutine;

    void Start()
    {
        

        LoadQuestionsFromFile("Math.txt");
        timerCoroutine = StartCoroutine(StartTimer());
        DisplayRandomQuestion();
    }

    void LoadQuestionsFromFile(string filename)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, filename);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    questions.Add(new Question(parts[0], parts[1]));
                }
            }
        }
        else
        {
            Debug.LogError("�w�肳�ꂽ�t�@�C����������܂���: " + filePath);
        }
    }

    void DisplayRandomQuestion()
    {
        if (solvedCount < maxQuestions) // solvedCount �� maxQuestions �����̏ꍇ�̂ݖ���\������
        {
            if (questions.Count > 0)
            {
                // �g�p����Ă��Ȃ����𒊏o����
                List<Question> availableQuestions = new List<Question>();
                foreach (var question in questions)
                {
                    if (!usedQuestions.Contains(question))
                    {
                        availableQuestions.Add(question);
                    }
                }

                // �g�p����Ă��Ȃ���肩�烉���_���ɑI��
                if (availableQuestions.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, availableQuestions.Count);
                    QuizText.text = availableQuestions[randomIndex].Math;
                    feedbackText.text = "";
                    inputField.text = "";
                    currentQuestionIndex = questions.IndexOf(availableQuestions[randomIndex]);

                    // �g�p���ꂽ����ǉ�
                    usedQuestions.Add(availableQuestions[randomIndex]);
                }
                else
                {
                    Debug.LogError("�g�p�\�Ȗ�肪����܂���");
                }
            }
            else
            {
                Debug.LogError("��肪����܂���");
            }
        }
        else
        {
            Debug.Log("���͂��łɉ����ς݂ł��B");
        }
    }

    public void CheckAnswer()
    {
        if (!gameEnded && inputField.text == questions[currentQuestionIndex].reading)
        {
            feedbackText.text = "�����I";
            solvedCount++;
            print("solvedCount:" + solvedCount);
            StartCoroutine(Wait());
            PlayCorrectSound(); // ���������Đ�
        }
        else if (!gameEnded)
        {
            feedbackText.text = "�s����...";
            PlayIncorrectSound(); // �s���������Đ�
        }

        inputField.text = "";

        if (solvedCount == maxQuestions)
        {
            gameEnded = true; // �Q�[�����I���������Ƃ��}�[�N
            EndGame();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        if (solvedCount < maxQuestions)
        {
            ResetTimer(); // �^�C�}�[�����Z�b�g
        }
        QuizText.text = "";
        DisplayRandomQuestion(); // ���̃����_���Ȗ���\��
    }

    IEnumerator StartTimer()
    {
        while (solvedCount < maxQuestions)
        {
            currentTime = timeLimit;
            while (currentTime > 0f)
            {
                if (!gameEnded)
                {
                    currentTime -= Time.deltaTime;
                    UpdateTimerText(currentTime);
                }

                yield return null;
            }
            feedbackText.text = "���Ԑ؂�I";
            yield return new WaitForSeconds(1f);
            DisplayRandomQuestion();
        }
        /*
        feedbackText.text = "�I�����܂����I";
        print("�Q�[���I��");
        gameEnded = true; // �Q�[�����I���������Ƃ��}�[�N
        StopCoroutine(timerCoroutine); // �^�C�}�[���~
        QuizText.text = ""; // �e�L�X�g������
        Common.ChangeScene(Common.GAME_SELECT_SCENE_NAME);
        */
    }

    void EndGame()
    {
        feedbackText.text = "�I�����܂����I";
        gameEnded = true; // �Q�[�����I���������Ƃ��}�[�N
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); // �^�C�}�[���~
        }
        QuizText.text = ""; // �e�L�X�g������
        Common.ChangeScene(Common.GAME_SELECT_SCENE_NAME);
    }

    void UpdateTimerText(float time)
    {
        timerText.text = "Time: " + Mathf.CeilToInt(time).ToString();
    }

    void ResetTimer()
    {
        currentTime = timeLimit;
        UpdateTimerText(currentTime);
        DisplayRandomQuestion(); // ���̃����_���Ȗ���\��
    }

    void PlayCorrectSound()
    {
        if (correctSound != null)
        {
            correctSound.Play();
        }
    }

    void PlayIncorrectSound()
    {
        if (incorrectSound != null)
        {
            incorrectSound.Play();
        }
    }
}


public class Question
{
    public string Math;
    public string reading;

    public Question(string Math, string reading)
    {
        this.Math = Math;
        this.reading = reading;
    }
}
