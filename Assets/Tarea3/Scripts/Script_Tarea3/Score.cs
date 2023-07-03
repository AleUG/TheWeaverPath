using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text puntajeFinalText;  // Referencia al componente de texto para mostrar el puntaje final
    public Text scoreFinalText;  // Referencia al componente de texto para mostrar el puntaje aparte final

    private void Start()
    {
        int scoreFinal = PlayerPrefs.GetInt("PuntajeAparte");  // Carga el puntaje aparte guardado utilizando PlayerPrefs
        scoreFinalText.text = scoreFinal.ToString();  // Muestra el puntaje aparte final en el componente de texto
    }
}
