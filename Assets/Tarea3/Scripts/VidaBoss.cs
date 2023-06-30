using UnityEngine;
using UnityEngine.UI;

public class VidaBoss : MonoBehaviour
{
    public int vidaMaxima = 100; // Vida m�xima del enemigo
    public int vidaActual; // Vida actual del enemigo
    public GameObject prefabItemVida; // Prefab del item de vida a soltar

    public Image healthBarImage;

    private void Start()
    {
        vidaActual = vidaMaxima; // Establecer la vida actual al valor m�ximo al iniciar
    }

    public void RecibirDa�o(int cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de da�o a la vida actual

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
            Instantiate(prefabItemVida, transform.position, Quaternion.identity); // Crea una instancia del prefab del item de vida en la posici�n del enemigo
        }
    }

    private void DestruirEnemigo()
    {
        // Aqu� puedes agregar cualquier l�gica adicional antes de destruir el enemigo, como reproducci�n de efectos de part�culas o sonidos

        Destroy(gameObject); // Destruir el objeto enemigo
    }

    public void Update()
    {
        healthBarImage.fillAmount = vidaActual / vidaMaxima;
    }
}