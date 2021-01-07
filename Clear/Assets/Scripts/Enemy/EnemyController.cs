using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    public float enemySpeed;
    public float enemyRetreatSpeed;
    public Transform[] randomSpot;
    private int singleRandomSpot;
    public float waitTimeLoop;
    private float changeWaitTime;

    public float detectRange;
    public float stoppingDistance;
    public float retreatingDistance;
    public float meleeDistance;
    Transform playerTransform;

    public GameObject bulletPrefab;
    public Transform firePoint;
    Rigidbody2D rb;

    public float startTimeBetweenShots;
    float timeBetweenShots;
    bool ifShoot;

    public int enemyMaxHealth;
    //int enemyCurrentHealth;
    public int enemyCurrentHealth { get; set; }

    public float startTimeBtwMeleeAttack;
    float timeBtwMeleeAttack;
    public float meleeRange;
    public int  meleeDamage;
    [SerializeField] private bool ifPatrol;

    // Enemy Health Bar
    public EnemyHealthBar enemyHealthBar;

    //audio
    public AudioSource audioSource;
    public AudioClip enemyRifleShootSFX;

    public Sprite deadEnemy;
    SpriteRenderer spriteRenderer;
    public Collider2D enemyCollider;
    public Collider2D enemyTriggerCollider;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        singleRandomSpot = Random.Range(0, randomSpot.Length);
        changeWaitTime = waitTimeLoop; 
        rb = GetComponent<Rigidbody2D>();
        timeBetweenShots = startTimeBetweenShots;
        enemyCurrentHealth = enemyMaxHealth;
        enemyHealthBar.SetEnemyMaxHealth(enemyMaxHealth);
        timeBtwMeleeAttack = startTimeBtwMeleeAttack;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void enemyShoot()
    {
        if (timeBetweenShots <= 0)
        {
            audioSource.PlayOneShot(enemyRifleShootSFX);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ifPatrol)
        {
            if (!isDead)
            {
                patrolEnemyBehaviour();
            }
            else
            {
                enemyCollider.enabled = false;
                enemyTriggerCollider.enabled = false;
            }
            
        }
        else
        {
            if (!isDead)
            {
                normalEnemyBehaviour();
            }
            else
            {
                enemyCollider.enabled = false;
                enemyTriggerCollider.enabled = false;
            }
        }

        
    }

    public void dealingDamageToEnemy(int damageAmount)
    {
        enemyCurrentHealth = Mathf.Clamp(enemyCurrentHealth + damageAmount, 0, enemyMaxHealth);
        enemyHealthBar.SetEnemyHealth(enemyCurrentHealth);
        if (enemyCurrentHealth <= 0)
        {
            spriteRenderer.sprite = deadEnemy;
            isDead = true;
            spriteRenderer.sortingOrder = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, meleeRange);
    }

    public void patrolEnemyBehaviour()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        //Debug.Log(distance);
        if (distance < detectRange) // detect player
        {
            Vector2 shootDir = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90.0f;
            rb.rotation = angle;

            if (distance > stoppingDistance) // move towards player and shoot 
            {
                ifShoot = true;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                // Debug.Log("Moving towards");

            }
            else if (distance < stoppingDistance && distance > retreatingDistance) // remain position and shoot 
            {
                ifShoot = true;
                transform.position = this.transform.position;
                //  Debug.Log("remain position");
            }
            else if (distance > meleeDistance && distance < retreatingDistance)// retreat and shoot 
            {
                ifShoot = true;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, -enemyRetreatSpeed * Time.deltaTime);
                // Debug.Log("Retreat");
            }
            else if (distance < meleeDistance)  // remain position and melee 
            {
                ifShoot = false;
                //Debug.Log("melee");
                if (timeBtwMeleeAttack <= 0)
                {
                    Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(firePoint.position, meleeRange);
                    for (int i = 0; i < objectsToDamage.Length; i++)
                    {
                        if (objectsToDamage[i].CompareTag("Player"))
                        {
                            objectsToDamage[i].GetComponent<PlayerController>().dealDamageToPlayer(-meleeDamage);
                        }

                    }
                    timeBtwMeleeAttack = startTimeBtwMeleeAttack;
                }
                else
                {
                    timeBtwMeleeAttack -= Time.deltaTime;
                }
                transform.position = this.transform.position;
            }

            // Enemy shooting
            if (ifShoot)
            {
                enemyShoot();
            }


        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, randomSpot[singleRandomSpot].position, enemySpeed * Time.deltaTime);
            if (changeWaitTime <= 0)
            {
                int lastSpot = singleRandomSpot;
                singleRandomSpot = Random.Range(0, randomSpot.Length);
                // Debug.Log(singleRandomSpot);
                if (lastSpot == singleRandomSpot)
                {
                    changeWaitTime = 0;
                }

                changeWaitTime = waitTimeLoop;
            }
            else
            {
                changeWaitTime -= Time.deltaTime;

            }
        }
    }

    public void normalEnemyBehaviour()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        //Debug.Log(distance);
        if (distance < detectRange) // detect player
        {
            Vector2 shootDir = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90.0f;
            rb.rotation = angle;

            if (distance > stoppingDistance) // move towards player and shoot 
            {
                ifShoot = true;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                // Debug.Log("Moving towards");

            }
            else if (distance < stoppingDistance && distance > retreatingDistance) // remain position and shoot 
            {
                ifShoot = true;
                transform.position = this.transform.position;
                //  Debug.Log("remain position");
            }
            else if (distance > meleeDistance && distance < retreatingDistance)// retreat and shoot 
            {
                ifShoot = true;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, -enemyRetreatSpeed * Time.deltaTime);
                // Debug.Log("Retreat");
            }
            else if (distance < meleeDistance)  // remain position and melee 
            {
                ifShoot = false;
                //Debug.Log("melee");
                if (timeBtwMeleeAttack <= 0)
                {
                    Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(firePoint.position, meleeRange);
                    for (int i = 0; i < objectsToDamage.Length; i++)
                    {
                        if (objectsToDamage[i].CompareTag("Player"))
                        {
                            objectsToDamage[i].GetComponent<PlayerController>().dealDamageToPlayer(-meleeDamage);
                        }

                    }
                    timeBtwMeleeAttack = startTimeBtwMeleeAttack;
                }
                else
                {
                    timeBtwMeleeAttack -= Time.deltaTime;
                }
                transform.position = this.transform.position;
            }

            // Enemy shooting
            if (ifShoot)
            {
                enemyShoot();
            }


        }
    }

}
