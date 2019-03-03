using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnDestroy()
    {
        MenuAudioController.Instance.PlaySound("explosion", false);
    }
}
