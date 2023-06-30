using UnityEngine;

public class ColeccionablesManager : MonoBehaviour
{
    public int coleccionablesRequeridos = 10;  // Cantidad de coleccionables requeridos para activar el portal
    public GameObject portal;  // Referencia al objeto "Portal"
    public GameObject mensajePortal;  // Referencia al objeto Canvas "mensajePortal"
    public float duracionMensaje = 3f;  // Duración en segundos que el mensaje estará activo

    private int contadorColeccionables = 0;  // Contador de coleccionables recolectados
    private bool mensajeActivo = false;  // Control para evitar mostrar el mensaje múltiples veces

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coleccionable"))
        {
            contadorColeccionables++;  // Incrementar el contador de coleccionables

            if (contadorColeccionables >= coleccionablesRequeridos)
            {
                ActivarPortal();
                MostrarMensajePortal();
            }

            Destroy(collision.gameObject);  // Destruir el coleccionable
        }
    }

    private void ActivarPortal()
    {
        if (portal != null)
        {
            portal.SetActive(true);  // Activar el objeto "Portal"
        }
    }

    private void MostrarMensajePortal()
    {
        if (mensajePortal != null && !mensajeActivo)
        {
            mensajeActivo = true;
            mensajePortal.SetActive(true);  // Activar el objeto Canvas "mensajePortal"
            Invoke("DesactivarMensajePortal", duracionMensaje);  // Desactivar el mensaje después de la duración especificada
        }
    }

    private void DesactivarMensajePortal()
    {
        if (mensajePortal != null)
        {
            mensajePortal.SetActive(false);  // Desactivar el objeto Canvas "mensajePortal"
            mensajeActivo = false;
        }
    }
}