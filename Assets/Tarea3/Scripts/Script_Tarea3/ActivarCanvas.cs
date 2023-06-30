using UnityEngine;
using UnityEngine.UI;

public class ActivarCanvas : MonoBehaviour
{
    public Button botonActivarDesactivar; // Referencia al bot�n que activar�/desactivar� el otro Canvas
    public Canvas canvasObjetivo; // Referencia al Canvas que se desea activar/desactivar

    private void Start()
    {
        botonActivarDesactivar.onClick.AddListener(ActivarDesactivarCanvasObjetivo);
    }

    private void ActivarDesactivarCanvasObjetivo()
    {
        canvasObjetivo.gameObject.SetActive(!canvasObjetivo.gameObject.activeSelf);
    }
}