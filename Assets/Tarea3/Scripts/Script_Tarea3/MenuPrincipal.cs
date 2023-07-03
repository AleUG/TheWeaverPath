using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public Text scoreTotalText;  // Referencia al componente de texto para mostrar el score total

    private void Start()
    {
        // Obtener el score total almacenado en PlayerPrefs, si existe
        int scoreTotal = PlayerPrefs.GetInt("ScoreTotal", 0);
        scoreTotalText.text = scoreTotal.ToString();  // Mostrar el score total en el componente de texto
    }
}
