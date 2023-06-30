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
        // Verificar si el jugador está dentro del rango de ataque
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        isPlayerInRange = distanceToPlayer <= attackRange;

        if (isPlayerInRange && !isAttacking)
        {
            // El jugador está dentro del rango y no estamos atacando, así que comenzamos el ataque
            Attack();
        }

        if (isAttacking)
        {
            // Si estamos atacando, actualizamos el temporizador de ataque
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackInterval)
            {
                // Ha pasado el tiempo suficiente desde el último ataque, así que atacamos nuevamente
                Attack();
            }
        }

        // Voltear el enemigo en el eje X según la posición relativa del jugador
        if (player.transform.position.x < transform.position.x)
        {
            // El jugador está a la izquierda del enemigo, voltear hacia la izquierda
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            // El jugador está a la derecha del enemigo, voltear hacia la derecha
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Attack()
    {
        // Reproducir la animación de ataque
        animator.SetTrigger("Ataque");
    }

    // Método invocado por la animación al completarse
    public void OnAttackAnimationEnd()
    {
        // Aquí puedes agregar lógica adicional para infligir daño al jugador o realizar cualquier otra acción de ataque

        // Instanciar el prefab de la flecha en el punto de spawneo y configurar su dirección
        GameObject arrow = null;
        if (transform.localScale.x > 0)
        {
            arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, rotationRight)));
        }
        else
        {
            arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, rotationLeft)));
        }

        // Obtener la dirección en la que se disparará la flecha según el flip del arquero
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Aplicar la dirección a la velocidad del Rigidbody2D de la flecha
        arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Ajusta la velocidad según tus necesidades

        // Reproducir el sonido de spawnear la flecha
        if (arrowSpawnSound != null)
        {
            audioSource.PlayOneShot(arrowSpawnSound);
        }

        // Reiniciar el temporizador de ataque
        attackTimer = 0f;
    }
}