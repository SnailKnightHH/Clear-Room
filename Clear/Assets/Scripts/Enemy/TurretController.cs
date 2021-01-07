using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TurretController : MonoBehaviour
{
    public PlayerController playerController;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    SpriteRenderer spriteRenderer;
    public float timeBtwShootSetUp;
    float timeBtwShoot;
    public float bulletForce;
    public float detectRange;
    public int turretMaxHealth;
    int turretCurrentHealth;
    public turretHealthBar turretHealthBar;
    public AudioSource audioSource;
    public AudioClip cannonFire;
    public Light2D turretLight;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        timeBtwShoot = timeBtwShootSetUp;
        turretCurrentHealth = turretMaxHealth;
        turretHealthBar.SetEnemyMaxHealth(turretMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerController.transform.position) < detectRange)
        {
            spriteRenderer.enabled = true;
            turretLight.enabled = true;
            Vector2 shootDir = playerController.transform.position - transform.position;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90.0f;
            rb.rotation = angle + 180;
            if(timeBtwShoot < 0)
            {
                TurretShoot(shootDir);
                timeBtwShoot = timeBtwShootSetUp;
            }
            else
            {
                timeBtwShoot -= Time.deltaTime;
            }
            
        }
        else
        {
            spriteRenderer.enabled = false;
            turretLight.enabled = false;
        }
    }

    private void TurretShoot(Vector2 shootDir)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(shootDir.normalized * bulletForce, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody2D bulletRB2 = bullet2.GetComponent<Rigidbody2D>();
      //  Vector2 shootDir2 = shootDir + new Vector2(-1, -Mathf.Tan(15)) * 3;
      //  bulletRB2.AddForce(shootDir2.normalized * bulletForce, ForceMode2D.Impulse);
        bulletRB2.AddForce(shootDir.normalized * bulletForce, ForceMode2D.Impulse);

        GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Rigidbody2D bulletRB3 = bullet3.GetComponent<Rigidbody2D>();
        bulletRB3.AddForce(shootDir.normalized * bulletForce, ForceMode2D.Impulse);
        // Vector2 shootDir3 = shootDir + new Vector2(1, Mathf.Tan(15)) * 3;
        //  bulletRB3.AddForce(shootDir3.normalized * bulletForce, ForceMode2D.Impulse);

        // Debug.Log("1 " + shootDir + "2 " + shootDir2 + "3 " + shootDir3);
        audioSource.PlayOneShot(cannonFire);

    }

    public void dealingDamageToTurret(int damageAmount)
    {
        turretCurrentHealth = Mathf.Clamp(turretCurrentHealth + damageAmount, 0, turretMaxHealth);
        turretHealthBar.SetEnemyHealth(turretCurrentHealth);
        if (turretCurrentHealth <= 0)
        {
            Destroy(gameObject);

        }
    }
}
