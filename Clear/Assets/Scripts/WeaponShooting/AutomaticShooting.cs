using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticShooting : MonoBehaviour
{
    //public static AutomaticShooting Instance { get; private set; }

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float shootingInterval;
    Coroutine fireCoutinuously = null;

    // Reload System
    public AutomaticWeaponReload automaticWeaponReload;
    public float automaticRifleReloadTime;
    public int rifleMaxAmmo; // max ammo per magazine
    public int rifleTotalAmmo;  // bullet net total
    public int rifleTotalAmmoCopy { get; private set; }
    public int rifleCurrentAmmo { get; set; }
    public bool getRifleIsReloading { get { return rifleIsReloading; } }
    bool rifleIsReloading;

    public AudioSource audioSource;
    public AudioClip M4A1SFX;
    public AudioClip reloadSFX;
    public AudioClip emptyMagazineDryShoot;

    bool isSoundPlayed = false;
    public float screenShakeIntensity, screenShakeTime;

    // Start is called before the first frame update
    void Awake()
    {
        // Instance = this;
        rifleIsReloading = false;
        rifleCurrentAmmo = rifleMaxAmmo;
        rifleTotalAmmoCopy = rifleTotalAmmo;
        automaticWeaponReload.setMaxReloadTime(automaticRifleReloadTime);
    }

    /*    private void OnEnable() // prevent bug of switching weapons while reloading? 
        {
            rifleIsReloading = false;
        }*/

    // Update is called once per frame
    void Update()
    {
        if (rifleTotalAmmo > 0 || rifleCurrentAmmo > 0)
        {
            if (rifleIsReloading)
            {
                // Debug.Log(rifleIsReloading);
                StopCoroutine(fireCoutinuously);
                return;
            }
            if (rifleCurrentAmmo <= 0)
            {
                StartCoroutine(ReloadRifle());
                // rifleIsReloading = true;
                return;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (rifleTotalAmmo > 0 && rifleCurrentAmmo < rifleMaxAmmo)
                {
                    StartCoroutine(ReloadRifle());
                    return;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                fireCoutinuously = StartCoroutine(shootingCoroutine());
                // Debug.Log("Firing continuously");
            }
            if (Input.GetButtonUp("Fire1"))
            {
                // Debug.Log("Button up detected");
                StopCoroutine(fireCoutinuously);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                audioSource.PlayOneShot(emptyMagazineDryShoot);
            }


            StopCoroutine(fireCoutinuously);
            automaticWeaponReload.canvasGroup.alpha = 0f;
        }

    }

    IEnumerator shootingCoroutine()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            CinemachineShake.Instance.shakeCamera(screenShakeIntensity, screenShakeTime);
            rifleCurrentAmmo--;
            audioSource.clip = M4A1SFX;
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(0.1f);
            // Debug.Log($"rifleCurrentAmmo {rifleCurrentAmmo}");
        }
    }
    IEnumerator ReloadRifle()
    {

        rifleIsReloading = true;
        audioSource.clip = reloadSFX;
        audioSource.PlayOneShot(audioSource.clip);
        for (float i = 0; i < automaticRifleReloadTime; i += 0.1f)
        {
            float progress = Mathf.Clamp01(i / 0.9f);
            automaticWeaponReload.setReloadTime(progress);
            yield return new WaitForSeconds(0.1f);
        }
        if (rifleTotalAmmo - (rifleMaxAmmo - rifleCurrentAmmo) >= 0)
        {
            rifleTotalAmmo -= (rifleMaxAmmo - rifleCurrentAmmo);
            rifleCurrentAmmo = rifleMaxAmmo;
        }
        else
        {
            rifleCurrentAmmo += rifleTotalAmmo;
            rifleTotalAmmo = 0;
        }
        rifleIsReloading = false;
    }
}
