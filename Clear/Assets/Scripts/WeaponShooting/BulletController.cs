using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float distanceTravelled = 0;
    Vector2 lastPositon;
    public int bulletDamage;
    public AudioClip[] hitWallClips;
    public GameObject particleSystemBulletHitWall;
    public GameObject particleSystemBulletHitPeople;

    private void Start()
    {
        lastPositon = transform.position;
        //ps.Stop();
    }

    private void Update()
    {
        distanceTravelled += Vector2.Distance(transform.position, lastPositon);
        lastPositon = transform.position;
        if(distanceTravelled > 12f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.dealingDamageToEnemy(-bulletDamage);
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
            obstacle.dealingDamageToObstacle(-bulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (fleeDog != null)
        {
            fleeDog.dealDamageToFleeingDog(-bulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (dog != null)
        {
            dog.dealDamageToDog(-bulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (turret != null)
        {
            turret.dealingDamageToTurret(-bulletDamage);
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Walls"))
        {
            int randomNumber = Random.Range(0, hitWallClips.Length);
            AudioClip randomClip = hitWallClips[randomNumber];
            AudioSource.PlayClipAtPoint(randomClip, transform.position);
            //ps.enableEmission = true;
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
           // Destroy(particleSystemBulletHitWall, 0.5f);
            Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Bullet")) 
        {
            //Physics2D.IgnoreCollision(, collision.collider.CompareTag("Bullet"));
            Destroy(gameObject);
        }

    }
}
