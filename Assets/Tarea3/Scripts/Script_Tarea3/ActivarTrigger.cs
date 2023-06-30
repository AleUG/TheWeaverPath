using UnityEngine;

public class ActivarTrigger : MonoBehaviour
{
    public GameObject objetoActivar; // El objeto que se activará al entrar en contacto con el trigger

    private bool enContacto;

    private void Update()
    {
        if (enContacto)
        {
            objetoActivar.SetActive(true);
        }
        else
        {
            objetoActivar.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = false;
        }
    }
}