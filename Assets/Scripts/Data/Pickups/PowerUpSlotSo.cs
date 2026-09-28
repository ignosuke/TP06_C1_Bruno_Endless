using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpSlot", menuName = "Data/Pickups/Power Up Slot")]
public class PowerUpSlotSo : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] private float spawnChance = .1f; // Probabilidad de que el punto quede ocupado
    [SerializeField] private GameObject[] candidates;

    public float GetSpawnChance() => spawnChance;
    public GameObject[] GetCandidates() => candidates;
}