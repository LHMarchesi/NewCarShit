using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private Slider progessionSlider;
    private float currentTime;
    private bool isLevelEnded;
    void Start()
    {
        StartCoroutine(StartLevel());
    }

   private IEnumerator StartLevel()
    {
        currentTime = 0;
        progessionSlider.maxValue = timer;

        while (!isLevelEnded && currentTime < timer)
        {
            currentTime += Time.deltaTime;
            float remainingTime = timer - currentTime;

            UpdateSliderUI(remainingTime);

            yield return null;
        }
        EndLevel();
    }

    private void UpdateSliderUI(float remainingTime)
    {
        progessionSlider.value = currentTime;
    }

    private void EndLevel()
    {
        isLevelEnded = true;
        GameManager.Instance.SetGameState(GameManager.GameStates.Win);
    }
}
