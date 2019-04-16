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
            otherHealth.GetComponent<IDestructable>().CurrentHealth = otherHealth.GetComponent<IDestructable>().MaxHealth;
            HUD.Instance.HealthBar.value = otherHealth.GetComponent<IDestructable>().MaxHealth;
            Destroy(gameObject);
        }
    }

}
