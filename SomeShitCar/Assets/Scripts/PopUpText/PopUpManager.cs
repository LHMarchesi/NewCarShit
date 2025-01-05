using System.Collections;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private PopUpText popUpText;  // Referencia al PopUpText
    [SerializeField] private PopUpData[] popUpMessages; // Lista de mensajes emergentes

    public void StartPopUps(float levelTime)
    {
        StartCoroutine(ShowPopUps(levelTime));
    }

    private IEnumerator ShowPopUps(float levelTime)
    {
        int popUpIndex = 0;
        float currentTime = 0;

        // Recorre los mensajes emergentes basándose en el temporizador del nivel
        while (currentTime < levelTime)
        {
            currentTime += Time.deltaTime;

            // Muestra el pop-up cuando llega al tiempo específico
            if (popUpIndex < popUpMessages.Length && currentTime >= popUpMessages[popUpIndex].timeToAppear)
            {
                PopUpData popUp = popUpMessages[popUpIndex];
                StartCoroutine(popUpText.PopUp(popUp.message, popUp.duration, popUp.position));
                popUpIndex++;  // Avanza al siguiente pop-up
            }

            yield return null;
        }
    }
}
