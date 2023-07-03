using UnityEngine;

public class ColeccionableManager : MonoBehaviour
{
    public static ColeccionableManager Instance;  // Instancia singleton del ColeccionableManager

    private int puntos = 0;  // Contador de puntos global

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementarPuntos(int cantidad)
    {
        puntos += cantidad;
    }

    public int ObtenerPuntos()
    {
        return puntos;
    }
}
