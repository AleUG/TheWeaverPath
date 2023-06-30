using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public KeyCode checkpointKey = KeyCode.C;

    private Transform playerTransform;
    private Vector3 lastCheckpointPosition;

    private void Start()
    {
        playerTransform = transform;
        lastCheckpointPosition = playerTransform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(checkpointKey))
        {
            SetCheckpointPosition(playerTransform.position);
        }
    }

    public void SetCheckpointPosition(Vector3 position)
    {
        lastCheckpointPosition = position;
        Debug.Log("Checkpoint set at: " + lastCheckpointPosition);
    }

    public void RespawnAtLastCheckpoint()
    {
        playerTransform.position = lastCheckpointPosition;
    }
}