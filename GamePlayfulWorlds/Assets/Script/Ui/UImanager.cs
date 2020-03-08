using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header ("Audio")]
    private AudioSource _AudioSourceUI;
    public AudioClip hitmarkerSound;
    public AudioClip powerUpSound;

    [Header("Porperties")]
    public float delay = 0.05f;

    [Header("Input")]
    public Image hitmarker;//hitmarker
    void Start()
    {
        _AudioSourceUI = GetComponent<AudioSource>();
        hitmarker = GameObject.Find("Hitmarker").GetComponent<Image>();
        hitmarker.enabled = false;
    }

    void Update()
    {
        
    }
    public IEnumerator ShowHitmarker()
    {
        hitmarker.enabled = true;
        yield return new WaitForSeconds(delay);
        PlayHitmarkerSound();
        hitmarker.enabled = false;
    }
    public void PlayHitmarkerSound()
    {
        _AudioSourceUI.PlayOneShot(hitmarkerSound);
    }

    public void PlayPickUpSound()
    {
        _AudioSourceUI.PlayOneShot(powerUpSound);
    }
}
