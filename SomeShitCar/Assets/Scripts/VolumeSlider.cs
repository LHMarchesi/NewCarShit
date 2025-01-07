using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        MUSIC,
        ENGINE,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();

        if (volumeSlider == null)
        {
            Debug.LogError("Slider component not found on " + gameObject.name);
            return;
        }
    }

    private void Start()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.Instance.MasterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.Instance.MusicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.Instance.SfxVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;

        }
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnSliderValueChanged(float value)
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.Instance.SetMasterVolume(value);
                break;
            case VolumeType.MUSIC:
                AudioManager.Instance.SetMusicVolume(value);
                break;
            case VolumeType.SFX:
                AudioManager.Instance.SetSFXVolume(value);
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }
}
