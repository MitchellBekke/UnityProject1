using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDrop : MonoBehaviour
{
    public PlayerHealth playerHealth;
    UImanager uiManager = null;
    private void Start()
    {
        uiManager = GameObject.Find("UISoundManager").GetComponent<UImanager>();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
        if (GameObject.Find("FPSController") != null)
        {
            playerHealth = GameObject.Find("FPSController").GetComponent<PlayerHealth>();
        }
    }
    private void OnTriggerEnter(Collider other)//call when player enters the object
    {
        if (playerHealth != null)
        {
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.currentHealth += 20;
                playerHealth.currentHealth = Mathf.Clamp(playerHealth.currentHealth, 0, 100);
            }
            uiManager.PlayPickUpSound();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    
}

