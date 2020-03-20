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
    public AudioClip keySound;
    public AudioClip damageSound;
    public AudioClip bossMusic;

    [Header("Porperties")]
    public float delay = 0.05f;
    public int keyCount = 0;
    public bool isShowingText = false;

    [Header("Input")]
    public Image hitmarker;//hitmarker
    public TMPro.TextMeshProUGUI keyCountText;
    public TMPro.TextMeshProUGUI startText;
    public TMPro.TextMeshProUGUI keyMaxText;
    public GameObject boss;
    public Image redScreen;

    public TriggerBossRoom triggerBossRoom = null;
    void Start()
    {
        _AudioSourceUI = GetComponent<AudioSource>();
        hitmarker.enabled = false;
        redScreen.enabled = false;
        keyMaxText.enabled = false;
        StartCoroutine(ShowStartText());
    }

    void Update()
    {
        StartCoroutine(ShowMaxKeyText());
    }
    public IEnumerator ShowMaxKeyText()
    {
        if (keyCount == 5 && isShowingText == false)
        { 
            keyMaxText.enabled = true;
            yield return new WaitForSeconds(3);
            keyMaxText.enabled = false;
            isShowingText = true;
        }
    }
    public IEnumerator ShowHitmarker()
    {
        hitmarker.enabled = true;
        yield return new WaitForSeconds(delay);
        PlayHitmarkerSound();
        hitmarker.enabled = false;
    }
    public IEnumerator ShowRedScreen()
    {
        redScreen.enabled = true;
        yield return new WaitForSeconds(delay);
        PlayDamageSound();
        redScreen.enabled = false;
    }
    public IEnumerator ShowStartText()
    {
        startText.enabled = true;
        yield return new WaitForSeconds(3.5f);
        startText.enabled = false;
    }
    public void PlayHitmarkerSound()
    {
        _AudioSourceUI.PlayOneShot(hitmarkerSound);
    }

    public void PlayPickUpSound()
    {
        _AudioSourceUI.PlayOneShot(powerUpSound);
    }
    public void PlayKeySound()
    {
        keyCount++;
        keyCountText.text = "Keys: " + keyCount;
        _AudioSourceUI.PlayOneShot(keySound);
    }
    public void PlayDamageSound()
    {
        _AudioSourceUI.PlayOneShot(damageSound);
    }

    public void PlayBossMusic()
    {
        _AudioSourceUI.PlayOneShot(bossMusic);
    }
}
