using System;
using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float appearDuration = .5f;
    [SerializeField] private float respawnHeight = 3f; // Sobre la posicion original
    [SerializeField] private KeyCode dropKey = KeyCode.Space;

    public event Action OnRespawnStarted;
    public event Action OnRespawnFinished;

    private Rigidbody2D rb;
    private Collider2D col;
    private Jumper jumper;
    private Animator animator;

    private Vector3 startPosition;
    private Vector3 originalScale; // Se guarda por el flip del sprite: la X puede ser negativa
    private bool isRespawning;

    public bool IsRespawning() => isRespawning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        jumper = GetComponent<Jumper>();
        animator = GetComponent<Animator>();

        startPosition = transform.position;
        originalScale = transform.localScale;
    }

    public void Begin()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;
        OnRespawnStarted?.Invoke();

        // El mundo sigue corriendo, se apaga el jugador y mientras no se suman puntos
        jumper.enabled = false;
        col.enabled = false;
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;

        // Animacion de salto mientras flota, asi no corre en el aire
        animator.SetBool("IsGrounded", false);

        transform.position = startPosition + Vector3.up * respawnHeight;
        transform.localScale = Vector3.zero;

        float elapsed = 0f;

        while (elapsed < appearDuration)
        {
            elapsed += Time.deltaTime;

            transform.localScale = originalScale * Mathf.SmoothStep(0f, 1f, elapsed / appearDuration);

            yield return null;
        }

        transform.localScale = originalScale;

        // Queda flotando hasta que el jugador decide caer
        while (!Input.GetKeyDown(dropKey))
            yield return null;

        col.enabled = true;
        rb.simulated = true;
        jumper.enabled = true;

        isRespawning = false;
        OnRespawnFinished?.Invoke();
    }
}