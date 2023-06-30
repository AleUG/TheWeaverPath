using UnityEngine;
using UnityEngine.UI;

public class PlayerVida : MonoBehaviour
{
    public float vidaMaxima; // Vida m�xima del jugador
    public float vidaActual; // Vida actual del jugador
    public GameObject gameOverCanvas; // Referencia al canvas de Game Over

    public Image healthBarImage;

    public AudioSource gameplayMusicSource; // Referencia al AudioSource de la m�sica del gameplay
    public AudioSource gameOverMusicSource; // Referencia al AudioSource de la m�sica del Game Over

    private float gameOverMusicVolume = 0.5f; // Volumen de la m�sica especial para el Game Over

    private bool invulnerable = false;

    private void Start()
    {
        vidaActual = vidaMaxima; // Establece la vida actual al valor m�ximo al iniciar
    }

    public void RecibirDa�o(int cantidad)
    {
        if (invulnerable)
        {
            return;
        }

        vidaActual -= cantidad; // Resta la cantidad de da�o a la vida actual

        if (vidaActual <= 0)
        {
            // El jugador ha perdido toda su vida

            // Deshabilitar los controles del jugador (opcional)
            // Aqu� puedes desactivar otros componentes relacionados con el jugador si es necesario

            // Activar el canvas de Game Over
            gameOverCanvas.SetActive(true);

            // Detener la m�sica del gameplay
            gameplayMusicSource.Stop();

            // Reproducir la m�sica especial para el Game Over
            gameOverMusicSource.volume = gameOverMusicVolume;
            gameOverMusicSource.Play();

            // Destruir el objeto despu�s de 0.1 segundos
            Destroy(gameObject, 0.01f);
        }
    }

    public void ActivarInvulnerabilidad()
    {
        invulnerable = true;
    }

    public void DesactivarInvulnerabilidad()
    {
        invulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un objeto que tiene el script "AtaqueEnemy"
        AtaqueEnemy ataqueEnemy = collision.gameObject.GetComponent<AtaqueEnemy>();
        if (ataqueEnemy != null)
        {
            RecibirDa�o(ataqueEnemy.da�o);
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad; // Suma la cantidad de curaci�n a la vida actual

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima; // Limita la vida actual al valor m�ximo
        }
    }

    public void Update()
    {
        healthBarImage.fillAmount = vidaActual / vidaMaxima;
    }
}
