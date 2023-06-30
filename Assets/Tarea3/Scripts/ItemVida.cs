using UnityEngine;

public class ItemVida : MonoBehaviour
{
    public int cantidadRecuperacion = 20; // Cantidad de vida a recuperar al tomar el item

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerVida playerVida = other.GetComponent<PlayerVida>();
            if (playerVida != null)
            {
                playerVida.Curar(cantidadRecuperacion); // Llama al m�todo Curar del script PlayerVida
            }

            Destroy(gameObject); // Destruye el objeto del item
        }
    }
}