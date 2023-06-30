using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    public float speed = 10f;
    private int direction;

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    private void Update()
    {
        // Mueve la flecha en línea recta en la dirección establecida
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprueba si la flecha ha colisionado con el jugador
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerVida>().RecibirDaño(20); // Inflije daño al jugador

            Destroy(gameObject); // Destruye la flecha después de colisionar
        }
    }
}