using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a cargar después de la cinemática

    private void Start()
    {
        // Reproducir la cinemática aquí

        // Esperar a que la cinemática termine
        float cinematicDuration = 17.0f; // Duración de la cinemática en segundos
        Invoke("LoadNextScene", cinematicDuration);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Cargar la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
