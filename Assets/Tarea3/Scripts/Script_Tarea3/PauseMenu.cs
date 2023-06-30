using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Referencia al canvas del men� de pausa

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Restaurar el timescale a 1 para reanudar el juego
        pauseMenuCanvas.SetActive(false); // Ocultar el canvas del men� de pausa
    }
}