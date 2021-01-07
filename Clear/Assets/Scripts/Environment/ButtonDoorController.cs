using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorController : MonoBehaviour
{
    Vector2 lastPosition;
    float distanceTravelled;
    public float speed;
    Coroutine buttonDoorCoroutine;
    public AudioSource audioSource;
    public AudioClip slideDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonController.buttonActivationNumber == 2)
        {
            AudioSource.PlayClipAtPoint(slideDoorOpen, transform.position);
            //audioSource.PlayOneShot(slideDoorOpen);
            buttonDoorCoroutine = StartCoroutine(OpenButtonDoor());
            if (distanceTravelled > 9f)
            {
                StopCoroutine(buttonDoorCoroutine);
                ButtonController.buttonActivationNumber = 0;
            }
        }
       // StartCoroutine(OpenButtonDoor());
    }

    IEnumerator OpenButtonDoor()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        distanceTravelled += Vector2.Distance(lastPosition, transform.position);
        lastPosition = transform.position;
        yield return new WaitForSeconds(0.01f);
        
    }
}
