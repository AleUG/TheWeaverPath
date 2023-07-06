using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a cargar despu�s de la cinem�tica

    private void Start()
    {
        // Reproducir la cinem�tica aqu�

        // Esperar a que la cinem�tica termine
        float cinematicDuration = 17.0f; // Duraci�n de la cinem�tica en segundos
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
