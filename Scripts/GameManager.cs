using System;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using UnityEditor.Build;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    [Header("Start Screen")]
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject PlayButton;

    [Header("Score")]
    [SerializeField] private TMP_Text Score;

    [Header("Get Ready Section")]
    [SerializeField] private GameObject GetReadyPanel;
    [SerializeField] private GameObject TapInstruction;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text GameOverScore;
    [SerializeField] private TMP_Text GameOverBestScore;

    [Header("References")]
    [SerializeField] private Bird_Movement Bird_Movement;

    [Header("Pipe_Spawner")]
    [SerializeField] private GameObject Pipe_Spawner;

    [Header("Camera Shake")]
    [SerializeField] private float ShakeDuration = 0.2f;
    [SerializeField] private float ShakeAmount = 0.5f;

    private const string Best_Score_Key = "BestScore";

    private GameState gameState = GameState.Home;

    public GameState CurrentGameState => gameState;

    private int currentScore;
    private Camera mainCamera;

    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            Score.text = CurrentScore.ToString();
        }
    }

    public int BestScore
    {
        get => PlayerPrefs.GetInt(Best_Score_Key, 0);
        set
        {
            PlayerPrefs.SetInt(Best_Score_Key, value);
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        gameState = GameState.Home;
        logo.SetActive(true);
        PlayButton.SetActive(true);
        TapInstruction.SetActive(false);

        GameOverPanel.SetActive(false);
        GetReadyPanel.SetActive(false);
        Score.gameObject.SetActive(false);

    }
    
    public void playButtn()
    {
        gameState = GameState.GetReady;
        logo.SetActive(false);
        PlayButton.SetActive(false);

        GameOverPanel.SetActive(false);
        GetReadyPanel.SetActive(true);
        TapInstruction.SetActive(true);
        Score.gameObject.SetActive(true);
        Pipe_Spawner.gameObject.SetActive(true);
        CurrentScore = 0;

        // Destroy all pipes when Clicked playButton
        Move_Pipe[] move_Pipes = FindObjectsByType<Move_Pipe>();
        for(int i = 0; i < move_Pipes .Length; i++)
        {
            Destroy(move_Pipes[i].gameObject);
        }
        

        resetGame();
    }

    public void resetGame()
    {
        Bird_Movement.ResetPlayer();
    }

    public void GamePlay()
    {
        gameState = GameState.Playing;
        GameOverPanel.SetActive(false);
        GetReadyPanel.SetActive(false);
        TapInstruction.SetActive(false);
        

    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        StartCoroutine(ShackCamera());
        Score.gameObject.SetActive(false);
        GameOverPanel.SetActive(true);
        PlayButton.SetActive(true);
        GameOverScore.text = CurrentScore.ToString();

        if (CurrentScore  > BestScore)
        {
            BestScore = CurrentScore;
        }
        GameOverBestScore.text = BestScore.ToString();
        
        // stop pipe_Spawner Script
        //*
        
        
          Pipe_Spawner.gameObject.SetActive(false);
      

       
        
        
    }

    IEnumerator ShackCamera()
    {
        Vector3 originalpos = mainCamera.transform.position;
        float timer = 0f;
        while(timer < ShakeDuration)
        {
            timer += Time.deltaTime;
            float x =  UnityEngine.Random.Range(-ShakeAmount, ShakeAmount);
            float y = UnityEngine.Random.Range(-ShakeAmount, ShakeAmount);

            mainCamera.transform.position = originalpos + new Vector3(x, y, 0);

            yield return null;
        }
        mainCamera.transform.position = originalpos;
    }

    public void AddScore()
    {
        if (gameState != GameState.Playing) return;
                CurrentScore++;
        AudioManager.instance.score();
    }



}
