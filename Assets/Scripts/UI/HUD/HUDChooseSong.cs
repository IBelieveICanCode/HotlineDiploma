using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDChooseSong : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonsLayout;
    [SerializeField]
    private Button _songButton;
    public GameObject ChooseSongScreen;
    [SerializeField]
    private Text _songOnCasetteName;


    private void OnEnable()
    {
        CreateButtonsForSongs();
        GameController.Instance.State = GameState.Pause;
    }


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
        ChooseSongScreen.SetActive(false);
        GameController.Instance.State = GameState.Play;
    }

    private void SongNameCasette(string song)
    {
        _songOnCasetteName.text = song;
    }
}
