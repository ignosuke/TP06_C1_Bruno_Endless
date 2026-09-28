using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayers; // Los que la dona puede rebotar
    [SerializeField] private LayerMask fallLayers;  // El vacio: la dona no sirve

    [Header("Launch On Hit")]
    [SerializeField] private Vector2 launchForce = new Vector2(8f, 10f);
    [SerializeField] private float launchTorque = 15f;
    [SerializeField] private float launchLifetime = 3f;

    public event Action OnDied;

    private PlayerPowerUps powerUps;
    private PlayerRespawn respawn;
    private bool isDead;

    private void Awake()
    {
        powerUps = GetComponent<PlayerPowerUps>();
        respawn = GetComponent<PlayerRespawn>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || respawn.IsRespawning()) return;

        int layer = other.gameObject.layer;

        bool isEnemy = IsInLayerMask(layer, enemyLayers);
        bool isFall = IsInLayerMask(layer, fallLayers);

        if (!isEnemy && !isFall) return;

        // La dona solo cubre enemigos, caerse al vacio mata igual
        if (isEnemy && powerUps.IsPowerDonutActive())
        {
            LaunchEnemy(other.gameObject);
            return;
        }

        if (powerUps.TryConsumeLife())
        {
            // El enemigo se destruye, el vacio no: es parte del nivel
            if (isEnemy)
                Destroy(other.gameObject);

            respawn.Begin();
            return;
        }

        isDead = true;
        OnDied?.Invoke();
    }

    // Se desprende del chunk para volar libre y deja de ser letal por si vuelve a tocar al jugador
    private void LaunchEnemy(GameObject enemy)
    {
        enemy.transform.SetParent(null);
        enemy.layer = LayerMask.NameToLayer("Default");

        if (!enemy.TryGetComponent(out Rigidbody2D enemyRb))
            enemyRb = enemy.AddComponent<Rigidbody2D>();

        enemyRb.bodyType = RigidbodyType2D.Dynamic;
        enemyRb.gravityScale = 1f;

        // La direccion depende del lado por el que lo toco, asi no siempre sale para el mismo lado
        float directionX = Mathf.Sign(enemy.transform.position.x - transform.position.x);

        enemyRb.AddForce(new Vector2(launchForce.x * directionX, launchForce.y), ForceMode2D.Impulse);
        enemyRb.AddTorque(launchTorque * -directionX, ForceMode2D.Impulse);

        Destroy(enemy, launchLifetime);
    }

    // Una LayerMask guarda las capas como bits: se corre un 1 hasta la posicion de la capa
    // y se compara con AND para ver si esta incluida
    private bool IsInLayerMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}