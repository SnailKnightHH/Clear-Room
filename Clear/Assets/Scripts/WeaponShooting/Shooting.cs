using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //public static Shooting Instance { get; private set; }

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float force;
    public float startTimeBtwShots;
    float timeBtwShots;
    bool firstShot = true;

    // Ammo & Reload System
    public int maxAmmo; // max ammo for each magazine 
    public int pistolTotalAmmo; // ammo net total 
    public int pistolTotalAmmoCopy { get; private set; }
    public int currentAmmo { get; set; }
    public float reloadingTime;

    bool isReloading;
    public bool getIsReloading { get { return isReloading; } }
    public PistolReloadBar pistolReloadBar;

    public AudioSource audioSource;
    public AudioClip pistolShootingSFX;
    public AudioClip pistolReloadSFX;
    public AudioClip emptyMagazineDryShoot;

    public float screenShakeIntensity;
    public float screenShakeTime;

    // public PistolReloadBar pistolReloadBar;

    /*    private void Awake()
        {
            Instance = this;
            //timeBtwShots = startTimeBtwShots;
           // currentAmmo = maxAmmo;
            //reloadBar.setMaxReloadTime(reloadingTime);
        }*/

    private void Awake()
    {
        //   Instance = this;
        isReloading = false;
        //    pistolReloadBar = GetComponent<PistolReloadBar>();
        timeBtwShots = startTimeBtwShots;
        currentAmmo = maxAmmo;
        pistolReloadBar.setMaxReloadTime(reloadingTime);
        pistolTotalAmmoCopy = pistolTotalAmmo;
    }

    /*   private void OnEnable()
       {
           isReloading = false;
       }*/

    private void Update()
    {
        if (pistolTotalAmmo > 0 || currentAmmo > 0) // only perform the following actions when you still have ammo left 
        {
            if (isReloading)
            {
                return;
            }
            if (currentAmmo <= 0)
            {
                Debug.Log("proceed to reload");
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"R pressed {pistolTotalAmmo} {currentAmmo} {maxAmmo}");
                if (pistolTotalAmmo > 0 && currentAmmo < maxAmmo)
                {
                    Debug.Log("proceed to reload");
                    StartCoroutine(Reload());
                    //  return;
                }

            }
            // Fire A Bullet 
            if (firstShot) // so the player does not have to wait for half a second to fire the first shot 
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                    firstShot = false;
                }
            }

            else
            {
                if (timeBtwShots <= 0)
                {

                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                        timeBtwShots = startTimeBtwShots;
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                audioSource.PlayOneShot(emptyMagazineDryShoot);
            }
            pistolReloadBar.canvasGroup.alpha = 0f;
        }


    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        CinemachineShake.Instance.shakeCamera(screenShakeIntensity, screenShakeTime);
        audioSource.clip = pistolShootingSFX;
        audioSource.PlayOneShot(audioSource.clip);
        currentAmmo--;
    }

    IEnumerator Reload()
    {

        isReloading = true;
        audioSource.clip = pistolReloadSFX;
        audioSource.PlayOneShot(audioSource.clip);
        for (float i = 0; i < reloadingTime; i += 0.1f)
        {
            float progress = Mathf.Clamp01(i / 0.9f);
            pistolReloadBar.setReloadTime(progress);
            yield return new WaitForSeconds(0.1f);
        }
        if (pistolTotalAmmo - (maxAmmo - currentAmmo) >= 0)
        {
            pistolTotalAmmo -= (maxAmmo - currentAmmo);
            currentAmmo = maxAmmo;
        }
        else
        {
            currentAmmo += pistolTotalAmmo;
            pistolTotalAmmo = 0;
        }

        // currentAmmo = maxAmmo;

        Debug.Log("Reload Complete");
        isReloading = false;
    }

}
