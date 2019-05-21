using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScoresScript : MonoBehaviour
{
    [SerializeField]
    private Text _scoreLabel;
    public Text CountFinalResult;
    [SerializeField]
    private Text _highScoreCount;

    private static float score;
    public static float Score { get => score; set => score = value; }

    public void SetPoints()
    {
        _scoreLabel.text = score.ToString();
    }

    public void SetHighScore()
    {

        if (PlayerPrefs.GetInt("PlayerBestScore") < int.Parse(_scoreLabel.text))
        {
            PlayerPrefs.SetInt("PlayerBestScore", int.Parse(_scoreLabel.text));
            _highScoreCount.text = "Best Score: " + PlayerPrefs.GetInt("PlayerBestScore").ToString();
        }
    }

    public void ShowHighScore()
    {

        _highScoreCount.text = "Best Score: " + PlayerPrefs.GetInt("PlayerBestScore").ToString();
    }

}
