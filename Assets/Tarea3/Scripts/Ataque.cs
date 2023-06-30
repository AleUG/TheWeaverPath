using UnityEngine;
using System.Collections;

public class Ataque : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator
    public int da�oAtaque = 20; // Cantidad de da�o infligido al atacar
    public float tiempoEntreAtaques = 0.5f; // Tiempo m�nimo entre cada ataque

    private bool puedeAtacar = true; // Indica si el jugador puede atacar
    private float tiempoUltimoAtaque; // Tiempo del �ltimo ataque

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && puedeAtacar)
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        animator.SetTrigger("Ataque"); // Activar la animaci�n de ataque
        puedeAtacar = false; // Desactivar la capacidad de atacar hasta que la animaci�n se complete
        tiempoUltimoAtaque = Time.time; // Registrar el tiempo del �ltimo ataque

        // Iniciar una corrutina para habilitar la capacidad de ataque despu�s de un tiempo
        StartCoroutine(HabilitarAtaque());

        // Obtener todos los enemigos dentro de un radio de 2 unidades
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (Collider2D enemigo in enemigos)
        {
            // Comprobar si el objeto enemigo tiene un componente de vida
            EnemyVida vidaEnemigo = enemigo.GetComponent<EnemyVida>();
            if (vidaEnemigo != null)
            {
                vidaEnemigo.RecibirDa�o(da�oAtaque); // Infligir da�o al enemigo
            }
        }
    }

    private IEnumerator HabilitarAtaque()
    {
        // Esperar hasta que la animaci�n de ataque est� completa
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Esperar el tiempo m�nimo entre ataques
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // Verificar si ha pasado suficiente tiempo desde el �ltimo ataque
        if (Time.time >= tiempoUltimoAtaque + tiempoEntreAtaques)
        {
            puedeAtacar = true; // Habilitar la capacidad de atacar nuevamente
        }
    }
}