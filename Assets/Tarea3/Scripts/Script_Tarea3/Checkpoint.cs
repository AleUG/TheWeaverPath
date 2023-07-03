using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public Button respawnButton;
    private Vector3 respawnPoint;
    private bool hasCheckpoint;

    private void Start()
    {
        respawnButton.onClick.AddListener(Respawn);
        hasCheckpoint = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("CheckpointJe");
            respawnPoint = other.transform.position;
            hasCheckpoint = true;
        }
    }

    private void Respawn()
    {
        if (hasCheckpoint)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = respawnPoint;
            Debug.Log("Player respawned at checkpoint: " + respawnPoint);
        }
        else
        {
            Debug.Log("No checkpoint set. Player respawns at default position.");
            // Aquí puedes definir una posición de respawn por defecto si no hay ningún checkpoint establecido.
        }
    }
}
