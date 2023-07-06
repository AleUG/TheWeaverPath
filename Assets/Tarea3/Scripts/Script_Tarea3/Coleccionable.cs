using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Coleccionable : MonoBehaviour
{
    public static int puntos = 0;  // Contador de puntos estático
    private bool recolectado = false;  // Control para evitar la recolección múltiple

    public Text contadorText;  // Referencia al componente de texto para mostrar el contador de coleccionables
    public Text scoreText;  // Referencia al componente de texto para mostrar el score adicional

    public AudioClip sonidoRecoleccion;  // Sonido al recolectar el coleccionable
    public AudioClip sonidoGastoColeccionable;  // Sonido al gastar un coleccionable
    public AudioSource audioSource;  // Referencia al componente AudioSource para reproducir los sonidos

    public KeyCode teclaGastarColeccionable; // Tecla para gastar coleccionables
    public PlayerVida playerVida; // Referencia al script PlayerVida para manipular la vida del jugador

    private void Start()
    {
        if (contadorText == null || scoreText == null)
        {
            Debug.LogError("No se encontraron los componentes de texto en la interfaz de usuario.");
        }
        else
        {
            SceneManager.sceneLoaded += ReiniciarPuntos;  // Suscribirse al evento de carga de escena para reiniciar los puntos en MenúPrincipal

            puntos = PlayerPrefs.GetInt("Puntos", 0);  // Obtener el valor del score guardado en PlayerPrefs
            ActualizarContadorUI();
            ActualizarScoreUI();
        }
    }

    private void ReiniciarPuntos(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenúPrincipal")
        {
            puntos = 0;  // Reiniciar los puntos a cero
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
            ActualizarScoreUI();
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
            int score = ObtenerScore();
            scoreText.text = score.ToString();
            Debug.Log("Score actualizado: " + score);
        }
    }

    private void ReproducirSonidoRecoleccion()
    {
        if (audioSource != null && sonidoRecoleccion != null)
        {
            audioSource.PlayOneShot(sonidoRecoleccion);
        }
    }

    private void ReproducirSonidoGastoColeccionable()
    {
        if (audioSource != null && sonidoGastoColeccionable != null)
        {
            audioSource.PlayOneShot(sonidoGastoColeccionable);
        }
    }

    public int ObtenerScore()
    {
        // Cálculo del score adicional basado en la cantidad de coleccionables recolectados
        return puntos * 100;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Puntos", puntos);  // Guardar el valor del score en PlayerPrefs al destruir el objeto
        PlayerPrefs.Save();  // Guardar los cambios en PlayerPrefs
    }

    private void Update()
    {
        if (Input.GetKeyDown(teclaGastarColeccionable))
        {
            if (puntos > 0)
            {
                puntos--;
                ActualizarContadorUI();
                playerVida.SetVidaMaxima(); // Establecer la vida del jugador en su valor máximo
                ReproducirSonidoGastoColeccionable(); // Reproducir el sonido de gasto de coleccionable
            }
        }
    }
}
