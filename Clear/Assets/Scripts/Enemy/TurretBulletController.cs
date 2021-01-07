using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if(player != null)
        {
            player.dealDamageToPlayer(-20);
            Destroy(gameObject);
        }
        else if (!collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
