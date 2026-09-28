using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerRespawn respawn;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        respawn = GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        // Durante el respawn el jugador flota con la velocidad en cero: el estado lo maneja PlayerRespawn
        if (respawn.IsRespawning())
        {
            animator.SetBool("isGrounded", false);
            return;
        }

        animator.SetBool("isGrounded", rb.linearVelocity.y == 0f);
    }
}