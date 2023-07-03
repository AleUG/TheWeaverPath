using UnityEngine;
using UnityEngine.UI;

public class MenuFinal : MonoBehaviour
{
    public Text scoreText;  // Referencia al componente de texto para mostrar el score final obtenido por el jugador

    private void Start()
    {
        Coleccionable coleccionable = FindObjectOfType<Coleccionable>();  // Encontrar la instancia de Coleccionable en la escena
        int score = coleccionable.ObtenerScore();  // Obtener el score del script Coleccionable
        scoreText.text = score.ToString();  // Mostrar el score en el componente de texto

        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);  // Obtener el score máximo guardado en PlayerPrefs
        if (score > maxScore)
        {
            maxScore = score;  // Si el score obtenido es mayor, actualizar el score máximo
            PlayerPrefs.SetInt("MaxScore", maxScore);  // Guardar el nuevo score máximo en PlayerPrefs
            PlayerPrefs.Save();  // Guardar los cambios en PlayerPrefs
        }
    }
}
