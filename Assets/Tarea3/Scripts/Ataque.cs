using UnityEngine;
using System.Collections;

public class Ataque : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator
    public int dañoAtaque = 20; // Cantidad de daño infligido al atacar
    public float tiempoEntreAtaques = 0.5f; // Tiempo mínimo entre cada ataque

    private bool puedeAtacar = true; // Indica si el jugador puede atacar
    private float tiempoUltimoAtaque; // Tiempo del último ataque

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
        animator.SetTrigger("Ataque"); // Activar la animación de ataque
        puedeAtacar = false; // Desactivar la capacidad de atacar hasta que la animación se complete
        tiempoUltimoAtaque = Time.time; // Registrar el tiempo del último ataque

        // Iniciar una corrutina para habilitar la capacidad de ataque después de un tiempo
        StartCoroutine(HabilitarAtaque());

        // Obtener todos los enemigos dentro de un radio de 2 unidades
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, 2f);

        foreach (Collider2D enemigo in enemigos)
        {
            // Comprobar si el objeto enemigo tiene un componente de vida
            EnemyVida vidaEnemigo = enemigo.GetComponent<EnemyVida>();
            if (vidaEnemigo != null)
            {
                vidaEnemigo.RecibirDaño(dañoAtaque); // Infligir daño al enemigo
            }
        }
    }

    private IEnumerator HabilitarAtaque()
    {
        // Esperar hasta que la animación de ataque esté completa
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Esperar el tiempo mínimo entre ataques
        yield return new WaitForSeconds(tiempoEntreAtaques);

        // Verificar si ha pasado suficiente tiempo desde el último ataque
        if (Time.time >= tiempoUltimoAtaque + tiempoEntreAtaques)
        {
            puedeAtacar = true; // Habilitar la capacidad de atacar nuevamente
        }
    }
}