using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public Text maxScoreText;  // Referencia al componente de texto para mostrar el score m�ximo

    private int scoreActual;  // Puntaje actual del jugador

    private void Start()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);  // Obtener el score m�ximo guardado en PlayerPrefs
        maxScoreText.text = maxScore.ToString();  // Mostrar el score m�ximo en el componente de texto

        GameObject coleccionable = GameObject.Find("Coleccionable");  // Obtener el objeto Coleccionable en la escena

        if (coleccionable != null)
        {
            Coleccionable coleccionableScript = coleccionable.GetComponent<Coleccionable>();  // Obtener el componente Coleccionable

            if (coleccionableScript != null)
            {
                scoreActual = coleccionableScript.ObtenerScore();  // Obtener el puntaje actual del jugador

                if (scoreActual > maxScore)
                {
                    maxScore = scoreActual;  // Actualizar el score m�ximo si el puntaje actual es mayor
                    PlayerPrefs.SetInt("MaxScore", maxScore);  // Guardar el nuevo score m�ximo en PlayerPrefs
                    maxScoreText.text = maxScore.ToString();  // Mostrar el nuevo score m�ximo en el componente de texto
                }
            }
        }
    }
}
