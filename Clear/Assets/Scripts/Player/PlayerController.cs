using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Variables
    // Player Movement 
    float horizontal;
    float vertical;
    Rigidbody2D rigidBody2D;
    public float speed;
    public float shiftSpeed;

    // Player Shooting

    public Camera cam;
    Vector2 mousePosition;

    // Player Health
    public int playerMaxHealth;
    int playerCurrentHealth;

    // HealthBar UI 
    public HealthBar healthBar;

    public Transform firePoint;

    // Sprites
    public SpriteRenderer spriteRenderer;
    public Sprite pistolForm;

    // mesh (FOV)
    [SerializeField] private FieldOfView fieldOfView;
    float angle;

    // Post-Processing
    public Volume volume;
    private Vignette vignette;

    // Audio
    public AudioSource audioSource;
    public AudioClip heartBeat;

    bool canMove = true;    // if character died he can no longer move 

    bool ifSoundPlayed = false;

    // lighting
    public Light2D playerFlashLight;
    public Light2D playerSurroudingLight;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = pistolForm;
        Cursor.visible = false;
        volume.profile.TryGet(out vignette);
        vignette.color.value = Color.red;
        vignette.intensity.value = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        // Player Movement: Get Axies
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Record Mouse Position
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); // ScreenToWorldPoint not the other way around! 

        fieldOfView.setOrigin(transform.position);

        if (Input.GetMouseButton(1))
        {
            fieldOfView.setFOV(40f);
            fieldOfView.setViewDistance(20f);
            fieldOfView.setAimDirection(angle + 130);
        }
        else
        {
            fieldOfView.setFOV(90f);
            fieldOfView.setViewDistance(10f);
        }

        if (playerCurrentHealth <= 40)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.55f, 1.5f * Time.deltaTime);
            if (ifSoundPlayed == false)
            {
                audioSource.PlayOneShot(heartBeat);
                ifSoundPlayed = true;
            }

            //  Debug.Log(vignette.intensity.value);
        }
        else
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, 1 * Time.deltaTime);
            audioSource.Stop();
            ifSoundPlayed = false;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            playerFlashLight.intensity = 1 - playerFlashLight.intensity;
            playerSurroudingLight.intensity = 0.5f - playerSurroudingLight.intensity;
        }
        if (playerCurrentHealth <= 0)
        {
            playerFlashLight.intensity = 0;
            playerSurroudingLight.intensity = 0;
        }

    }


    private void FixedUpdate()
    {
        // Move Player 
        Vector2 position = rigidBody2D.position;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Debug.Log("Input detected");
                position.x += horizontal * shiftSpeed * Time.deltaTime;
                position.y += vertical * shiftSpeed * Time.deltaTime;
            }
            else
            {
                // Debug.Log("else");
                position.x += horizontal * speed * Time.deltaTime;
                position.y += vertical * speed * Time.deltaTime;
            }
            rigidBody2D.MovePosition(position);
        }


        // Aim (Mouse Direction)
        Vector2 lookDir = mousePosition - rigidBody2D.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90.0f;
        //rigidBody2D.rotation = angle;  

        transform.eulerAngles = new Vector3(0, 0, angle);

        fieldOfView.setAimDirection(angle - 180);

        // Debug.DrawLine((Vector2)firePoint.position, (Vector2)firePoint.position + lookDir.normalized * 1.5f);
    }


    public void dealDamageToPlayer(int damageAmount)
    {
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth + damageAmount, 0, playerMaxHealth);
        healthBar.SetHealth(playerCurrentHealth);
        if (playerCurrentHealth <= 0)
        {
            spriteRenderer.enabled = false;
            canMove = false;
            StartCoroutine(waitBeforeNewScene());
            //Destroy(gameObject);
        }
    }

    IEnumerator waitBeforeNewScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

}
