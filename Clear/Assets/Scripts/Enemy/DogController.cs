using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float speed;
    private Transform target;
    bool ifMoving = true;
    public PlayerController playerController;
    public Transform playerTransform;
    public Rigidbody2D rb2;
    public float startTimeBtwAttack;
    private float timeBTWAttack;
    public float dogMaxHealth;
    private float dogHealth;
    public AudioSource audioSource;
    public AudioClip dogBite;
    SpriteRenderer spriteRenderer;
    public Sprite deadDog;
    bool dogDead = false;
    Collider2D dogCollider; 

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBTWAttack = startTimeBtwAttack;
        dogHealth = dogMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        dogCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dogDead)
        {
            if (Vector2.Distance(transform.position, target.position) < 15f && Vector2.Distance(transform.position, target.position) > 3f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, target.position) < 3f)
            {
                if (timeBTWAttack > 0)
                {
                    timeBTWAttack -= Time.deltaTime;
                }
                else
                {
                    audioSource.PlayOneShot(dogBite);
                    playerController.dealDamageToPlayer(-20);
                    timeBTWAttack = startTimeBtwAttack;
                }

            }
        }
        

    }

    private void LateUpdate()
    {
        if (!dogDead)
        {
            Vector2 shootDir = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90.0f;
            rb2.rotation = angle;
        }

    }

    public void dealDamageToDog(int damageAmount)
    {
        dogHealth = Mathf.Clamp(dogHealth + damageAmount, 0, dogMaxHealth);
        if (dogHealth <= 0)
        {
            dogDead = true;
            spriteRenderer.sprite = deadDog;
            spriteRenderer.sortingOrder = 1;
            dogCollider.enabled = false;
        }
    }


}
