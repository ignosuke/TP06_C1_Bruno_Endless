using System;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color powerDonutColor = new Color(1f, .95f, .4f);

    public event Action<float> OnPowerDonutChanged; // Segundos restantes, 0 = inactivo
    public event Action<int> OnLivesChanged;

    private float powerDonutTimer;
    private int extraLives;

    public bool IsPowerDonutActive() => powerDonutTimer > 0f;
    public int GetExtraLives() => extraLives;

    private void Update()
    {
        if (powerDonutTimer <= 0f) return;

        powerDonutTimer -= Time.deltaTime;

        if (powerDonutTimer <= 0f)
        {
            powerDonutTimer = 0f;
            spriteRenderer.color = Color.white;
        }

        OnPowerDonutChanged?.Invoke(powerDonutTimer);
    }

    public void Apply(PickupDataSo data)
    {
        if (data.GetPowerUpType() == PowerUpType.PowerDonut)
            ActivatePowerDonut(data.GetDuration());
        else
            AddLife();
    }

    public bool TryConsumeLife()
    {
        if (extraLives <= 0) return false;

        extraLives--;
        OnLivesChanged?.Invoke(extraLives);

        return true;
    }

    private void ActivatePowerDonut(float duration)
    {
        powerDonutTimer = duration; // Se reinicia, no se acumula
        spriteRenderer.color = powerDonutColor;

        OnPowerDonutChanged?.Invoke(powerDonutTimer);
    }

    private void AddLife()
    {
        extraLives++;
        OnLivesChanged?.Invoke(extraLives);
    }
}