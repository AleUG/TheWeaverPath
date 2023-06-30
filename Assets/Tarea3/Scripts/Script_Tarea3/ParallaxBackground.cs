using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player; // Referencia al objeto que controla el movimiento horizontal
    public float parallaxFactor = 0.5f; // Factor de desplazamiento del parallax

    private Vector3 previousPlayerPosition;

    private void Start()
    {
        previousPlayerPosition = player.position;
    }

    private void Update()
    {
        float playerDeltaX = player.position.x - previousPlayerPosition.x;
        transform.position += Vector3.right * (playerDeltaX * parallaxFactor);

        previousPlayerPosition = player.position;
    }
}
