using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrenade : MonoBehaviour
{
    public float areaOfEffect;
    public float GrenadeExplosionTime;
    public int grenadeDamage;
/*    bool enemyDamaged = false;
    bool playerDamaged = false;
    bool obstaclesDamaged = false;
    bool dogDamaged = false;
    bool fleeingDogDamaged = false;
*/
    bool turretDamaged = false;

    public float screenShakeIntensity;
    public float screenShakeTime;

  //  public AudioSource audioSource;
    public AudioClip grenadeExplosion;

   // public SpriteRenderer spriteRenderer;


    // Update is called once per frame
    void Update()
    {
        if (GrenadeExplosionTime <= 0)
        {
            //turretDamaged == false
          //  audioSource.clip = grenadeExplosion;
            AudioSource.PlayClipAtPoint(grenadeExplosion, transform.position);
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].CompareTag("Enemy"))
                {
                    objectsToDamage[i].GetComponent<EnemyController>().dealingDamageToEnemy(-grenadeDamage);
                   // enemyDamaged = true;
                }
                if (objectsToDamage[i].CompareTag("Player"))
                {
                    objectsToDamage[i].GetComponent<PlayerController>().dealDamageToPlayer(-grenadeDamage);
                }
                if (objectsToDamage[i].CompareTag("Obstacles"))
                {
                    objectsToDamage[i].GetComponent<ObstacleHealth>().dealingDamageToObstacle(-grenadeDamage);
                    
                }
                if(objectsToDamage[i].CompareTag("Dog"))
                {
                    objectsToDamage[i].GetComponent<DogController>().dealDamageToDog(-grenadeDamage);
                    
                }
                if (objectsToDamage[i].CompareTag("FleeingDog"))
                {
                    objectsToDamage[i].GetComponent<FleeingDogController>().dealDamageToFleeingDog(-grenadeDamage);
                    
                }
                if (objectsToDamage[i].CompareTag("Turret") && turretDamaged == false)
                {
                    objectsToDamage[i].GetComponent<TurretController>().dealingDamageToTurret(-grenadeDamage);
                    
                }

            }
            CinemachineShake.Instance.shakeCamera(screenShakeIntensity, screenShakeTime);
       //     spriteRenderer.enabled = false;
            Destroy(gameObject);
        }
        else
        {
            GrenadeExplosionTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
