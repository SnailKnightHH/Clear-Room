using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class ThrowingGrenades : MonoBehaviour
{

    public GameObject handGrenadePrefab;
    public Transform throwingPoint;
    public int grenadesMaxCount;
    public int currentGrenadesCount;
    float holdDownStartTime;
    public float maxThrowingForce;


    // Start is called before the first frame update
    void Start()
    {
        currentGrenadesCount = grenadesMaxCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGrenadesCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                holdDownStartTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.G))
            {
                float thisHoldDownTime = Time.time - holdDownStartTime;
                GameObject grenade = Instantiate(handGrenadePrefab, throwingPoint.position, throwingPoint.rotation);
                Rigidbody2D grenadeRB = grenade.GetComponent<Rigidbody2D>();
                grenadeRB.AddForce(throwingPoint.up * calculateHoldDownForce(thisHoldDownTime), ForceMode2D.Impulse);
                currentGrenadesCount--;
            }
        }


    }

    float calculateHoldDownForce(float holdDownTime)
    {
        float maxHoldDownTime = 2f;
        float holdDownTimeNormalized = Mathf.Clamp01(holdDownTime / maxHoldDownTime);
        float force = maxThrowingForce * holdDownTimeNormalized;
        return force;
    }
}
