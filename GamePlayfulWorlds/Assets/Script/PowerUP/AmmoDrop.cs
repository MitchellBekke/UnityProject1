﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    public Weapon weapon;
    UImanager uiManager = null;
    private void Start()
    {
        uiManager = GameObject.Find("UISoundManager").GetComponent<UImanager>();
    }
    private void OnTriggerEnter(Collider other)//call when player enters the object
    {
        weapon.bulletsLeft = weapon.bulletsLeft + 30;
        weapon.UpdateAmmoText();
        uiManager.PlayPickUpSound();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
        weapon = GameObject.Find("Tommygun").GetComponent<Weapon>();//probleem dat die hier staat is omdat ik mijn tommygun hide tijdens de melee attack. hierdoor kan het in de start functie niet mijn tommygun vinden.
    }
}