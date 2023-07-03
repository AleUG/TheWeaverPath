using UnityEngine;
using System.Collections.Generic;

public class StaticCamera : MonoBehaviour
{
    public Transform player;
    public List<Camera> playerCameras;
    public float activationDistance = 5f;

    private Camera currentCamera;
    private bool isSwitchingCamera = false;

    void Start()
    {
        // Desactivar todas las cámaras al inicio
        foreach (Camera camera in playerCameras)
        {
            camera.gameObject.SetActive(false);
        }

        // Obtener la cámara más cercana al jugador al inicio
        currentCamera = GetClosestCamera();
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (!isSwitchingCamera && ShouldSwitchCamera())
        {
            SwitchCamera();
        }
    }

    bool ShouldSwitchCamera()
    {
        if (currentCamera == null)
        {
            // No hay cámara activa actualmente
            return true;
        }

        float distanceToCurrentCamera = GetDistanceToCamera(currentCamera);
        return distanceToCurrentCamera > activationDistance;
    }

    void SwitchCamera()
    {
        Camera nextCamera = GetClosestCamera();
        if (nextCamera != null && nextCamera != currentCamera)
        {
            // Desactivar la cámara actual
            currentCamera.gameObject.SetActive(false);

            // Activar la siguiente cámara
            nextCamera.gameObject.SetActive(true);

            // Actualizar la referencia a la cámara actual
            currentCamera = nextCamera;
        }
    }

    float GetDistanceToCamera(Camera camera)
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 playerToCamera = camera.transform.position - player.position;

        // Proyectar el vector playerToCamera en la dirección de la cámara
        float distance = Vector3.Dot(playerToCamera, cameraForward);

        // Si la distancia proyectada es negativa, el jugador está detrás de la cámara
        // En ese caso, la distancia a la cámara es la distancia euclidiana entre el jugador y la cámara
        if (distance < 0f)
        {
            distance = playerToCamera.magnitude;
        }

        return distance;
    }

    Camera GetClosestCamera()
    {
        Camera closestCamera = null;
        float closestDistance = float.MaxValue;

        foreach (Camera camera in playerCameras)
        {
            float distanceToCamera = GetDistanceToCamera(camera);
            if (distanceToCamera < closestDistance)
            {
                closestCamera = camera;
                closestDistance = distanceToCamera;
            }
        }

        return closestCamera;
    }
}
