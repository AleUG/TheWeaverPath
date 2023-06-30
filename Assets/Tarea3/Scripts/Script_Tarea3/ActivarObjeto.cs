using UnityEngine;
using System.Collections.Generic;

public class ActivarObjeto : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers; // Lista de SpriteRenderers
    public float fadeInDuration = 1f; // Duración de la transición de opacidad (fade-in)
    public float fadeOutDuration = 1f; // Duración de la transición de opacidad (fade-out)

    private bool enContacto;
    private float currentOpacity = 0f;
    private float fadeSpeed;

    private void Start()
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false; // Ocultar los SpriteRenderers al inicio
        }
        fadeSpeed = 1f / fadeInDuration;
    }

    private void Update()
    {
        if (enContacto)
        {
            currentOpacity += fadeSpeed * Time.deltaTime;
            currentOpacity = Mathf.Clamp01(currentOpacity);
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentOpacity);
            }
        }
        else
        {
            currentOpacity -= fadeSpeed * Time.deltaTime;
            currentOpacity = Mathf.Clamp01(currentOpacity);
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, currentOpacity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = true;
            currentOpacity = 0f; // Reiniciar la opacidad
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = true; // Mostrar los SpriteRenderers al entrar en contacto
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cambia "Player" por la etiqueta que desees
        {
            enContacto = false;
        }
    }
}
