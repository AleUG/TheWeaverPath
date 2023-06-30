using UnityEngine;

public class AtaqueArquero : MonoBehaviour
{
    public float attackRange = 3f;
    public float attackInterval = 1f;
    public GameObject player;
    public Animator animator;
    public GameObject arrowPrefab; // Prefab de la flecha
    public Transform arrowSpawnPoint; // Punto de spawneo de la flecha

    public float rotationLeft;
    public float rotationRight;
    public AudioClip arrowSpawnSound; // Sonido al spawnear la flecha

    private bool isPlayerInRange;
    private bool isAttacking;
    private float attackTimer;
    private AudioSource audioSource;

    private void Start()
    {
        // Obtener una referencia al objeto del jugador
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Verificar si el jugador est� dentro del rango de ataque
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        isPlayerInRange = distanceToPlayer <= attackRange;

        if (isPlayerInRange && !isAttacking)
        {
            // El jugador est� dentro del rango y no estamos atacando, as� que comenzamos el ataque
            Attack();
        }

        if (isAttacking)
        {
            // Si estamos atacando, actualizamos el temporizador de ataque
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackInterval)
            {
                // Ha pasado el tiempo suficiente desde el �ltimo ataque, as� que atacamos nuevamente
                Attack();
            }
        }

        // Voltear el enemigo en el eje X seg�n la posici�n relativa del jugador
        if (player.transform.position.x < transform.position.x)
        {
            // El jugador est� a la izquierda del enemigo, voltear hacia la izquierda
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            // El jugador est� a la derecha del enemigo, voltear hacia la derecha
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Attack()
    {
        // Reproducir la animaci�n de ataque
        animator.SetTrigger("Ataque");
    }

    // M�todo invocado por la animaci�n al completarse
    public void OnAttackAnimationEnd()
    {
        // Aqu� puedes agregar l�gica adicional para infligir da�o al jugador o realizar cualquier otra acci�n de ataque

        // Instanciar el prefab de la flecha en el punto de spawneo y configurar su direcci�n
        GameObject arrow = null;
        if (transform.localScale.x > 0)
        {
            arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, rotationRight)));
        }
        else
        {
            arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, rotationLeft)));
        }

        // Obtener la direcci�n en la que se disparar� la flecha seg�n el flip del arquero
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Aplicar la direcci�n a la velocidad del Rigidbody2D de la flecha
        arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Ajusta la velocidad seg�n tus necesidades

        // Reproducir el sonido de spawnear la flecha
        if (arrowSpawnSound != null)
        {
            audioSource.PlayOneShot(arrowSpawnSound);
        }

        // Reiniciar el temporizador de ataque
        attackTimer = 0f;
    }
}