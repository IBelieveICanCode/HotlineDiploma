using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioController : MonoBehaviour
{
    public List<AudioClip> ListOfSongs = new List<AudioClip>();

    public AudioSource SFX;
    public AudioSource Music;
    public float SfxStartVolume = 1;
    public float MusicStartVolume = 1;
    public static MenuAudioController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);
        SFX.volume = SfxStartVolume;
        Music.volume = SfxStartVolume;
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

    public void PlayMusic(string songName)
    {
        string path = "Audio/Music/";
        Music.clip = (Resources.Load<AudioClip>(path + songName));
        Music.Play();
    }
}
