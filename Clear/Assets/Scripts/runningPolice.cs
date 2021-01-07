using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runningPolice : MonoBehaviour
{
    public float policeSpeed;
    public PoliceSpawner policeSpawner;

    Vector2 runningPolicePosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, -1);
        transform.eulerAngles = new Vector3(0, 0, -90);
        runningPolicePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      //  Vector2 position = rigidBody2D.position;
        
        runningPolicePosition.x += 1 * policeSpeed * Time.deltaTime;
        transform.position = runningPolicePosition;
        if(transform.position.x > 30)
        {
            policeSpawner.numberOfPolice--;
            Debug.Log(policeSpawner.numberOfPolice);
            Destroy(gameObject);
            
        }

       // rigidBody2D.MovePosition(position);
    }

    /*IEnumerator movePolice()
    {
        while (transform.position.x < 30)
        {
            runningPolicePosition.x += 1;
            transform.position = runningPolicePosition;
            yield return new WaitForSeconds(policeSpeedWaitForSeconds);
        }
    }*/
}
