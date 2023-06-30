using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject objetoActivar; // El objeto que se activará al entrar en contacto con el trigger
    public GameObject keyInteract;
    public KeyCode teclaActivacion = KeyCode.E; // La tecla para activar el objeto

    private bool enContacto;

    private void Start()
    {
        keyInteract.SetActive(false);
    }

    private void Update()
    {
        if (enContacto && Input.GetKeyDown(teclaActivacion))
        {
            objetoActivar.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = true;
            keyInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = false;
            keyInteract.SetActive(false);
        }
    }
}
