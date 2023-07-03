using UnityEngine;

public class SierraEnemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5f;
    public int daño = 10; // Cantidad de daño que la sierra inflige al jugador

    private Transform target;

    private void Start()
    {
        // Establecer el punto A como destino inicial si está asignado
        if (pointA != null)
        {
            target = pointA;
        }
        else
        {
            // Si no hay punto A asignado, detener el movimiento
            target = null;
        }
    }

    private void Update()
    {
        // Si no hay punto de destino asignado, no se realiza el movimiento
        if (target == null)
        {
            return;
        }

        // Mover hacia el destino actual
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Si el enemigo llega al destino, cambiar al siguiente destino
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            if (target == pointA)
            {
                // Si hay un punto B asignado, cambiar al punto B
                if (pointB != null)
                {
                    target = pointB;
                }
                else
                {
                    // Si no hay punto B asignado, detener el movimiento
                    target = null;
                }
            }
            else if (target == pointB)
            {
                // Si hay un punto A asignado, cambiar al punto A
                if (pointA != null)
                {
                    target = pointA;
                }
                else
                {
                    // Si no hay punto A asignado, detener el movimiento
                    target = null;
                }
            }
        }
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
