using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    
    
    [Header("Sound Sliders")]
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            LoadVolume();
        }
        else
        {
            ChangeMainVolume();
            ChangeMusicVolume();
            ChangeSFXVolume();
        }
    }

    public void ChangeMainVolume()
    {
        audioMixer.SetFloat("MasterVol", MasterSlider.value);
        PlayerPrefs.SetFloat("MasterVol", MasterSlider.value);
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVol", MusicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", MusicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVol", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVol", SFXSlider.value);
    }

    private void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        
        ChangeMainVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }
}
