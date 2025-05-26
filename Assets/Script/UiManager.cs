using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using System;

public class UiManager : MonoBehaviour
{
    public TMP_Text TextScore = null;
    public TMP_Text TextCount = null;
    public TMP_Text TextTimer = null;

    public int Score = 0;                // 현재 점수
    public int Count = 10;      // 남은 밤송이 수 (게임 횟수)
    public float GameTime = 30.0f;  // 총 게임 시간
    private float CurrentTime;      // 현재 남은 게임 시간

    private static UiManager _instance = null;
    public static UiManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("UiManager is null.");
            return _instance;
        }
    }
    
    void Start()
    {
        CurrentTime = GameTime;
    }

    // Update is called once per frame
    void Update()
    {
        TextScore.text = "Score : " + Score.ToString();
        TextCount.text = "Count : " + Count.ToString();

        UpdateTime();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.Log("UiManager has another instance.");
            Destroy(gameObject);
        }
    }

    public void UpdateScore()
    {
        Score += 10;
    }

    public void UpdateGameCount()
    {
        Count--;

        if(Count <= 0)
        {
            // 엔드씬 불러오기
            SceneManager.LoadScene("EndScene");// 엔드씬 불러오기
        }
    }
    public void GameOver()
    {
        // 간단하게 씬 다시 시작 (재도전)
        // 또는 UI 띄워서 "게임 종료" 알리기 등 가능
        Debug.Log("게임 종료!");
        SceneManager.LoadScene("EndScene");
    }
    public void UpdateTime()
    {
        CurrentTime -= Time.deltaTime;
        TextTimer.text = "Time: " + Mathf.CeilToInt(CurrentTime).ToString();

        if (CurrentTime <= 0)
        {
            GameOver();
        }
    }
}
