using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    UImanager uiManager = null;
    private void Start()
    {
        uiManager = GameObject.Find("UISoundManager").GetComponent<UImanager>();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * -Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)//call when player enters the object
    {
        uiManager.PlayKeySound();
        Destroy(gameObject);
    }

    // Update is called once per frame
    
}

