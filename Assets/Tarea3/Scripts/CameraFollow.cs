using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float lookaheadDistance = 2f;
    public float minXLimit = -10f;
    public float maxXLimit = 10f;
    public bool enableVerticalMovement = true;

    private Vector3 desiredPosition;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calcular la posición deseada de la cámara
        desiredPosition = target.position + offset;

        // Calcular la dirección del jugador
        float direction = Mathf.Sign(target.localScale.x);

        // Si el jugador mira hacia la derecha, ajustar la posición deseada hacia adelante
        if (direction > 0)
            desiredPosition += Vector3.right * lookaheadDistance;

        // Si el jugador mira hacia la izquierda, ajustar la posición deseada hacia adelante
        if (direction < 0)
            desiredPosition += Vector3.left * lookaheadDistance;

        // Aplicar límites a la posición deseada de la cámara
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXLimit, maxXLimit);

        // Aplicar movimiento en el eje Y solo si está habilitado
        if (!enableVerticalMovement)
            desiredPosition.y = transform.position.y;

        // Suavizar el movimiento de la cámara hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar líneas de visualización para los límites en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minXLimit, transform.position.y, transform.position.z), new Vector3(minXLimit, transform.position.y + 5f, transform.position.z));
        Gizmos.DrawLine(new Vector3(maxXLimit, transform.position.y, transform.position.z), new Vector3(maxXLimit, transform.position.y + 5f, transform.position.z));
    }
}
