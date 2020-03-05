using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float startHealth = 100;//health duh 
    [SerializeField] private float health = 100;//health duh 


    public Image healthBar;
    public void Start()
    {
        health = startHealth;
    }
    // Start is called before the first frame update
    public void ApplyDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
