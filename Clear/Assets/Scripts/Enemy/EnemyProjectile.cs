using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float enemyProjectileSpeed;
    public int bulletDamage;
    Transform playerTransform;
    Vector2 target;
    public GameObject particleSystemBulletHitPeople;
    public GameObject particleSystemBulletHitWall;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, enemyProjectileSpeed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // maybe considering layermask? 
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        ObstacleHealth obstacle = collision.collider.GetComponent<ObstacleHealth>();
        if (player != null)
        {
            player.dealDamageToPlayer(-bulletDamage);
            Instantiate(particleSystemBulletHitPeople, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Walls"))
        {
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (obstacle != null)
        {
            Instantiate(particleSystemBulletHitWall, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        /*if(collision.collider.gameObject.CompareTag("Obstacles"))//== GameObject.FindGameObjectWithTag("Obstacles")
        {
            Destroy(gameObject);
        }*/

        else if (!collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
