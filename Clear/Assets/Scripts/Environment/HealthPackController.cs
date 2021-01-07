using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    public AudioClip HPRegenSighBreath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            player.dealDamageToPlayer(50);
            AudioSource.PlayClipAtPoint(HPRegenSighBreath, transform.position);
            Destroy(gameObject);
        }
    }
}
