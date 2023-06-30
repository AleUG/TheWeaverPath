using UnityEngine;
using UnityEngine.UI;

public class Coleccionable : MonoBehaviour
{
    public static int puntos = 0;  // Contador de puntos estático
    private bool recolectado = false;  // Control para evitar la recolección múltiple

    public Text contadorText;  // Referencia al componente de texto para mostrar el contador
    public AudioClip sonidoRecoleccion;  // Sonido al recolectar el coleccionable
    public AudioSource audioSource;  // Referencia al componente AudioSource para reproducir el sonido

    private void Start()
    {
        if (contadorText == null)
        {
            Debug.LogError("No se encontró el componente de texto en la interfaz de usuario.");
        }
        else
        {
            ActualizarContadorUI();
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
            Debug.Log("Contador actualizado: " + puntos);
        }
    }

    private void ReproducirSonidoRecoleccion()
    {
        if (audioSource != null && sonidoRecoleccion != null)
        {
            audioSource.PlayOneShot(sonidoRecoleccion);
        }
    }
}