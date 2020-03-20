using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    HealthController healthController = null;
    public TMPro.TextMeshProUGUI victoryText;
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        victoryText.enabled = false;
        healthController = GameObject.Find("Boss").GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthController.health <= 0)
        {
            victoryText.enabled = true;
            spawner.SetActive(false);
        }
    }
}
