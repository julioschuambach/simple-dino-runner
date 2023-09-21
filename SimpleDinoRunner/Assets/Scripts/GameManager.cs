using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject gameOverPanel;
    private Button retryButton;

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
        if (gameOverPanel == null)
            FindGameOverPanel();

        gameOverPanel.SetActive(false);
    }

    private void FindGameOverPanel()
    {
        gameOverPanel = GameObject.Find("GameOverPanel");
    }

    private void OnEnable()
        => SceneManager.sceneLoaded += OnLevelFinishedLoading;

    private void OnDisable()
        => SceneManager.sceneLoaded -= OnLevelFinishedLoading;

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        FindGameOverPanel();
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        retryButton.onClick.AddListener(Retry);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        SetTimeScale(0.0f);
        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
        SetTimeScale(1.0f);
    }

    private void SetTimeScale(float timeScale)
        => Time.timeScale = timeScale;
}
