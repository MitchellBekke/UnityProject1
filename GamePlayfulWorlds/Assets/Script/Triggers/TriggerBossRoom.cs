using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OPEN DEUR");
    }
}
