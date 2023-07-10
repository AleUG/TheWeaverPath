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
        lineRenderer.SetPosition(0, new Vector3(puntoA.position.x, puntoA.position.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(puntoB.position.x, puntoB.position.y, 0f));
    }

    private void LateUpdate()
    {
        // Actualizar los puntos del LineRenderer si los puntos A y B cambian
        lineRenderer.SetPosition(0, new Vector3(puntoA.position.x, puntoA.position.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(puntoB.position.x, puntoB.position.y, 0f));
    }
}
