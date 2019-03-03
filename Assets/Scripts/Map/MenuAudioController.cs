using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource SFX;
    [SerializeField]
    private AudioSource Music;
    [SerializeField]
    private Slider soundSlider;

    [SerializeField]
    private Slider musicSlider;

    public static MenuAudioController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);       
    }
    private void Start()
    {
        soundSlider.value = FindObjectOfType<Settings>().SFX;
        musicSlider.value = FindObjectOfType<Settings>().Music;
        SFX.volume = soundSlider.value;
        Music.volume = musicSlider.value;

    }

    public void SetSoundVolume(Slider slider)
    {
        FindObjectOfType<Settings>().SFX = slider.value;
        SFX.volume = soundSlider.value;
    }
    public void SetMusicVolume(Slider slider)
    {
        FindObjectOfType<Settings>().Music = slider.value;
        Music.volume = musicSlider.value;
    }

    public void PlaySound(string clipName,bool randomPitch)
    {
        string path = "Audio/Sounds/";
        if (randomPitch)
            SFX.pitch = Random.Range(0.5f, 1.3f);
        else
            SFX.pitch = 1f;
        
        SFX.PlayOneShot(Resources.Load<AudioClip>(path + clipName));
    }

    public void StopMusic()
    {
        Music.Stop();
           
    }
}
