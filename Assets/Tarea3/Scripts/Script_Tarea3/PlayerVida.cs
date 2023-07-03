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
    public AudioSource damageAudioSource; // Referencia al AudioSource para reproducir el sonido de daño

    private float gameOverMusicVolume = 0.5f; // Volumen de la música especial para el Game Over

    private bool invulnerable = false;
    private bool isTakingDamage = false; // Indica si el jugador está recibiendo daño actualmente
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float reboteForce = 10f;
    public float invulnerabilityDuration = 2f;
    public float blinkInterval = 0.2f;

    private void Start()
    {
        vidaActual = vidaMaxima; // Establece la vida actual al valor máximo al iniciar
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RecibirDaño(int cantidad)
    {
        if (invulnerable || isTakingDamage)
        {
            return;
        }

        vidaActual -= cantidad; // Resta la cantidad de daño a la vida actual

        if (vidaActual <= 0)
        {
            // El jugador ha perdido toda su vida

            // Deshabilitar los controles del jugador (opcional)
            // Aquí puedes desactivar otros componentes relacionados con el jugador si es necesario

            // Reproducir el sonido de daño
            if (damageAudioSource != null)
            {
                damageAudioSource.Play();
            }

            // Detener la música del gameplay
            gameplayMusicSource.Stop();

            gameOverCanvas.SetActive(true);
            // Reproducir la música especial para el Game Over
            gameOverMusicSource.volume = gameOverMusicVolume;
            gameOverMusicSource.Play();

            // Desactivar el objeto después de la duración del sonido de daño
            gameObject.SetActive(false);
        }
        else
        {
            // Rebote del jugador al recibir daño
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * reboteForce, ForceMode2D.Impulse);

            // Activar invulnerabilidad y el parpadeo del sprite
            ActivarInvulnerabilidad();
            InvokeRepeating("ToggleSpriteRenderer", 0f, blinkInterval);
            Invoke("DesactivarInvulnerabilidad", invulnerabilityDuration);

            // Reproducir el sonido de daño
            if (damageAudioSource != null)
            {
                damageAudioSource.Play();
            }

            // Indicar que el jugador está recibiendo daño
            isTakingDamage = true;
        }
    }

    public void ActivarInvulnerabilidad()
    {
        invulnerable = true;
    }

    public void DesactivarInvulnerabilidad()
    {
        invulnerable = false;
        spriteRenderer.enabled = true;
        CancelInvoke("ToggleSpriteRenderer");
        isTakingDamage = false;
    }

    private void ToggleSpriteRenderer()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad; // Suma la cantidad de curación a la vida actual

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima; // Limita la vida actual al valor máximo
        }
    }

    public void SetVidaMaxima()
    {
        vidaActual = vidaMaxima;
        // Lógica adicional, si es necesario
    }

    public void Update()
    {
        healthBarImage.fillAmount = vidaActual / vidaMaxima;
    }
}
