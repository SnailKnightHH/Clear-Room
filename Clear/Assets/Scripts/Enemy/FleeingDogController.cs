using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingDogController : MonoBehaviour
{
    public float speed;
    private Transform target;
    public PlayerController player;
    public Transform playerTransform;
    public Rigidbody2D rb2;
    bool ifFlee = true;
    public float startTimeBtwAttack;
    float timeBTWAttack;
    public int fleeDogMaxHealth;
    int fleeDogHealth;
    public AudioSource audioSource;
    public AudioClip dogBite;

    bool fleeDogDead = false;
    SpriteRenderer spriteRenderer;
    Collider2D fleeDogCollider;
    public Sprite deadDog;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBTWAttack = startTimeBtwAttack;
        fleeDogHealth = fleeDogMaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fleeDogCollider = GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        if (!fleeDogDead)
        {
            Vector2 shootDir = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90.0f;
            rb2.rotation = angle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!fleeDogDead)
        {
            if (Vector2.Distance(transform.position, target.position) < 15f)
            {
                if (ifFlee)
                    transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
                else
                {
                    if (Vector2.Distance(transform.position, target.position) < 3f)
                    {
                        if (timeBTWAttack > 0)
                        {
                            timeBTWAttack -= Time.deltaTime;
                        }
                        else
                        {
                            audioSource.PlayOneShot(dogBite);
                            player.dealDamageToPlayer(-20);
                            timeBTWAttack = startTimeBtwAttack;
                        }

                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    }
                }

            }
        }      

    }

    public void dealDamageToFleeingDog(int damageAmount)
    {
        fleeDogHealth = Mathf.Clamp(fleeDogHealth + damageAmount, 0, fleeDogMaxHealth);
        if (fleeDogHealth <= 0)
        {
            fleeDogDead = true;
            spriteRenderer.sprite = deadDog;
            spriteRenderer.sortingOrder = 1;
            fleeDogCollider.enabled = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
            ifFlee = false;
    }
}
