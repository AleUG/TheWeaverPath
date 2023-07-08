using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuntosManager : MonoBehaviour
{
    public static int puntos = 0;
    public Text contadorText;
    public Text scoreText;
    public KeyCode teclaGastarColeccionable;
    public PlayerVida playerVida;

    public AudioClip sonidoGastoColeccionable;
    public AudioSource audioSource;

    private void Start()
    {
        if (contadorText == null)
        {
            Debug.LogError("No se encontró el componente de texto en la interfaz de usuario.");
        }
        else
        {
            puntos = PlayerPrefs.GetInt("Puntos", 0);
            ActualizarContadorUI();
        }

        SceneManager.sceneLoaded += ReiniciarPuntos;
    }

    private void ReiniciarPuntos(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenúPrincipal")
        {
            puntos = 0;
            ActualizarContadorUI();
        }
    }

    public void AgregarPunto()
    {
        puntos++;
    }

    public void ActualizarContadorUI()
    {
        if (contadorText != null)
        {
            contadorText.text = puntos.ToString();
            Debug.Log("Contador de coleccionables actualizado: " + puntos);
        }
    }

    public void ActualizarScoreUI()
    {
        if (scoreText != null)
        {
            int score = ObtenerScore();
            scoreText.text = score.ToString();
            Debug.Log("Score actualizado: " + score);
        }
    }

    public int ObtenerScore()
    {
        return puntos * 100;
    }

    private void ReproducirSonidoGastoColeccionable()
    {
        if (audioSource != null && sonidoGastoColeccionable != null)
        {
            audioSource.PlayOneShot(sonidoGastoColeccionable);
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Puntos", puntos);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(teclaGastarColeccionable))
        {
            if (puntos > 0)
            {
                puntos--;
                ActualizarContadorUI();
                playerVida.SetVidaMaxima();
                ReproducirSonidoGastoColeccionable();
            }
        }
    }
}
