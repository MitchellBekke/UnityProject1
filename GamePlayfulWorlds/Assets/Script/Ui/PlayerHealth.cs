using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHealthSlider healthBar;
    void Start()
    {
        healthBar = GameObject.Find("AmmoUI/MiniMap").GetComponentInChildren<PlayerHealthSlider>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       /*if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyDamage(20);
        }*/
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
