using UnityEngine;
using UnityEngine.UI;

public class PlayerVida : MonoBehaviour
{
    public float vidaMaxima; // Vida máxima del jugador
    public float vidaActual; // Vida actual del jugador
    public GameObject gameOverCanvas; // Referencia al canvas de Game Over

    public Image healthBarImage;

    public AudioSource gameplayMusicSource; // Referencia al AudioSource de la música del gameplay
    public AudioSource gameOverMusicSource; // Referencia al AudioSource de la música del Game Over

    private float gameOverMusicVolume = 0.5f; // Volumen de la música especial para el Game Over

    private bool invulnerable = false;

    private void Start()
    {
        vidaActual = vidaMaxima; // Establece la vida actual al valor máximo al iniciar
    }

    public void RecibirDaño(int cantidad)
    {
        if (invulnerable)
        {
            return;
        }

        vidaActual -= cantidad; // Resta la cantidad de daño a la vida actual

        if (vidaActual <= 0)
        {
            // El jugador ha perdido toda su vida

            // Deshabilitar los controles del jugador (opcional)
            // Aquí puedes desactivar otros componentes relacionados con el jugador si es necesario

            // Activar el canvas de Game Over
            gameOverCanvas.SetActive(true);

            // Detener la música del gameplay
            gameplayMusicSource.Stop();

            // Reproducir la música especial para el Game Over
            gameOverMusicSource.volume = gameOverMusicVolume;
            gameOverMusicSource.Play();

            // Destruir el objeto después de 0.1 segundos
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
            RecibirDaño(ataqueEnemy.daño);
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad; // Suma la cantidad de curación a la vida actual

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima; // Limita la vida actual al valor máximo
        }
    }

    public void Update()
    {
        healthBarImage.fillAmount = vidaActual / vidaMaxima;
    }
}
