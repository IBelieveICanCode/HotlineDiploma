using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum GameState { Play, Pause };
public class HUD : MonoBehaviour
{
    public Image FadePlane;
    public Image TypeWeapon;
    [SerializeField]
    private Text _waveCount;
    [SerializeField]
    private Text _scoreLabel;
    [SerializeField]
    private Text _countFinalResult;
    [SerializeField]
    private Text _highScoreCount;
    [SerializeField]
    private Text _ammoCount;
    [SerializeField]
    private Text _songName;

    private static float score;
    private static float currentWave;
    private static float ammo;

    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    IDestructable playerHealth;
    [SerializeField]
    ILogicDeathDependable playerDeath;
    public GameObject GameOverScreen;

    private GameState state;
    static private HUD _instance;


    public GameState State
    {
        get
        {
            return state;
        }

        set
        {
            if (value == GameState.Play)
            {
                Time.timeScale = 1.0f;
            }
            else if (value == GameState.Pause)
            {
                Time.timeScale = 0.0f;
            }
            state = value;
        }
    }
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

    public static float Score { get => score; set => score = value; }
    public static float CurrentWave { get => currentWave; set => currentWave = value; }
    public static float Ammo { get => ammo; set => ammo = value; }

    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        playerHealth = FindObjectOfType<PlayerParticleDestructable>().GetComponent<IDestructable>();
        playerDeath = FindObjectOfType<PlayerParticleDestructable>().GetComponent<ILogicDeathDependable>();
        playerDeath.DeathEvent += OnGameOver; 
        SetHealthValue();
        
    }

    void Update()
    {
        SetPoints();
        SetWaveCount();
        SetAmmoCount();
        ShowHighScore();
        SetHealthValue();
    }
    //TO DO: This metod is called when player take damage
    public void SetHealthValue()
    {      
        HealthBar.value = playerHealth.CurrentHealth;
    }

    public void SetPoints()
    {
        _scoreLabel.text = score.ToString();
    }


    public void SetWaveCount()
    {
        _waveCount.text = "Wave: " + currentWave;
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
        State = GameState.Play;
        Score = 0;
    }

    public void ShowWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Opened", true);
        //window.alpha = 1f;
        //window.blocksRaycasts = true;
        //window.interactable = true;
        State = GameState.Pause;
    }
    public void HideWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Opened", false);
        //window.alpha = 0f;
        //window.blocksRaycasts = false;
        //window.interactable = false;
        State = GameState.Play;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    private void OnGameOver()
    {
        MenuAudioController.Instance.StopMusic();
        StartCoroutine(Fade(Color.clear, Color.black, 2f));
        
        //gameOverScreen.SetActive(true);
    }
    public void SetHighScore()
    {
        
        if (PlayerPrefs.GetInt("PlayerBestScore") < int.Parse(_scoreLabel.text))
        {
            PlayerPrefs.SetInt("PlayerBestScore", int.Parse(_scoreLabel.text));
            _highScoreCount.text = "Best Score:" + PlayerPrefs.GetInt("PlayerBestScore").ToString();
        }
    }

    public void ShowHighScore()
    {
        
        _highScoreCount.text = "Best Score:" + PlayerPrefs.GetInt("PlayerBestScore").ToString();
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
        ShowWindow(GameOverScreen);

        _countFinalResult.text = "But you've earned " + score + " scores";
        SetHighScore();
    }


}
