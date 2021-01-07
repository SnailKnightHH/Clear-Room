using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class breakableDoors : MonoBehaviour
{
    public Transform player;
    public int rotateSpeed;
    public Transform rotatePoint;
    public TextMeshProUGUI textMeshProUGUI;
    public CanvasGroup canvasGroup;
    public bool doorIsOpened = false;
    int angleRotated = 0;
    public PlayerController playerController;
    public AudioSource audioSource;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    bool doorCanInteract = true;


    // Start is called before the first frame update
    void Start()
    {
        // transform.eulerAngles = new Vector3(0, 0, -90);
        // rotatePoint = transform.position + transform.up * 4f;
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerController.transform.position) < 4f)
        {
            if (doorIsOpened == false)
            {
                canvasGroup.alpha = 1f;
                textMeshProUGUI.SetText("Press E To Open Door");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (doorCanInteract)
                    {
                        audioSource.PlayOneShot(openDoor);
                        StartCoroutine(OpenDoor());
                        doorIsOpened = true;
                    }

                }

            }
            else
            {
                canvasGroup.alpha = 1f;
                textMeshProUGUI.SetText("Press E To Close Door");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (doorCanInteract)
                    {
                        audioSource.PlayOneShot(closeDoor);
                        StartCoroutine(CloseDoor());
                        doorIsOpened = false;
                    }

                }

            }


        }
        else
        {
            canvasGroup.alpha = 0f;
        }
    }

    /*    public IEnumerator OpenDoor()     // outdated, use the new ones below 
        {
            while (transform.eulerAngles.z <= 90)
            {
                //transform.Rotate(0, 0, rotateSpeed);
                transform.RotateAround(rotatePoint, Vector3.forward, rotateSpeed);
                yield return new WaitForSeconds(0.01f);
            }
        }*/

    /*    public IEnumerator CloseDoor()
        {
            while (transform.eulerAngles.z > 1) // if it's 0 it somehow does not work for the first iteration lol
            {
                //transform.Rotate(0, 0, rotateSpeed);
                transform.RotateAround(rotatePoint, Vector3.forward, -rotateSpeed);
                yield return new WaitForSeconds(0.01f);
            }
        }
    */


    public IEnumerator OpenDoor()
    {
        doorCanInteract = false;  // so that while door is opening player can't interact with it, prevent bug
        while (angleRotated <= 90)
        {
            transform.RotateAround(rotatePoint.position, Vector3.forward, rotateSpeed);
            angleRotated += rotateSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        angleRotated = 0;
        doorCanInteract = true;
    }

    public IEnumerator CloseDoor()
    {
        doorCanInteract = false;
        while (angleRotated <= 90)
        {
            transform.RotateAround(rotatePoint.position, Vector3.forward, -rotateSpeed);
            angleRotated += rotateSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        doorCanInteract = true;
        angleRotated = 0;
    }


}
