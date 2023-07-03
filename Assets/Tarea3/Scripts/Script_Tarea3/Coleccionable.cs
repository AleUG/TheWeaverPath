using UnityEngine;
using UnityEngine.UI;

public class Coleccionable : MonoBehaviour
{
    public static int puntos = 0;  // Contador de puntos estático
    private bool recolectado = false;  // Control para evitar la recolección múltiple

    public Text contadorText;  // Referencia al componente de texto para mostrar el contador de coleccionables
    public Text scoreText;  // Referencia al componente de texto para mostrar el score adicional

    public AudioClip sonidoRecoleccion;  // Sonido al recolectar el coleccionable
    public AudioSource audioSource;  // Referencia al componente AudioSource para reproducir el sonido

    private void Start()
    {
        if (contadorText == null || scoreText == null)
        {
            Debug.LogError("No se encontraron los componentes de texto en la interfaz de usuario.");
        }
        else
        {
            ActualizarContadorUI();
            ActualizarScoreUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!recolectado && other.CompareTag("Player"))
        {
            recolectado = true;  // Marcamos el coleccionable como recolectado
            puntos++;
            ActualizarContadorUI();
            ReproducirSonidoRecoleccion();
            Destroy(gameObject);
        }
    }

    private void ActualizarContadorUI()
    {
        if (contadorText != null)
        {
            contadorText.text = puntos.ToString();
            Debug.Log("Contador de coleccionables actualizado: " + puntos);
        }
    }

    private void ActualizarScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = ObtenerScore().ToString();
            Debug.Log("Score actualizado: " + ObtenerScore());
        }
    }

    private void ReproducirSonidoRecoleccion()
    {
        if (audioSource != null && sonidoRecoleccion != null)
        {
            audioSource.PlayOneShot(sonidoRecoleccion);
        }
    }

    public int ObtenerScore()
    {
        // Cálculo del score adicional basado en la cantidad de coleccionables recolectados
        return puntos * 100;
    }
}
