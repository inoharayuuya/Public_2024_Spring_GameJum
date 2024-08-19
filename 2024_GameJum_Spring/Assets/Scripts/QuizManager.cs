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
    public AudioSource correctSound; // 正解音のオーディオソース
    public AudioSource incorrectSound; // 不正解音のオーディオソース

    private List<Question> questions = new List<Question>();
    private List<Question> usedQuestions = new List<Question>(); // 出題済みの問題を管理するリスト
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
            Debug.LogError("指定されたファイルが見つかりません: " + filePath);
        }
    }

    void DisplayRandomQuestion()
    {
        if (solvedCount < maxQuestions) // solvedCount が maxQuestions 未満の場合のみ問題を表示する
        {
            if (questions.Count > 0)
            {
                // 使用されていない問題を抽出する
                List<Question> availableQuestions = new List<Question>();
                foreach (var question in questions)
                {
                    if (!usedQuestions.Contains(question))
                    {
                        availableQuestions.Add(question);
                    }
                }

                // 使用されていない問題からランダムに選択
                if (availableQuestions.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, availableQuestions.Count);
                    QuizText.text = availableQuestions[randomIndex].Math;
                    feedbackText.text = "";
                    inputField.text = "";
                    currentQuestionIndex = questions.IndexOf(availableQuestions[randomIndex]);

                    // 使用された問題を追加
                    usedQuestions.Add(availableQuestions[randomIndex]);
                }
                else
                {
                    Debug.LogError("使用可能な問題がありません");
                }
            }
            else
            {
                Debug.LogError("問題がありません");
            }
        }
        else
        {
            Debug.Log("問題はすでに解決済みです。");
        }
    }

    public void CheckAnswer()
    {
        if (!gameEnded && inputField.text == questions[currentQuestionIndex].reading)
        {
            feedbackText.text = "正解！";
            solvedCount++;
            print("solvedCount:" + solvedCount);
            StartCoroutine(Wait());
            PlayCorrectSound(); // 正解音を再生
        }
        else if (!gameEnded)
        {
            feedbackText.text = "不正解...";
            PlayIncorrectSound(); // 不正解音を再生
        }

        inputField.text = "";

        if (solvedCount == maxQuestions)
        {
            gameEnded = true; // ゲームが終了したことをマーク
            EndGame();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        if (solvedCount < maxQuestions)
        {
            ResetTimer(); // タイマーをリセット
        }
        QuizText.text = "";
        DisplayRandomQuestion(); // 次のランダムな問題を表示
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
            feedbackText.text = "時間切れ！";
            yield return new WaitForSeconds(1f);
            DisplayRandomQuestion();
        }
        /*
        feedbackText.text = "終了しました！";
        print("ゲーム終了");
        gameEnded = true; // ゲームが終了したことをマーク
        StopCoroutine(timerCoroutine); // タイマーを停止
        QuizText.text = ""; // テキストを消す
        Common.ChangeScene(Common.GAME_SELECT_SCENE_NAME);
        */
    }

    void EndGame()
    {
        feedbackText.text = "終了しました！";
        gameEnded = true; // ゲームが終了したことをマーク
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); // タイマーを停止
        }
        QuizText.text = ""; // テキストを消す
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
        DisplayRandomQuestion(); // 次のランダムな問題を表示
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
