using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    [SerializeField]
    private IDestructable owner;

    private bool rotateBar = true;

	void Start () {
        owner = transform.parent.parent.GetComponent<IDestructable>();
        healthBar = gameObject.GetComponent<Image>();
            rotateBar = false;
        
	}
	
	void Update ()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0.0f, owner.MaxHealth, owner.CurrentHealth);
    
        if (rotateBar)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
