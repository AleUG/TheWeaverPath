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
        // Desactivar todas las c�maras al inicio
        foreach (Camera camera in playerCameras)
        {
            camera.gameObject.SetActive(false);
        }

        // Obtener la c�mara m�s cercana al jugador al inicio
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
            // No hay c�mara activa actualmente
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
            // Desactivar la c�mara actual
            currentCamera.gameObject.SetActive(false);

            // Activar la siguiente c�mara
            nextCamera.gameObject.SetActive(true);

            // Actualizar la referencia a la c�mara actual
            currentCamera = nextCamera;
        }
    }

    float GetDistanceToCamera(Camera camera)
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 playerToCamera = camera.transform.position - player.position;

        // Proyectar el vector playerToCamera en la direcci�n de la c�mara
        float distance = Vector3.Dot(playerToCamera, cameraForward);

        // Si la distancia proyectada es negativa, el jugador est� detr�s de la c�mara
        // En ese caso, la distancia a la c�mara es la distancia euclidiana entre el jugador y la c�mara
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
