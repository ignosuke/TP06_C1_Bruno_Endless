using UnityEngine;

[CreateAssetMenu(fileName = "PickupSo", menuName = "Data/Pickups/Pickup Data")]
public class PickupDataSo : ScriptableObject
{
    [Header("Score Up")]
    [SerializeField] private int scoreValue = 5;
    [SerializeField] private ParticleSystem collectEffect;

    [Header("Power Up")]
    [SerializeField] private bool isPowerUp;
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration = 5f;

    public int GetScoreValue() => scoreValue;
    public ParticleSystem GetCollectEffect() => collectEffect;

    public bool IsPowerUp() => isPowerUp;
    public PowerUpType GetPowerUpType() => powerUpType;
    public float GetDuration() => duration;
}