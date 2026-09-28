using System;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private PlayerPickup playerPickup;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private ScrollSettingsSo scrollSettings;
    [SerializeField] private float pointsPerUnit = 1f;

    public event Action<int> OnScoreChanged;

    private float elapsedTime = 0f;
    private float rawScore; // Se acumula en float para no perder las fracciones de distancia
    private bool isRunning = true;
    private bool isScoring = true;

    public float GetElapsedTime() => elapsedTime;

    private void OnEnable()
    {
        playerPickup.OnPickedUp += AddPoints;
        playerRespawn.OnRespawnStarted += StopScoring;
        playerRespawn.OnRespawnFinished += ResumeScoring;
    }

    private void OnDisable()
    {
        playerPickup.OnPickedUp -= AddPoints;
        playerRespawn.OnRespawnStarted -= StopScoring;
        playerRespawn.OnRespawnFinished -= ResumeScoring;
    }

    private void Update()
    {
        if (!isRunning) return;

        // El tiempo sigue corriendo durante el respawn, solo el puntaje se congela
        elapsedTime += Time.deltaTime;

        if (!isScoring) return;

        // El puntaje por distancia usa la velocidad de scroll
        AddRaw(scrollSettings.GetSpeed() * pointsPerUnit * Time.deltaTime);
    }

    // Frena todo al morir definitivamente
    public void Stop()
    {
        isRunning = false;
        isScoring = false;
    }

    // Frena solo el puntaje, durante el respawn el tiempo sigue
    public void StopScoring()
    {
        isScoring = false;
    }

    public void ResumeScoring()
    {
        isScoring = true;
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(rawScore);
    }

    private void AddPoints(int points)
    {
        AddRaw(points);
    }

    private void AddRaw(float amount)
    {
        int previousScore = GetScore();
        rawScore += amount;

        // Solo avisa cuando el entero visible cambia, no todos los frames
        if (GetScore() != previousScore)
            OnScoreChanged?.Invoke(GetScore());
    }
}