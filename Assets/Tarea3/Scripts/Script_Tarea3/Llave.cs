using UnityEngine;

public class Llave : MonoBehaviour
{
    public Transform puntoSeguimiento;
    public float velocidadSeguimiento = 2.0f;

    public bool llaveAgarrada = false;
    private float distanciaMinima = 0.8f; // Distancia mínima para abrir la puerta

    public Puerta puertaAsociada; // Referencia a la puerta que esta llave puede abrir
    private float tiempoDestruccion = 0.001f; // Tiempo en segundos antes de destruir la puerta

    public AudioSource sonidoLlave; // AudioSource para el sonido al agarrar la llave

    private void FixedUpdate()
    {
        if (llaveAgarrada)
        {
            if (puntoSeguimiento != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, puntoSeguimiento.position, velocidadSeguimiento * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        if (llaveAgarrada)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanciaMinima);
                foreach (Collider2D collider in colliders)
                {
                    Puerta puerta = collider.GetComponent<Puerta>();
                    if (puerta == puertaAsociada)
                    {
                        puerta.DesactivarPuerta(tiempoDestruccion); // Pasar el tiempo de destrucción como parámetro
                        Invoke("DestruirLlave", tiempoDestruccion); // Invocar la función de destrucción de la llave después del tiempo de destrucción
                        break; // Salir del bucle una vez que se ha encontrado la puerta asociada
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // La llave ha sido agarrada por el jugador
            llaveAgarrada = true;

            // Desactivar el collider para evitar que se produzcan más interacciones
            GetComponent<Collider2D>().enabled = false;

            // Reproducir el sonido de la llave
            if (sonidoLlave != null)
            {
                sonidoLlave.Play();
            }
        }
    }

    private void DestruirLlave()
    {
        Destroy(gameObject);
    }
}
