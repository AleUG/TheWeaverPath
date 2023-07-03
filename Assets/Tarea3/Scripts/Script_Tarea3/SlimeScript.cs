using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpInterval = 2f;
    public float jumpRange = 2f;

    private Rigidbody2D rb;
    private Collider2D collider;
    private float nextJumpTime;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        nextJumpTime = Time.time + jumpInterval;
        isJumping = false;
        // Ignorar colisiones con el jugador (layer "Player")
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"));
    }

    void Update()
    {
        if (Time.time >= nextJumpTime && !isJumping)
        {
            Jump();
        }
    }

    void Jump()
    {
        float randomDirection = Random.Range(-1f, 1f);
        Vector2 jumpForceVector = new Vector2(randomDirection * jumpRange, jumpForce);
        rb.AddForce(jumpForceVector, ForceMode2D.Impulse);
        isJumping = true;

        nextJumpTime = Time.time + jumpInterval;
        Invoke(nameof(EndJump), 1f);
    }

    void EndJump()
    {
        isJumping = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Permitir que el jugador atraviese al enemigo
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collider, collision.collider, true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Reactivar las colisiones con el jugador al salir del contacto
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collider, collision.collider, false);
        }
    }
}
