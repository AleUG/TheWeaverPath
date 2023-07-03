using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivarObjetosPermanente : MonoBehaviour
{
    public List<GameObject> objetosDesactivar; // Lista de objetos que se desactivarán al entrar en contacto con el trigger
    public List<GameObject> objetosActivar; // Lista de objetos que se activarán al entrar en contacto con el trigger
    public AudioSource gameplayMusicSource; // Referencia al AudioSource de la música del gameplay

    public bool canvasActive;
    public Animation animationToCheck;
    public string sceneToLoad;

    public bool isSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject objeto in objetosDesactivar)
            {
                objeto.SetActive(false);
            }

            foreach (GameObject objeto in objetosActivar)
            {
                objeto.SetActive(true);
            }

            if (isSound)
            {
                gameplayMusicSource.Stop();
            }

            if (canvasActive)
            {
                StartCoroutine(LoadSceneWithDelay());
            }
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(3f); // Ajusta el tiempo de espera según tus necesidades

        SceneManager.LoadScene(sceneToLoad);
    }
}
