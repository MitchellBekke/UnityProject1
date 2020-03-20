using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerKey : MonoBehaviour
{
    UImanager uiManager = null;
    public TMPro.TextMeshProUGUI messageText;
    public Animator animDeurL;
    public Animator animDeurR;
    private bool alleKeys;
    public int maxKeys;
    private void Start()
    {
        uiManager = GameObject.Find("UISoundManager").GetComponent<UImanager>();
        messageText.enabled = false;
    }

    private void Update()
    {
        if (alleKeys == true)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (uiManager.keyCount == maxKeys)
            {
                animDeurR.CrossFadeInFixedTime("DeurOpenR", 0f);
                animDeurL.CrossFadeInFixedTime("DeurOpenL", 0f);
                alleKeys = true;
            }
            else
            {
                messageText.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        messageText.enabled = false;
    }
}
