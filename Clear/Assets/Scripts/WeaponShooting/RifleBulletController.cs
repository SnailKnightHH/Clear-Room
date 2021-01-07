using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBulletController : MonoBehaviour
{
    float distanceTravelled = 0;
    Vector2 lastPositon;
    public int rifleBulletDamage;
    public AudioClip[] hitWallClips;
    public GameObject particleSystemBulletHitWall;
    public GameObject particleSystemBulletHitPeople; 

    private void Start()
    {
        lastPositon = transform.position;
    }

    private void Update()
    {
        distanceTravelled += Vector2.Distance(transform.position, lastPositon);
        lastPositon = transform.position;

        if (distanceTravelled > 20f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.dealingDamageToEnemy(-rifleBulletDamage);
            Instantiate(particleSystemBulletHitPeople, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObstacleHealth obstacle = collision.collider.GetComponent<ObstacleHealth>();
        FleeingDogController fleeDog = collision.collider.GetComponent<FleeingDogController>();
        DogController dog = collision.collider.GetComponent<DogController>();
        TurretController turret = collision.collider.GetComponent<TurretController>();
        if (obstacle != null)
        {
            obstacle.dealingDamageToObstacle(-rifleBulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (fleeDog != null)
        {
            fleeDog.dealDamageToFleeingDog(-rifleBulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (dog != null)
        {
            dog.dealDamageToDog(-rifleBulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (turret != null)
        {
            turret.dealingDamageToTurret(-rifleBulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Walls"))
        {
            int randomNumber = Random.Range(0, hitWallClips.Length);
            AudioClip randomClip = hitWallClips[randomNumber];
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

    }
}
