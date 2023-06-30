using UnityEngine;
using UnityEngine.UI;

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 100; // Vida máxima del enemigo
    public int vidaActual; // Vida actual del enemigo
    public GameObject prefabItemVida; // Prefab del item de vida a soltar

    public Image healthBarImage;

    private void Start()
    {
        vidaActual = vidaMaxima; // Establecer la vida actual al valor máximo al iniciar
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de daño a la vida actual

        if (vidaActual <= 0)
        {
            SoltarItemVida();
            DestruirEnemigo();
        }
    }

    private void SoltarItemVida()
    {
        if (prefabItemVida != null)
        {
            Instantiate(prefabItemVida, transform.position, Quaternion.identity); // Crea una instancia del prefab del item de vida en la posición del enemigo
        }
    }

    private void DestruirEnemigo()
    {
        // Aquí puedes agregar cualquier lógica adicional antes de destruir el enemigo, como reproducción de efectos de partículas o sonidos

        Destroy(gameObject); // Destruir el objeto enemigo
    }

    public void Update()
    {
        healthBarImage.fillAmount = vidaActual / vidaMaxima;
    }
}