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

        masterBus = RuntimeManager.GetBus("Bus:/");
      //  musicBus = RuntimeManager.GetBus("Bus:/Music");
       // sfxBus = RuntimeManager.GetBus("Bus:/SFX");

        
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
    }

    private void Update()
    {
        //masterBus.setVolume(masterVolume);
    }

    public void PlaySfx(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

}
