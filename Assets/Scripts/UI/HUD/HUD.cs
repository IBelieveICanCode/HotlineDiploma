using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HUD : MonoBehaviour
{
    [Header("HUD Scripts")]
    [SerializeField]
    private HUDChooseSong HUDChooseSong;
    [SerializeField]
    private HUDScoresScript HUDScoresScript;
    [Space]
    [SerializeField]
    private Slider _soundSlider;
    [SerializeField]
    private Slider _musicSlider;
    public Image FadePlane;
    public Image TypeWeapon;
    [SerializeField]
    private Text _waveCount;
    [SerializeField]
    private Text _ammoCount;
    [SerializeField]
    private Text _songName;

    private static float currentWave;
    private static float ammo;

    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    IDestructable playerHealth;
    [SerializeField]
    ILogicDeathDependable playerDeath;
    public GameObject GameOverScreen;

    private static HUD _instance;
    public static HUD Instance
    {
        get
        {
            return _instance;
        }
    }

    public Slider HealthBar
    {
        get
        {
            return healthBar;
        }

        set
        {
            healthBar = value;
        }
    }

    public static float CurrentWave { get => currentWave; set => currentWave = value; }
    public static float Ammo { get => ammo; set => ammo = value; }

    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        SlidersSetup();
        playerHealth = FindObjectOfType<PlayerParticleDestructable>().GetComponent<IDestructable>();
        playerDeath = FindObjectOfType<PlayerParticleDestructable>().GetComponent<ILogicDeathDependable>();
        playerDeath.DeathEvent += OnGameOver; 
        SetHealthValue();
        HUDChooseSong.ChooseSongScreen.SetActive(true);
    }

    void SlidersSetup()
    {
        _soundSlider.onValueChanged.AddListener(
            delegate {
            MenuAudioController.Instance.SFX.volume = _soundSlider.value;
        });
        _musicSlider.onValueChanged.AddListener(
            delegate {
            MenuAudioController.Instance.Music.volume = _musicSlider.value;
        });
        _musicSlider.value = MenuAudioController.Instance.Music.volume;
        _soundSlider.value = MenuAudioController.Instance.SFX.volume;
    }

    void Update()
    {
        HUDScoresScript.SetPoints();
        SetWaveCount();
        SetAmmoCount();
        HUDScoresScript.ShowHighScore();
        SetHealthValue();
    }
    //TO DO: This metod is called when player take damage
    public void SetHealthValue()
    {      
        HealthBar.value = playerHealth.CurrentHealth;
    }

    public void SetWaveCount()
    {
        _waveCount.text = currentWave.ToString();
    }

    public void SetAmmoCount()
    {
        _ammoCount.text = ammo.ToString();
    }

    public void ChangeWeaponImage(Sprite weapon)
    {
        TypeWeapon.sprite = weapon;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        GameController.Instance.State = GameState.Play;
        HUDScoresScript.Score = 0;
    }

    public void Quit()
    {
        GameController.Instance.State = GameState.Play;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void OnGameOver()
    {
        //GameController.Instance.State = GameState.Pause;
        MenuAudioController.Instance.StopMusic();
        StartCoroutine(Fade(Color.clear, Color.black, 2f));
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            FadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
        //gameOverScreen.SetActive(true);
        MenuAudioController.Instance.PlaySound("youdied", false);

        //TODO: ADD EVENT
        //ShowWindow(GameOverScreen);
        GameOverScreen.SetActive(true);
        HUDScoresScript.CountFinalResult.text = "But you've earned " + HUDScoresScript.Score + " scores";
        HUDScoresScript.SetHighScore();
    }


}
