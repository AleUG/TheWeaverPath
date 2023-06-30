using UnityEngine;

public class AtaqueEnemy : MonoBehaviour
{
    public Transform player; // Referencia al objeto jugador
    public float distanciaAtaque = 2f; // Distancia a la que el enemigo comienza a atacar
    public int daño = 10; // Cantidad de daño que hace el enemigo al jugador

    private Animator animator; // Referencia al componente Animator
    private bool haAtacado = false; // Indica si el enemigo ha atacado recientemente

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator del enemigo
    }

    private void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanciaAlJugador = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de ataque
        if (distanciaAlJugador <= distanciaAtaque)
        {
            // Si el enemigo no ha atacado recientemente, realiza el ataque
            if (!haAtacado)
            {
                Atacar();
            }
        }
        else
        {
            haAtacado = false; // Reiniciar el indicador de ataque si el jugador está fuera de alcance
        }
    }

    private void Atacar()
    {
        animator.SetTrigger("Ataque"); // Activar la animación de ataque
        // player.GetComponent<PlayerVida>().RecibirDaño(daño); // Hacer daño al jugador

        haAtacado = true; // Establecer el indicador de ataque a verdadero
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si ha colisionado con el jugador
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerVida>().RecibirDaño(daño);
            haAtacado = false; // Reiniciar el indicador de ataque al acercarse al jugador
        }
    }
}