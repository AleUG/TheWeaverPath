using UnityEngine;

public class npcCharacter : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
            }
        }
    }
}