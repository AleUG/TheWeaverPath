using UnityEngine;
using System.Collections;

public class AtaqueEmbestida : MonoBehaviour
{
    public Transform player; // Referencia al objeto jugador
    public float speed = 2f; // velocidad de movimiento del enemigo
    public float maxDistance = 5f; // distancia máxima a la que el enemigo seguirá al jugador
    public int daño = 10; // Cantidad de daño que hace el enemigo al jugador
    public float tiempoSinSeguir = 2f; // Tiempo durante el cual el enemigo no seguirá al jugador después de causar daño

    private float distanceToPlayer; // distancia entre el enemigo y el jugador
    private bool isFollowingPlayer = true; // Variable de estado para controlar si el enemigo sigue al jugador o no

    private Rigidbody2D rb; // Referencia al Rigidbody2D del enemigo
    private Animator animator; // Referencia al componente Animator
    private Vector3 previousPosition; // Posición anterior del objeto

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene la referencia al Rigidbody2D del enemigo
        animator = GetComponent<Animator>();
        previousPosition = transform.position; // Inicializa la posición anterior del objeto
    }

    private void Update()
    {
        // calcular la distancia entre el enemigo y el jugador
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // si el jugador está dentro del rango de visión y el enemigo está siguiendo al jugador
        if (distanceToPlayer < maxDistance && isFollowingPlayer)
        {
            // calcular la dirección hacia el jugador solo en el eje horizontal
            Vector2 direction = new Vector2(player.position.x - transform.position.x, 0f).normalized;

            // mover el enemigo hacia el jugador
            transform.Translate(direction * speed * Time.deltaTime);

            // Girar enemigo en dirección al jugador
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }

        // Detecta el movimiento horizontal
        if (transform.position.x > previousPosition.x)
        {
            // El objeto se está moviendo hacia la derecha
            animator.SetBool("Horizontal", true);
        }
        else if (transform.position.x < previousPosition.x)
        {
            // El objeto se está moviendo hacia la izquierda
            animator.SetBool("Horizontal", true);
        }
        else
        {
            // El objeto no se está moviendo en horizontal
            animator.SetBool("Horizontal", false);
        }

        // Actualiza la posición anterior del objeto
        previousPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si ha colisionado con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerVida playerVida = collision.gameObject.GetComponent<PlayerVida>();
            if (playerVida != null)
            {
                playerVida.RecibirDaño(daño);

                // Dejar de seguir al jugador durante un tiempo
                isFollowingPlayer = false;
                StartCoroutine(ReanudarSeguimiento());
            }
        }
    }

    private IEnumerator ReanudarSeguimiento()
    {
        yield return new WaitForSeconds(tiempoSinSeguir);
        isFollowingPlayer = true;
    }
}