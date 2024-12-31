using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelConfig levelConfig;

    private float currentTime;
    private bool isLevelEnded;

    void Start()
    {
        ApplyLevelConfig();

        if (levelConfig.startAnimation != null)
        {
            PlayStartAnimationAndBegin();
        }
        else
        {
            StartCoroutine(StartLevel());
        }
    }

    private IEnumerator PlayStartAnimationAndBegin()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(levelConfig.startAnimation.name);
        yield return new WaitForSeconds(levelConfig.startAnimation.length);

        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        currentTime = 0;
        UIManager.Instance.SetSliderMaxValue(levelConfig.timer);

        while (!isLevelEnded && currentTime < levelConfig.timer)
        {
            currentTime += Time.deltaTime;
            float remainingTime = levelConfig.timer - currentTime;

            UIManager.Instance.UpdateSliderUI(currentTime);

            yield return null;
        }
        EndLevel();
    }

    private void ApplyLevelConfig()
    {
        DifficultyManager.Instance.AdjustDifficulty(levelConfig.enemySpawnMultiplier, 
                                                        levelConfig.obstacleSpawnMultiplier,
                                                             levelConfig.obstacleSpeedMultiplier, 
                                                                levelConfig.playerSpeedMultiplier
            );
    }

    private void EndLevel()
    {
        isLevelEnded = true;
        GameManager.Instance.SetGameState(GameManager.GameStates.Win);
    }
}
