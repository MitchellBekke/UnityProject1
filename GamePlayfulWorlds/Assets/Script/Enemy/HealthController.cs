using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float startHealth = 100;//health duh 
    [SerializeField] private float health = 100;//health duh 

    public int randomNummerMin = 1; // voor spawn van drops
    public int randomNummerMax = 10;// voor spawn van drops

    public Transform ammoDrop;

    public int juisteGetal = 5;// dit getal zodat de drop spawned

    public Image healthBar;
    public void Awake()
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
            int ranNum = Random.Range(randomNummerMin, randomNummerMax);
            if(ranNum == juisteGetal)
            {
                Instantiate(ammoDrop, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    
}
