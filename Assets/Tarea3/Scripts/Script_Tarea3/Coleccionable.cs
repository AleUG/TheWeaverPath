using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    private bool recolectado = false;

    public AudioClip sonidoRecoleccion;
    public AudioSource audioSource;

    private PuntosManager puntosManager;

    private void Start()
    {
        puntosManager = FindObjectOfType<PuntosManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!recolectado && other.CompareTag("Player"))
        {
            recolectado = true;

            if (puntosManager != null)
            {
                puntosManager.AgregarPunto();
                puntosManager.ActualizarContadorUI();
                puntosManager.ActualizarScoreUI();
            }

            ReproducirSonidoRecoleccion();
            Destroy(gameObject);
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
