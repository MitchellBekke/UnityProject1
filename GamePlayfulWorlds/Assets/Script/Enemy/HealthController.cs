using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float startHealth = 100;//health duh 
    [SerializeField] private float health = 100;//health duh 

    public int randomNummerMin = 1; // voor spawn van drops
    public int randomNummerMaxAmmo = 5;// voor spawn van drops

    public int randomNummerMaxHealth = 10;// voor spawn van drops

    public Transform ammoDrop;
    public Transform healthDrop;

    public int juisteGetalAmmo = 5;// dit getal zodat de drop spawned
    public int juisteGetalHealth = 4;

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
            int ranNumAmmo = Random.Range(randomNummerMin, randomNummerMaxAmmo);
            int ranNumHeart = Random.Range(randomNummerMin, randomNummerMaxHealth);
            if(ranNumAmmo == juisteGetalAmmo)
            {
                Instantiate(ammoDrop, gameObject.transform.position, gameObject.transform.rotation);
            }
            else if(ranNumHeart == juisteGetalHealth)
            {
                Instantiate(healthDrop, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    
}
