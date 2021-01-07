using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockDoorController : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textMeshProUGUI;
    public Transform playerTransform;
    //public KeyController keyController;
    public Transform rotatePoint;
    public float rotateSpeed;
    bool isOpen = false;
    float angleRotated = 0;

    public AudioSource audioSource;
    public AudioClip doorLocked;
    public AudioClip doorUnlocked;
    public AudioClip doorClosed;
    bool doorCanInteract = true; // same with breakable doors; players can't interact with doors while opening / closing, prevent bug

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(keyController.keyAvaiable);
        if (Vector2.Distance(transform.position, playerTransform.position) < 5f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOpen == false)
                {
                    if (KeyController.keyAvaiable <= 0)
                    {
                        audioSource.PlayOneShot(doorLocked);
                        canvasGroup.alpha = 1;
                        textMeshProUGUI.SetText("Key Required");
                    }
                    else
                    {
                        if (doorCanInteract)
                        {
                            audioSource.PlayOneShot(doorUnlocked);
                            StartCoroutine(OpenLockDoor());
                            isOpen = true;
                            KeyController.keyAvaiable--;
                        }

                    }

                }
                else
                {
                    if (doorCanInteract)
                    {
                        audioSource.PlayOneShot(doorClosed);
                        StartCoroutine(CloseLockDoor());
                        isOpen = false;
                    }


                }
            }
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

    IEnumerator OpenLockDoor()
    {
        doorCanInteract = false;
        while (angleRotated <= 90)
        {
            transform.RotateAround(rotatePoint.position, Vector3.forward, rotateSpeed);
            angleRotated += rotateSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        angleRotated = 0;
        doorCanInteract = true;
    }

    IEnumerator CloseLockDoor()
    {
        doorCanInteract = false;
        while (angleRotated <= 90)
        {
            transform.RotateAround(rotatePoint.position, Vector3.forward, -rotateSpeed);
            angleRotated += rotateSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        angleRotated = 0;
        doorCanInteract = true;
    }
}
