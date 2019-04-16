using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public enum GameState { Play, Pause };
public class HUD : MonoBehaviour
{
    public HighScoreHUD HighScoreScript;
    public ChooseSongHUD ChooseSongScript;
    public Image FadePlane;
    public Image TypeWeapon;
    [SerializeField]
    private Text _waveCount;
    public Text _scoreLabel;
    [SerializeField]
    private Text _ammoCount;
    

    private static float score;
    private static float currentWave;
    private static float ammo;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    Destructable owner;
    public GameObject GameOverScreen;
    public GameObject MainMenu;
    //public GameObject UpgradeScreen;

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
        ShowWindow(ChooseSongScript.ChooseSongScreen);
        owner.OnDeath += OnGameOver; 
        SetHealthValue();
        ChooseSongScript.CreateButtonsForSongs();
    }

    void Update()
    {
        SetEnemyReward();
        SetWaveCount();
        SetAmmoCount();
        HighScoreScript.ShowHighScore();
    }

    public void SetHealthValue()
    {      
        HealthBar.value = owner.hitPointsCurrent;
    }

    public void SetEnemyReward()
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
        //window.GetComponent<Animator>().SetBool("Opened", true);
        window.SetActive(true);
        //window.alpha = 1f;
        //window.blocksRaycasts = true;
        //window.interactable = true;
        State = GameState.Pause;
    }
    public void HideWindow(GameObject window)
    {
        window.SetActive(false);
        //window.GetComponent<Animator>().SetBool("Opened", false);
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
        HighScoreScript.CountFinalResult.text = "But you've earned " + score + " scores";
        HighScoreScript.SetHighScore();

    }

}
