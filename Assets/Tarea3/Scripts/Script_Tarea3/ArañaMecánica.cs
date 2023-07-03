using UnityEngine;
using UnityEngine.UI;

public class ArañaMecánica : MonoBehaviour
{
    public LineRendererScript lrs;
    public LineRenderer lr;
    public float maxDistance = 2f; // Distancia máxima para colgarse
    public LayerMask ceilingLayer; // Capa del techo
    public Transform hangPoint; // Punto de referencia para colgarse

    private float tiempoActual = 5f; // Tiempo actual restante
    public float duracionTotal = 5f; // Duración total del tiempo
    public Image barraDeTiempo; // Barra de tiempo para mostrar visualmente

    private bool isHanging = false;
    private bool isFalling = false;
    private bool isScriptActive = true; // Estado actual del script ArañaMecánica

    private Rigidbody2D rb;
    private DistanceJoint2D distanceJoint;
    private Quaternion initialRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isHanging)
            {
                TryHang();
            }
            else
            {
                ReduceTime();
            }
        }
        else if (isHanging)
        {
            StopHang();
        }
        else
        {
            RecoverTime();
        }

        // Actualizar la barra de tiempo
        UpdateTimeBar();
    }

    private void FixedUpdate()
    {
        if (isHanging && isFalling)
        {
            rb.gravityScale = 5f;
        }
        else
        {
            rb.gravityScale = 3.5f;
        }
    }

    private void TryHang()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, ceilingLayer);

        if (hit.collider != null && Vector2.Distance(transform.position, hit.point) <= maxDistance)
        {
            HangOnCeiling(hit.point);
            lrs.puntoB = hit.transform;
            lr.enabled = true;
        }
    }

    private void HangOnCeiling(Vector2 hangPosition)
    {
        isHanging = true;
        isFalling = false;
        rb.velocity = Vector2.zero;

        distanceJoint.enabled = true;
        distanceJoint.connectedAnchor = hangPosition;

        Vector3 direction = hangPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void StopHang()
    {
        isHanging = false;
        distanceJoint.enabled = false;
        lr.enabled = false;
        transform.rotation = initialRotation;

        if (isScriptActive)
        {
            isScriptActive = false;
            Invoke("ActivateScript", 2f); // Tiempo después de desactivar el script para reactivarlo (3 segundos en este ejemplo)
        }
    }

    private void ActivateScript()
    {
        isScriptActive = true;
        // Lógica adicional para activar el script ArañaMecánica
    }

    private void ReduceTime()
    {
        tiempoActual -= Time.deltaTime;
        if (tiempoActual <= 0f)
        {
            StopHang();
        }
    }

    private void RecoverTime()
    {
        tiempoActual += Time.deltaTime;
        tiempoActual = Mathf.Clamp(tiempoActual, 0f, duracionTotal);
    }

    private void UpdateTimeBar()
    {
        float fillAmount = tiempoActual / duracionTotal;
        barraDeTiempo.fillAmount = fillAmount;
    }

    public void ChangeTime(float time)
    {
        tiempoActual += time;
    }

    public float CurrentTime()
    {
        return tiempoActual;
    }
}
