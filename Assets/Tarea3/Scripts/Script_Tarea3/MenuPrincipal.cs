using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public Text maxScoreText;  // Referencia al componente de texto para mostrar el score máximo

    private int scoreActual;  // Puntaje actual del jugador

    private void Start()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);  // Obtener el score máximo guardado en PlayerPrefs
        maxScoreText.text = maxScore.ToString();  // Mostrar el score máximo en el componente de texto

        GameObject coleccionable = GameObject.Find("Coleccionable");  // Obtener el objeto Coleccionable en la escena

        if (coleccionable != null)
        {
            PuntosManager puntosManager = coleccionable.GetComponent<PuntosManager>();  // Obtener el componente Coleccionable

            if (puntosManager != null)
            {
                scoreActual = puntosManager.ObtenerScore();  // Obtener el puntaje actual del jugador

                if (scoreActual > maxScore)
                {
                    maxScore = scoreActual;  // Actualizar el score máximo si el puntaje actual es mayor
                    PlayerPrefs.SetInt("MaxScore", maxScore);  // Guardar el nuevo score máximo en PlayerPrefs
                    maxScoreText.text = maxScore.ToString();  // Mostrar el nuevo score máximo en el componente de texto
                }
            }
        }
    }
}
