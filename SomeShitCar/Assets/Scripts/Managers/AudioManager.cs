using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    { get { return instance; } }

    public float masterVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;

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

        masterVolume = PlayerPrefs.GetFloat("MasterVolume", .7f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", .7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", .7f);

        // Set volume from PlayerPrefs
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    public void PlaySfx(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        masterBus.setVolume(masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicBus.setVolume(musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxBus.setVolume(sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
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
