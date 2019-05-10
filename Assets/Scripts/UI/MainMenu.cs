using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Slider _soundSlider;
    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    GameObject optionsWindow;

    bool menu;

    private void Start()
    {
        Time.timeScale = 1f;
        SlidersSetup();
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        

    }

    public void ShowWindow(GameObject window)
    {
        //window.GetComponent<Animator>().SetBool("Opened", true);
        window.SetActive(true);
        //HUD.Instance.State = GameState.Pause;
    }

    public void HideWindow(GameObject window)
    {
        //window.GetComponent<Animator>().SetBool("Opened", false);
        window.SetActive(false);
        //HUD.Instance.State = GameState.Play;
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


    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
    }
    

    public void ExitButton()
    {
        Application.Quit();
    }
}
