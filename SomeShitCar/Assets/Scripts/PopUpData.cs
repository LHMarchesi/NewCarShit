using UnityEngine;

[System.Serializable]
public class PopUpData : MonoBehaviour
{
    public float timeToAppear;// El tiempo en el que debe aparecer el mensaje

    [TextArea(3, 10)]
    public string message;      // El mensaje a mostrar

    public float duration;      // La duraci�n del mensaje
    public PopUpText.Positions position;  // La posici�n del mensaje
}
