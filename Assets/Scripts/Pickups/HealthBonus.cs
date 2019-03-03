using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        PlayerControl otherHealth = other.gameObject.GetComponent<PlayerControl>();
        if (otherHealth != null)
        {
            MenuAudioController.Instance.PlaySound("healthup", false);
            otherHealth.GetComponent<Destructable>().hitPointsCurrent = otherHealth.GetComponent<Destructable>().hitPoints;
            HUD.Instance.HealthBar.value = otherHealth.GetComponent<Destructable>().hitPointsCurrent;
            Destroy(gameObject);
        }
    }

    public static void Create(Vector3 position)
    {
        Instantiate(Resources.Load("Prefabs/HealthPivot"), position, Quaternion.identity);
    }
}
