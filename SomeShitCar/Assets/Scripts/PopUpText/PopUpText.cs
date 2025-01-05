using System.Collections;
using UnityEngine;
using TMPro;
public class PopUpText : MonoBehaviour
{
    public enum Positions
    {
        Top,Down
    }

    [SerializeField] private TextMeshProUGUI popUpText;

    public IEnumerator PopUp(string message, float duration, Positions position)
    {
        popUpText.text = message;           // Establece el texto
        SetTextPosition(position);
        popUpText.gameObject.SetActive(true); // Muestra el texto

        yield return new WaitForSeconds(duration); // Espera el tiempo especificado

        popUpText.gameObject.SetActive(false);  // Oculta el texto
    }

    private void SetTextPosition(Positions position)
    {
        // Ajusta la posición del texto en la pantalla dependiendo de la opción seleccionada
        RectTransform rectTransform = GetComponent<RectTransform>();

        switch (position)
        {
            case Positions.Top:
                rectTransform.anchorMin = new Vector2(0.5f, 1f);
                rectTransform.anchorMax = new Vector2(0.5f, 1f);
                rectTransform.anchoredPosition = new Vector2(0, -700); // Ajusta la posición Y según necesites
                break;

            case Positions.Down:
                rectTransform.anchorMin = new Vector2(0.5f, 0f);
                rectTransform.anchorMax = new Vector2(0.5f, 0f);
                rectTransform.anchoredPosition = new Vector2(0, 1000); // Ajusta la posición Y según necesites
                break;
        }
    }
}
