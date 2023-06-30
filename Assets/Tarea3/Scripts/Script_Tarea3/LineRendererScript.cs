using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public LineRenderer lineRenderer;

    private void Start()
    {
        // Obtener el componente LineRenderer adjunto al objeto
        lineRenderer = GetComponent<LineRenderer>();

        // Establecer los puntos iniciales del LineRenderer
        lineRenderer.SetPosition(0, puntoA.position);
        lineRenderer.SetPosition(1, puntoB.position);
    }

    private void Update()
    {
        // Actualizar los puntos del LineRenderer si los puntos A y B cambian
        lineRenderer.SetPosition(0, puntoA.position);
        lineRenderer.SetPosition(1, puntoB.position);
    }
}
