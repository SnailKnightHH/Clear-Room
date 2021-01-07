using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject police;
    public int numberOfPolice { get; set; }
   
    // Start is called before the first frame update
    void Start()
    {
        numberOfPolice = 0;
/*        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPolicePosition = new Vector3(Random.Range(-50, -25), Random.Range(-3, -12), 0);
           // randomPolicePosition.x = Random.Range(-50, -25);
         //   randomPolicePosition.y = Random.Range(-3, -12);
         //   transform.position.x = randomPolicePosition.x;
            Debug.Log(randomPolicePosition);
            Instantiate(police, randomPolicePosition, Quaternion.identity);

        }*/
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(numberOfPolice);
        if (numberOfPolice < 6)
        {
            StartCoroutine(spawningNPCPolice());
            numberOfPolice++;
            Debug.Log(numberOfPolice);
        }
       // Instantiate(police, randomPolicePosition);
    }

    IEnumerator spawningNPCPolice()
    {
            float randomSpawningInterval = Random.Range(0.5f, 1.5f);
            Vector3 randomPolicePosition = new Vector3(Random.Range(-50, -25), Random.Range(-3, -12), 0);
            Instantiate(police, randomPolicePosition, Quaternion.identity);
            yield return new WaitForSeconds(randomSpawningInterval);
    }
}
