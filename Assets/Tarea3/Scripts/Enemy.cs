using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // el objeto jugador
    public float speed = 2f; // velocidad de movimiento del enemigo
    public float maxDistance = 5f; // distancia máxima a la que el enemigo seguirá al jugador
    public int daño = 1; // Cantidad de daño que la sierra inflige al jugador

    private float distanceToPlayer; // distancia entre el enemigo y el jugador

    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    private Animator animator;

    private Vector3 previousPosition; // Posición anterior del objeto

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene la referencia al Rigidbody2D del jugador

        animator = GetComponent<Animator>();

        previousPosition = transform.position; // Inicializa la posición anterior del objeto
    }

    private void Update()
    {
        // calcular la distancia entre el enemigo y el jugador
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // si el jugador está dentro del rango de visión
        if (distanceToPlayer < maxDistance)
        {
            // calcular la dirección hacia el jugador
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

        else
        {
            player = null;
        }

        //// Detecta el movimiento horizontal
        //if (transform.position.x > previousPosition.x)
        //{
            // El objeto se está moviendo hacia la derecha
        //    animator.SetBool("Horizontal", true);
        //}
        //else if (transform.position.x < previousPosition.x)
        //{
            // El objeto se está moviendo hacia la izquierda
        //    animator.SetBool("Horizontal", true);
        //}
        //else
        //{
            // El objeto no se está moviendo en horizontal
        //    animator.SetBool("Horizontal", false);
        //}

        // Actualiza la posición anterior del objeto
        previousPosition = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Si entra en contacto con el jugador
        if (collision.CompareTag("Player"))
        {
            // Obtener el componente PlayerVida del jugador
            PlayerVida playerVida = collision.GetComponent<PlayerVida>();
            if (playerVida != null)
            {
                // Infligir daño al jugador
                playerVida.RecibirDaño(daño);
            }
        }
    }
}