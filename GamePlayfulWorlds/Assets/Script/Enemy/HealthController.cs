using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    [SerializeField] private float health = 100;//health duh
    public Image hitmarker;//hitmarker 
    public float delay = 0.05f;

    public void Start()
    {
        hitmarker = GameObject.Find("Hitmarker").GetComponent<Image>();
        hitmarker.enabled = false;
    }
    // Start is called before the first frame update
    public void ApplyDamage(float damage)
    {
        health -= damage;
        StartCoroutine(ShowHitmarker());

        if (health <= 0)
        {
            hitmarker.enabled = false;
            Destroy(gameObject);
        }
    }

    public IEnumerator ShowHitmarker()
    {
        hitmarker.enabled = true;
        yield return new WaitForSeconds(delay);
        hitmarker.enabled = false;
    }
}
