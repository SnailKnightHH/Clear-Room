using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Transform playerTransform;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textMeshProUGUI;
    public AudioClip laserExplosion; 

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) < 6f)
        {
            canvasGroup.alpha = 1;
            textMeshProUGUI.SetText("Press E To Dismantle");
            if (Input.GetKeyDown(KeyCode.E))
            {
                canvasGroup.alpha = 0;
                Destroy(gameObject);
            }

        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.dealDamageToPlayer(-1000);
            AudioSource.PlayClipAtPoint(laserExplosion, transform.position);
            Destroy(gameObject);
        }
    }
}
