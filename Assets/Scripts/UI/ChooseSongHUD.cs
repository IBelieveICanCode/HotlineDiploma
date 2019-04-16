using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSongHUD : MonoBehaviour
{
    [Header("List of songs")]
    [SerializeField]
    private GameObject _buttonsLayout;
    [SerializeField]
    private Button _songButton;
    public GameObject ChooseSongScreen;
    [SerializeField]
    private Text _songName;


    public void CreateButtonsForSongs()
    {
        for (int i = 0; i < MenuAudioController.Instance.ListOfSongs.Count; i++)
        {
            Button button = Instantiate(_songButton);
            button.transform.parent = _buttonsLayout.transform;
            button.GetComponentInChildren<Text>().text = MenuAudioController.Instance.ListOfSongs[i].name;
            button.onClick.AddListener(() => SongChosen(button.GetComponentInChildren<Text>().text));

        }
    }

    private void SongChosen(string text)
    {
        SongNameCasette(text);
        MenuAudioController.Instance.PlayMusic(text);
        HUD.Instance.HideWindow(ChooseSongScreen);
    }

    private void SongNameCasette(string song)
    {
        _songName.text = song;
    }
}
