using UnityEngine;
using UnityEngine.UI;

public class EscaladorDeParedYTecho : MonoBehaviour
{
    public float distanciaMaxima = 1f;
    public float velocidadEscalada = 5f;
    public float fuerzaSalto = 5f;
    public LayerMask capaParedes;
    public LayerMask capaTechos;

    private bool puedeEscalarPared = false;
    private bool escalandoPared = false;
    private bool puedeTreparTecho = false;
    private bool clickDerechoPresionado = false;
    private Rigidbody2D rb;
    private Quaternion rotacionInicial;

    private SpriteRenderer spriteRenderer;

    public float duracionTotal = 50f;
    public Image barraDeTiempo;

    private ArañaMecánica arañaMecánica;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rotacionInicial = transform.rotation;
        barraDeTiempo = GameObject.Find("TimeBar").GetComponent<Image>();
        arañaMecánica = GetComponent<ArañaMecánica>();
    }

    private void Update()
    {
        clickDerechoPresionado = Input.GetMouseButton(1);

        float fillAmount = arañaMecánica.CurrentTime() / duracionTotal;
        barraDeTiempo.fillAmount = fillAmount;
    }

    private void FixedUpdate()
    {
        if (puedeEscalarPared && clickDerechoPresionado)
        {
            if (arañaMecánica.CurrentTime() > 0f)
            {
                EscalarPared();

                if (!escalandoPared)
                {
                    arañaMecánica.enabled = false;
                }
            }
            else
            {
                DetenerEscalada();
            }
        }
        else if (puedeTreparTecho && clickDerechoPresionado)
        {
            if (arañaMecánica.CurrentTime() > 0f)
            {
                TreparTecho();

                if (!escalandoPared)
                {
                    arañaMecánica.enabled = false;
                }
            }
            else
            {
                DetenerEscalada();
            }
        }
        else
        {
            DetenerEscalada();
            puedeTreparTecho = false;

            if (!arañaMecánica.enabled && !clickDerechoPresionado)
            {
                // Activar el otro script cuando deja de escalar
                arañaMecánica.enabled = true;
            }
        }
    }

    private void EscalarPared()
    {
        if (transform.localScale == new Vector3(-1f, 1f, 1f))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        arañaMecánica.ChangeTime(-Time.deltaTime);

        // Congelar el eje Y del Rigidbody
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        if ((puedeEscalarPared && Input.GetMouseButton(0)) || escalandoPared)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }

        float movimientoVertical = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(0f, movimientoVertical * velocidadEscalada * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movimiento);

        if (movimientoVertical > 0f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            spriteRenderer.flipX = false;
        }
        else if (movimientoVertical < 0f)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            spriteRenderer.flipX = true;
        }

        if (arañaMecánica.CurrentTime() <= 0f)
        {
            DetenerEscalada();
            return;
        }

        if (escalandoPared && Input.GetButtonDown("Jump"))
        {
            escalandoPared = false;
            // Saltar desde la pared
            rb.AddForce(new Vector2((spriteRenderer.flipX ? -1f : 1f) * fuerzaSalto, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

    private void TreparTecho()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        spriteRenderer.flipX = true;
        arañaMecánica.ChangeTime(-Time.deltaTime);

        // Congelar el eje Y del Rigidbody
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        float movimientoHorizontal = Input.GetAxis("Horizontal");

        if (movimientoHorizontal > 0f)
        {
            // Descongelar el eje X del Rigidbody
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            transform.rotation = Quaternion.Euler(-1f, 0f, 180f);
            spriteRenderer.flipX = true;
        }
        else if (movimientoHorizontal < 0f)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            transform.rotation = Quaternion.Euler(1f, 0f, 180f);
        }
        else
        {
            // Congelar el eje X del Rigidbody
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }

        if (arañaMecánica.CurrentTime() <= 0f)
        {
            DetenerEscalada();
            return;
        }

        Vector2 movimiento = new Vector2(movimientoHorizontal * velocidadEscalada * Time.fixedDeltaTime, 0f);
        rb.MovePosition(rb.position + movimiento);
    }

    private void DetenerEscalada()
    {
        transform.rotation = rotacionInicial;
        // Descongelar los ejes del Rigidbody
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pared"))
        {
            float distancia = Mathf.Abs(collision.transform.position.x - transform.position.x);
            if (distancia <= distanciaMaxima)
            {
                puedeEscalarPared = true;
                puedeTreparTecho = false; // Desactivar la capacidad de trepar techos al entrar en contacto con una pared
            }
        }
        else if (collision.CompareTag("Techo"))
        {
            float distancia = Mathf.Abs(collision.transform.position.y - transform.position.y);
            if (distancia <= distanciaMaxima)
            {
                puedeTreparTecho = true;
                puedeEscalarPared = false; // Desactivar la capacidad de escalar paredes al entrar en contacto con un techo
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pared"))
        {
            float movimientoVertical = Input.GetAxis("Vertical");

            if (clickDerechoPresionado && movimientoVertical > 0f)
            {
                rb.velocity = new Vector2(0f, 0f);
                escalandoPared = true;
            }
            else if (!clickDerechoPresionado)
            {
                escalandoPared = false;
            }
        }
        else if (collision.CompareTag("Techo"))
        {
            float movimientoHorizontal = Input.GetAxis("Horizontal");

            if (clickDerechoPresionado && movimientoHorizontal != 0f)
            {
                rb.velocity = new Vector2(0f, 0f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pared"))
        {
            puedeEscalarPared = false;
            escalandoPared = false;
        }
        else if (collision.CompareTag("Techo"))
        {
            puedeTreparTecho = false;
        }
    }
}
