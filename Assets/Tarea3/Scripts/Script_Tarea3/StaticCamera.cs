using UnityEngine;
using System.Collections;

public class StaticCamera : MonoBehaviour
{
    public Transform player;
    public Camera[] cameras;
    public float cameraSwitchDelay = 0.5f;

    private int currentCameraIndex = 0;
    private bool isSwitchingCamera = false;

    void Start()
    {
        EnableCamera(currentCameraIndex);
    }

    void Update()
    {
        if (!isSwitchingCamera && !IsPlayerInView(cameras[currentCameraIndex]))
        {
            SwitchCamera();
        }
    }

    bool IsPlayerInView(Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, player.GetComponent<Collider2D>().bounds);
    }

    void SwitchCamera()
    {
        int nextCameraIndex = (currentCameraIndex + 1) % cameras.Length;
        StartCoroutine(SwitchCameraDelayed(nextCameraIndex));
    }

    System.Collections.IEnumerator SwitchCameraDelayed(int nextCameraIndex)
    {
        isSwitchingCamera = true;
        DisableCamera(currentCameraIndex);
        yield return new WaitForSeconds(cameraSwitchDelay);
        EnableCamera(nextCameraIndex);
        currentCameraIndex = nextCameraIndex;
        isSwitchingCamera = false;
    }

    void EnableCamera(int index)
    {
        cameras[index].gameObject.SetActive(true);
    }

    void DisableCamera(int index)
    {
        cameras[index].gameObject.SetActive(false);
    }
}
