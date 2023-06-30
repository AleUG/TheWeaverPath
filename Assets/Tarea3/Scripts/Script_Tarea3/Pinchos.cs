using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
    public int recibirDa�o;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprueba si la flecha ha colisionado con el jugador
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerVida>().RecibirDa�o(recibirDa�o); // Inflije da�o al jugador
        }
    }
}
