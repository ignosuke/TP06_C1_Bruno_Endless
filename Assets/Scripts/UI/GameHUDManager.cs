using TMPro;
using UnityEngine;

public class GameHUDManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private ScoreTracker scoreTracker;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Power Ups")]
    [SerializeField] private PlayerPowerUps playerPowerUps;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text powerDonutText;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private GameObject dropHintText;

    private void OnEnable()
    {
        scoreTracker.OnScoreChanged += RefreshScore;
        playerPowerUps.OnPowerDonutChanged += RefreshPowerDonut;
        playerPowerUps.OnLivesChanged += RefreshLives;
        playerRespawn.OnRespawnStarted += ShowDropHint;
        playerRespawn.OnRespawnFinished += HideDropHint;
    }

    private void OnDisable()
    {
        scoreTracker.OnScoreChanged -= RefreshScore;
        playerPowerUps.OnPowerDonutChanged -= RefreshPowerDonut;
        playerPowerUps.OnLivesChanged -= RefreshLives;
        playerRespawn.OnRespawnStarted -= ShowDropHint;
        playerRespawn.OnRespawnFinished -= HideDropHint;
    }

    private void Start()
    {
        RefreshScore(scoreTracker.GetScore());
        RefreshLives(0);
        RefreshPowerDonut(0f);
        dropHintText.SetActive(false);
    }

    private void Update()
    {
        timeText.text = FormatTime(scoreTracker.GetElapsedTime());
    }

    private string FormatTime(float seconds)
    {
        int total = Mathf.FloorToInt(seconds);

        return $"Time: {total / 60:00}:{total % 60:00}";
    }

    private void RefreshScore(int score)
    {
        scoreText.text = $"Score: {score.ToString()}";
    }

    private void RefreshLives(int lives)
    {
        livesText.text = $"Extra Lives: {lives}";
    }

    private void RefreshPowerDonut(float remaining)
    {
        powerDonutText.text = $"Donut Time: {remaining:F1}s";
    }

    private void ShowDropHint()
    {
        dropHintText.SetActive(true);
    }

    private void HideDropHint()
    {
        dropHintText.SetActive(false);
    }
}