using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaCollider: MonoBehaviour
{
    public string nombreEscena;  // Nombre de la escena a la que se cambiará

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CambiarEscena();
        }
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}