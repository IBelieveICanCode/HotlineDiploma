using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHUDControl : MonoBehaviour
{
    private void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HUD.Instance.ShowWindow(GameObject.FindGameObjectWithTag("MainMenu"));
        }
    }

    private void Update()
    {
        OpenMenu();
    }
}
