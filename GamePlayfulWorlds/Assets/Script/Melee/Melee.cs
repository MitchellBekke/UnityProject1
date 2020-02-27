using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{ 
    MeleeManager meleeManager = null;
    Weapon weapon = null;
    public GameObject tommyGun;
    public GameObject switchBlade;
    void Start()
    {
        meleeManager = GetComponentInParent<MeleeManager>();
        weapon = GetComponentInChildren<Weapon>();
        tommyGun = GameObject.Find("Tommygun");
        switchBlade = GameObject.Find("Melee");

    }
    
    public void enableTommy() // spawn de tommygun weer en disable het melee wapen
    {
        switchBlade.SetActive(false);
        tommyGun.SetActive(true);
        meleeManager.IsMelee = false;
    }    
}
