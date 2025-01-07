using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    { get { return instance; } }

    public float MasterVolume { get => masterVolume; set => masterVolume = value; }
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public float SfxVolume { get => sfxVolume; set => sfxVolume = value; }

    private float masterVolume;
    private float musicVolume;
    private float sfxVolume;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // Initialize Fmod buses
        masterBus = RuntimeManager.GetBus("Bus:/");
        musicBus = RuntimeManager.GetBus("Bus:/Music");
        sfxBus = RuntimeManager.GetBus("Bus:/SFX");

        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", .7f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", .7f);
        SfxVolume = PlayerPrefs.GetFloat("SFXVolume", .7f);

        // Set volume from PlayerPrefs
        SetMasterVolume(MasterVolume);
        SetMusicVolume(MusicVolume);
        SetSFXVolume(SfxVolume);
    }

    public void PlaySfx(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void SetMasterVolume(float volume)
    {
        MasterVolume = Mathf.Clamp01(volume);
        masterBus.setVolume(MasterVolume);
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = Mathf.Clamp01(volume);
        musicBus.setVolume(MusicVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        SfxVolume = Mathf.Clamp01(volume);
        sfxBus.setVolume(SfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", SfxVolume);
    }

    public void PauseAllAudio(bool pause)
    {
        masterBus.setPaused(pause);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
