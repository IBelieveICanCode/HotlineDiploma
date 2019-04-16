using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreHUD : MonoBehaviour
{
    public Text CountFinalResult;
    public Text HighScoreCount;

    public void SetHighScore()
    {

        if (PlayerPrefs.GetInt("PlayerBestScore") < int.Parse(HUD.Instance._scoreLabel.text))
        {
            PlayerPrefs.SetInt("PlayerBestScore", int.Parse(HUD.Instance._scoreLabel.text));
            HighScoreCount.text = "Best Score:" + PlayerPrefs.GetInt("PlayerBestScore").ToString();
        }
    }

    public void ShowHighScore()
    {
        HighScoreCount.text = "Best Score:" + PlayerPrefs.GetInt("PlayerBestScore").ToString();
    }
}
