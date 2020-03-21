using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int timer;
    public int timeLimit = 60;

    public PlayerHealthSlider healthBar;
    public GameObject backgroundMusic;
    void Start()
    {
        healthBar = GameObject.Find("AmmoUI/MiniMap").GetComponentInChildren<PlayerHealthSlider>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        backgroundMusic = GameObject.Find("BackgroundMusic");
    }
    void Update()
    {
        timer++;
        healthBar.SetHealth(currentHealth);
        if(currentHealth < maxHealth && timer >= timeLimit)
        {
            currentHealth++;
            timer = 0;
        }
        
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Destroy(gameObject);
        }
    }
}
