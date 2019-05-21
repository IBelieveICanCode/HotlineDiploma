using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHUDControl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> HUDElementList = new List<GameObject>();
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject playerGameHUD;

    private void Awake()
    {
        HideAllWindows();

    }
    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.Instance.State == GameState.Play)
            {
                
                GameController.Instance.State = GameState.Pause;
                mainMenu.SetActive(true);
                playerGameHUD.SetActive(false);
            }
            else
            {
                HideAllWindows();
            }
        }
    }

    private void Update()
    {
        OpenMenu();
    }

    public void HideAllWindows()
    {
        foreach (var HUDelement in HUDElementList)
        {
            HUDelement.SetActive(false);
        }
        GameController.Instance.State = GameState.Play;
        playerGameHUD.SetActive(true);
    }
}
