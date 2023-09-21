using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    private int score;
    [SerializeField] private Text scoreText;
    private int highscore;
    [SerializeField] private Text highscoreText;

    [Header("GameOverPanel")]
    [SerializeField] private GameObject gameOverPanel;
    private Button retryButton;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameplayClip;
    [SerializeField] private AudioClip gameOverClip;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeAudioClip(gameplayClip);
        EnsureCanvasElements();
        gameOverPanel.SetActive(false);
    }

    private void ChangeAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void EnsureCanvasElements()
    {
        if (gameOverPanel == null) gameOverPanel = GameObject.Find("GameOverPanel");
        if (scoreText == null) scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        if (highscoreText == null) highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
    }

    private void OnEnable()
        => SceneManager.sceneLoaded += OnLevelFinishedLoading;

    private void OnDisable()
        => SceneManager.sceneLoaded -= OnLevelFinishedLoading;

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        EnsureCanvasElements();
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        retryButton.onClick.AddListener(Retry);
        gameOverPanel.SetActive(false);
        ClearScore();
        UpdateHighscore();
    }

    public void Score(int value)
    {
        score += value;
        UpdateScore(scoreText, score);
    }

    private void UpdateScore(Text textObject, int score)
        => textObject.text = score.ToString("00000");

    private void UpdateHighscore()
    {
        if (highscore < score) highscore = score;

        UpdateScore(highscoreText, highscore);
    }

    private void ClearScore()
        => score = 0;

    public void GameOver()
    {
        UpdateHighscore();
        SetTimeScale(0.0f);
        gameOverPanel.SetActive(true);
        ChangeAudioClip(gameOverClip);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
        SetTimeScale(1.0f);
        ChangeAudioClip(gameplayClip);
    }

    private void SetTimeScale(float timeScale)
        => Time.timeScale = timeScale;
}
