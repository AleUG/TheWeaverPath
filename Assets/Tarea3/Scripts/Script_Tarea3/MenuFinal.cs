using UnityEngine;
using UnityEngine.UI;

public class MenuFinal : MonoBehaviour
{
    public Text scoreActualText;  // Referencia al componente de texto para mostrar el score actual

    private void Start()
    {
        int scoreActual = Coleccionable.puntos;  // Obtener el score actual de la partida desde Coleccionable
        scoreActualText.text = scoreActual.ToString();  // Mostrar el score actual en el componente de texto

        // Guardar el score actual como score total si es mayor al score almacenado previamente
        int scoreTotal = PlayerPrefs.GetInt("ScoreTotal", 0);
        if (scoreActual > scoreTotal)
        {
            PlayerPrefs.SetInt("ScoreTotal", scoreActual);
            PlayerPrefs.Save();
        }
    }
}
