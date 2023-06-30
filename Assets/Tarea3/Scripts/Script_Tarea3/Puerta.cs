using UnityEngine;

public class Puerta : MonoBehaviour
{
    public GameObject puertaLlave;
    public KeyCode teclaInteraccion = KeyCode.E; // La tecla para la interacción
    public GameObject botonInteraccion;
    public AudioSource sonidoSinLlave; // AudioClip para el sonido al intentar abrir la puerta sin la llave
    public AudioSource sonidoConLlave; // AudioClip para el sonido al abrir la puerta con la llave

    public Llave llave; // Referencia al script Llave

    private bool enContacto;
    private bool puertaAbierta;

    private void Start()
    {
        botonInteraccion.SetActive(false);
    }

    private void Update()
    {
        if (enContacto && Input.GetKeyDown(teclaInteraccion))
        {
            if (llave != null && llave.llaveAgarrada)
            {
                if (!puertaAbierta)
                {
                    Debug.Log("Sonido Con Llave");
                    sonidoConLlave.Play();
                    DesactivarPuerta(0f); // Llamada al método con retraso de 0 segundos
                }
            }
            else
            {
                Debug.Log("Sonido sin Llave");
                sonidoSinLlave.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enContacto = true;
            botonInteraccion.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enContacto = false;
            botonInteraccion.SetActive(false);
        }
    }

    public void DesactivarPuerta(float delay)
    {
        if (!puertaAbierta)
        {
            StartCoroutine(DesactivarPuertaConRetraso(delay));
        }
    }

    private System.Collections.IEnumerator DesactivarPuertaConRetraso(float delay)
    {
        yield return new WaitForSeconds(delay);
        puertaLlave.SetActive(false);
        puertaAbierta = true;
    }
}
