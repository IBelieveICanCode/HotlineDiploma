using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    [SerializeField]
    private Destructable owner;

    private bool rotateBar = true;

	// Use this for initialization
	void Start () {
        healthBar = gameObject.GetComponent<Image>();
            rotateBar = false;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0.0f, owner.hitPoints, owner.hitPointsCurrent);
    
        if (rotateBar)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
