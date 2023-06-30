using UnityEngine;

public class InteractuarTecla : MonoBehaviour
{
    public GameObject palanca; // La palanca que se activará al entrar en contacto con el trigger
    public GameObject keyInteract;
    public GameObject[] objetos;

    public KeyCode teclaActivacion = KeyCode.E; // La tecla para activar la palanca
    public AudioSource sonidoPalanca; // El AudioSource para reproducir el sonido de la palanca
    private Animator palancaAnimator;
    private bool palancaActivada = true;

    private bool enContacto;

    private void Start()
    {
        keyInteract.SetActive(false);
        palancaAnimator = palanca.GetComponent<Animator>(); // Obtener el componente Animator de la palanca

        // Establecer el estado inicial del parámetro "Activado" de la palanca
        palancaAnimator.SetBool("Activado", palancaActivada);

        // Establecer la velocidad de reproducción de la animación de la palanca a 0
        palancaAnimator.speed = 0f;

        // Establecer la posición inicial de la palanca
        float initialPosition = palancaActivada ? 1f : 0f;
        palancaAnimator.Play("PalancaAnimation", 0, initialPosition);
    }

    private void Update()
    {
        if (enContacto && Input.GetKeyDown(teclaActivacion))
        {
            // Cambiar el estado del parámetro "Activado" de la palanca
            palancaActivada = !palancaActivada;
            palancaAnimator.SetBool("Activado", palancaActivada);

            // Reproducir el sonido de la palanca
            if (sonidoPalanca != null)
            {
                sonidoPalanca.Play();
            }

            for (int i = 0; i < objetos.Length; i++)
            {
                // Invertir el estado de cada objeto relacionado
                objetos[i].SetActive(!objetos[i].activeSelf);
            }

            // Establecer la velocidad de reproducción de la animación de la palanca a 1
            palancaAnimator.speed = 1f;

            // Iniciar la animación de la palanca
            palancaAnimator.Play("PalancaAnimation");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = true;
            keyInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = false;
            keyInteract.SetActive(false);
        }
    }
}