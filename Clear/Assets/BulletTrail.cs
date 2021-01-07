using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public Transform bulletTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = bulletTransform.position; // must have this to prevent bug. if no initial position is assigned, starting point would go to a random positon 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = bulletTransform.position;
    }
}
