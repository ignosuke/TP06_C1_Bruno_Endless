using System;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private LayerMask pickupLayers;
    private PlayerPowerUps powerUps;

    public event Action<int> OnPickedUp;

    private void Awake()
    {
        powerUps = GetComponent<PlayerPowerUps>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar por layer descarta computando menos, el componente trae el dato
        if (!IsInLayerMask(other.gameObject.layer, pickupLayers)) return;
        if (!other.TryGetComponent<Pickup>(out Pickup pickup)) return;

        if (pickup.GetData().IsPowerUp())
            powerUps.Apply(pickup.GetData());
        else
            OnPickedUp?.Invoke(pickup.GetScoreValue());

        pickup.Collect();
    }

    private bool IsInLayerMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}