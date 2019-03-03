using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    GameObject optionsWindow;

    bool menu;

    private void Start()
    {
        Time.timeScale = 1f;
        
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        

    }

    public void ShowWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Opened", true);
        //HUD.Instance.State = GameState.Pause;
    }

    public void HideWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool("Opened", false);
        //HUD.Instance.State = GameState.Play;
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
