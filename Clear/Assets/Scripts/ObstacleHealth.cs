using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    public float obstacleMaxHealth;
    float obstacleCurrentHealth;


    // Start is called before the first frame update
    void Start()
    {
        obstacleCurrentHealth = obstacleMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void dealingDamageToObstacle(int damageAmount)
    {
        obstacleCurrentHealth = Mathf.Clamp(obstacleCurrentHealth + damageAmount, 0, obstacleMaxHealth);
        if (obstacleCurrentHealth <= 0)
        {
            //  CinemachineShake shake = GetComponent<CinemachineShake>();
            //shake.shakeCamera(3, 0.1f);
            CinemachineShake.Instance.shakeCamera(10, 0.8f);

            Destroy(gameObject);
        }
    }

/*    void shakeEffect()
    {
        if(gameObject == null)
        {
            Debug.Log("Wall Destroyed");
            CinemachineShake.Instance.shakeCamera(3, 0.4f);
        }
    }*/

}
