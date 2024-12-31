using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Slider progessionSlider;
    [SerializeField] private RawImage panel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateSliderUI(float remainingTime)
    {
        progessionSlider.value = remainingTime;
    }

    public void SetSliderMaxValue(float maxValue)
    {
        progessionSlider.maxValue = maxValue;
    }

    public IEnumerator StartPanelEffect(Color color)
    {
        color.a = 0.4f;
        panel.color = color;
        panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(.25f);
        panel.gameObject.SetActive(false);
    }
}
