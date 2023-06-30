using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    public bool CanMove { get; set; } = true; // Variable para controlar el movimiento del jugador

    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del jugador

    private Animator animator;

    [Header("Jump setup")]
    // the key used to activate the push
    public KeyCode key = KeyCode.Space;

    // strength of the push
    public float jumpStrength = 10f;

    [Header("Ground setup")]
    //if the object collides with another object tagged as this, it can jump again
    public string groundTag = "Ground";

    //this determines if the script has to check for when the player touches the ground to enable him to jump again
    //if not, the player can jump even while in the air
    public bool checkGround = true;

    public bool canJump = true;

    public bool hasSecondJump = true;
    private bool canSecondJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene la referencia al Rigidbody2D del jugador
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene la referencia al SpriteRenderer del jugador

        //animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!CanMove)
        {
            return;
        }

        float horizontalMovement = Input.GetAxis("Horizontal"); // Obtiene el movimiento horizontal del jugador

        //animator.SetFloat("Horizontal", Mathf.Abs(horizontalMovement));

        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y); // Aplica el movimiento horizontal al jugador

        // Gira el sprite del jugador según la dirección en la que se mueve
        if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // No gira el sprite horizontalmente
        }
        else if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Gira el sprite horizontalmente
        }
    }

    private void Update()
    {
        if (!CanMove)
        {
            return;
        }

        if (canJump && Input.GetKeyDown(key) || canSecondJump && hasSecondJump && Input.GetKeyDown(key))
        {
            // Apply an instantaneous upwards force
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);

            // Trigger the jump animation
            //animator.SetTrigger("Jump");

            if (!canJump)
            {
                canSecondJump = false;
            }
            canJump = !checkGround;
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (!CanMove)
        {
            return;
        }

        if (checkGround && collisionData.gameObject.CompareTag(groundTag))
        {
            canJump = true;
            canSecondJump = false;
        }
    }
}
