using UnityEngine;
using UnityEngine.UI;

public class BarraDeTiempoCompartida : MonoBehaviour
{
    public float tiempoActual = 5f;
    public float duracionTotal = 5f;
    public Image barraDeTiempo;

    private void Start()
    {
        barraDeTiempo = GetComponent<Image>();
    }

    private void Update()
    {
        float fillAmount = tiempoActual / duracionTotal;
        barraDeTiempo.fillAmount = fillAmount;
    }
}
