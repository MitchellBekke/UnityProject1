using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeManager : MonoBehaviour
{
    HitmarkerManager hitmarkerManager = null;

    public float damage = 80;
    public float range = 1;
    public LayerMask enemyLayers;

    Weapon weapon = null;

    public Transform attackPoint;
    public GameObject tommyGun;
    public GameObject switchBlade;

    public bool IsMelee = false;
    void Start()
    {
        hitmarkerManager = GameObject.Find("UIManager").GetComponent<HitmarkerManager>();
        weapon = GetComponentInChildren<Weapon>();
        tommyGun = GameObject.Find("Tommygun");
        switchBlade = GameObject.Find("Melee");
        switchBlade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayMelee();
    }
    private void PlayMelee()
    {
        if (Input.GetKeyDown(KeyCode.E) && !weapon.IsReloading && !weapon.IsAiming && !IsMelee && !weapon.IsSpawning)//kijk of ik niks doe met het wapen om vervolgens het mes te enablen en de tommygun te disablen. en doe dan attack logica
        {
            //MELEE LOGICA HIER
            switchBlade.SetActive(true);
            tommyGun.SetActive(false);
            IsMelee = true;
            Attack();
        }
    }
    public void Attack()//attack functie
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, enemyLayers);// maakt een sphere aan op het attack punt. en kijkt vervolgens naar of de layer mask overlapt

        foreach (Collider enemy in hitEnemies)
        {
            StartCoroutine(hitmarkerManager.ShowHitmarker());
            enemy.GetComponent<HealthController>().ApplyDamage(damage); //voor elke overlapte enemy doe damage
        }
    }

    private void OnDrawGizmosSelected()// dit is puur voor de editor om de attack range te zien
    {
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
