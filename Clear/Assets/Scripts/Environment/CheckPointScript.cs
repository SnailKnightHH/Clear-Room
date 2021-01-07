using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] private Transform RespawnPoint;
    bool checkPointReached = false;
    [SerializeField] private GameObject playerGameobject;
    [SerializeField] private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPointReached)
        {
            if (playerGameobject.activeSelf == false)
            {
                playerGameobject.transform.position = RespawnPoint.position;
                playerGameobject.SetActive(true);
                playerController.dealDamageToPlayer(10);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            checkPointReached = true;
        }
    }
}
