using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    UImanager uiManager = null;
    private Animator anim;// de animator op het wapen
    private AudioSource _AudioSource;// 0 idee eerlijk gezegd, maar ik neem aan de plek waar audio vandaan komt

    [Header("Properties")]
    public float reloadTime = 3.2f; // deze moet 3.2 of tenminste de exacte tijd van de reload animatie want anders bugged het met de root motion
    public float spreadFactor = 0.1f;// hoe groot de spread is als je hipfired
    public float damage = 20;//damage wat verwacht je
    public float fireRate = 0.1f;//delay between each shot
    public float range = 100f; //max range for weapon
    public float AODspeed = 8f; // snelheid waarmee het wapen van punt A naar B verplaats voor het aimen
    public float smoothAiming = 2;// snelheid waarmee de camera verplaats tijdens het aimen
    public int currentBullets; // current bullets in mag
    public int bulletsPerMag = 50; // Magazine size
    public int bulletsLeft = 200;// Total bullets
    public int FOVStartCamera = 67;// start punt van de weapon camera
    public int FOVAimCamera = 55;// ingezoomde punt van de weapon cam na het aimen


    [Header("Input")]
    public Image crosshair; // crosshair
    public TMPro.TextMeshProUGUI ammoText;// text waar je huidige ammo word weergegeven
    public TMPro.TextMeshProUGUI totalBulletText;// hoeveel totale bullets je hebt
    public Transform shootPoint; // point from were we shoot
    public GameObject hitParticles;// de sparks die spawnen als je iets raakt
    public GameObject bulletImpact;// de bullethole die spawnen als je iets raakt
    public ParticleSystem muzzleFlash;// de flash als je schiet die uit de barrel komt (VUUR)
    public Camera worldCamera;// De camera die alles behalve het wapen bekijkt. deze zoomed in door een ander stuk code
    public Vector3 aimPosition; // positie waar het wapen heen gaat als je aimed.

    [Header("Audio")]
    public AudioClip shootSound; // geluid dat gebruikt word als je schiet

    float fireTimer; // time counter for the delay

    private Vector3 originalPosition;// positie van het wapen als we inladen
    
    public bool IsReloading;//is de speler aan het reloaden ja of nee?
    public bool IsSpawning;// is de speler aan het inspawnen?
    public bool IsAiming;// is de speler aan het aimen ja of nee?
    public bool IsSprinting;// is de speler aan het sprinten ja of nee?

    private void OnEnable()
    {
        IsSpawning = true;
    }
    void Start()
    {
        //VERGEET NIET HIER ALLES TE ASIGNEN ZODAT ALS JE LATER RESPAWNING DOET ALLES ASIGNED WORD

        uiManager = GameObject.Find("UISoundManager").GetComponent<UImanager>();
        anim = GetComponent<Animator>();//pakt animator van het wapen en linked anim
        _AudioSource = GetComponent<AudioSource>();//pakt audiosource van het wapen en linked _AudioSource

        currentBullets = bulletsPerMag; // zorgt voor een geladen magazijn
        originalPosition = transform.localPosition;// zet de orginele positie gelijk aan wat je hebt als je spawned
        crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        ammoText = GameObject.Find("BulletCount").GetComponent<TMPro.TextMeshProUGUI>();
        totalBulletText = GameObject.Find("TotalBullets").GetComponent<TMPro.TextMeshProUGUI>();
        UpdateAmmoText();// Update de text UI

    }
    void Update()
    {
        if (Input.GetButton("Fire1"))//als je de linker muis knop indrukt doe dit
        {
            if (currentBullets > 0)
            {
                Fire();//Use Fire function
            }
            else if(bulletsLeft > 0 && !IsReloading && !IsSpawning)
            {
                StartCoroutine(DoReload());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))//als je R drukt doe reload
        {
            if (currentBullets < bulletsPerMag && bulletsLeft > 0 && !IsReloading && !IsSpawning )
            {
                StartCoroutine(DoReload());
            }
        }

        if (fireTimer < fireRate)//increased firetimer als het minder is dan firerate
            fireTimer += Time.deltaTime; //add time on the time counter

        Sprinting();
        AimDownSights();// kijkt of je Rechtermuisknop doet zo ja aim
        UpdateCrosshair();//kijkt of je aimed en zo ja haalt de crosshair weg

        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
                
        anim.SetBool("Aim", IsAiming); // zet de bool aim in de animator gelijk aan de bool IsAiming
        anim.SetBool("SprintRun", IsSprinting);//zet de bool sprint in de animator gelijk aan de bool IsSprinting
    }

    private void AimDownSights()//Aim functie
    {
        if (Input.GetButton("Fire2") && !IsReloading && !IsSpawning)//kijkt of je Rechtermuisknop doet zo ja aim
        {
            IsAiming = true;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * AODspeed);
            worldCamera.fieldOfView = Mathf.Lerp(worldCamera.fieldOfView, FOVAimCamera, Time.deltaTime * smoothAiming);
        }
        else
        {
            IsAiming = false;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * AODspeed);
           worldCamera.fieldOfView = Mathf.Lerp(worldCamera.fieldOfView, FOVStartCamera, Time.deltaTime * smoothAiming);
        }
    }

    private void Fire()//Schiet functie
    {
        if (fireTimer < fireRate || currentBullets <= 0  || IsReloading || IsSpawning || IsSprinting) //doe de commando niet als 1 van deze opties waar is.
            return;
        RaycastHit hit;

        Vector3 shootDirection = shootPoint.transform.forward;// shootDirection is naar voren voor de raycast shooting
        if (IsAiming == false)// als je aimed is het recht voor uit, maar als je niet aimed ga in dit commando en geef ons een spreadfactor
        {
            shootDirection = shootDirection + shootPoint.TransformDirection
            (new Vector3(Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor)));
        }
        

        if (Physics.Raycast(shootPoint.position, shootDirection, out hit, range))// als je contact maakt met iets doe het volgende
        {
            // Particle spawn op impact en maak het een child van het gehitte object 
            GameObject hitParticlesEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hitParticlesEffect.transform.SetParent(hit.transform);
            // bullethole spawn op impact en maak het een child van het gehitte object 
            GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            bulletHole.transform.SetParent(hit.transform);

            Destroy(hitParticlesEffect, 1f);//destroy het particle effect na 1 seconde
            Destroy(bulletHole, 2f);//destroy de bullethole na 2 seconde

            if (hit.transform.GetComponent<HealthController>())//als het een script heeft genaamd Healthcontroller doe dit ommando
            {
                StartCoroutine(uiManager.ShowHitmarker());
                hit.transform.GetComponent<HealthController>().ApplyDamage(damage); //stuurt het damage variabel naar het healthcontroller script.
            }
        }

        anim.CrossFadeInFixedTime("Fire", 0.01f); //play fire anim 
        muzzleFlash.Play();//show muzzle flash
        PlayShootSound(); //shoot sound effect

        currentBullets--;//deduct bullet from mag
        UpdateAmmoText();// update ammo text
        fireTimer = 0; //reset firetimer
    }

    public void Reload()//Reload functie
    {
        if (bulletsLeft <= 0) return;
        {
            int bulletsToLoad = bulletsPerMag - currentBullets;
            //                         if                      then   1st      else  2nd
            int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

            bulletsLeft -= bulletsToDeduct;
            currentBullets += bulletsToDeduct;
            UpdateAmmoText();
            EnableRootMotion(); // zet root motion aan zodat idle, fire en andere animaties weer normaal werken
        }
    }
    private IEnumerator DoReload()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        IsReloading = true;
        DisableRootMotion();// zet root motion uit zodat de reload animatie niet bugged
        anim.CrossFadeInFixedTime("Reload", 0.01f);

        yield return new WaitForSeconds(reloadTime);// wacht voor 3.2 seconde en zet daarna pas IsReloading op false en doe de reload calculatie

        IsReloading = false;
        Reload();
    }

    private void PlayShootSound()
    {
        _AudioSource.PlayOneShot(shootSound);
    }

    public void UpdateAmmoText()
    {
        ammoText.text = currentBullets + "/" + bulletsPerMag;// 50/50
        totalBulletText.text = bulletsLeft.ToString(); //200
    }

    private void UpdateCrosshair()//Crosshair update. zorgt ervoor dat als je aimed de crosshair verdwijnt
    {
        if(IsAiming == true)
        {
            crosshair.enabled = false;
        }
        else
        {
            crosshair.enabled = true;
        }
    }

    private void Sprinting()//Aim functie
    {
        if (Input.GetKey(KeyCode.LeftShift) && !IsReloading && !IsAiming)//Sprintlogica
        {
            IsSprinting = true;
        }
        else
        {
            IsSprinting = false;
        }
    }

    private void EnableRootMotion()//Animation event 
    {
        anim.applyRootMotion = true;
    }
    private void DisableRootMotion()//Animation event 
    {
        anim.applyRootMotion = false;
    }
    private void SpawningUit()//Animation event 
    {
        IsSpawning = false;
    }
    private void SpawningAan()//Animation event 
    {
        IsSpawning = true;
    }
}
