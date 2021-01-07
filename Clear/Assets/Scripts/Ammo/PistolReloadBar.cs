using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolReloadBar : MonoBehaviour
{
   // public static PistolReloadBar Instance { get; private set; }

    public Slider Reloadslider;
    public Image fill;
    public Transform playerReloadPosition;
    public Gradient gradient;

    public Shooting shooting;
    public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
     //   Instance = this;
        //   shooting = GetComponent<Shooting>();
        //canvasGroup = GetComponent<CanvasGroup>(); Not sure if needed
        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
       // Debug.Log(shooting.currentAmmo);
       // Debug.Log(canvasGroup.alpha);
        if (shooting != null)
        {
            pistolReloadBar();
        }
        
    }
    void pistolReloadBar()
    {

        if (shooting.getIsReloading == true)
        {
            // Debug.Log("Activated");
            canvasGroup.alpha = 1f;
        }
        if (canvasGroup.alpha == 1f)
        {
            if (shooting.getIsReloading == false)
            {
                canvasGroup.alpha = 0f;
                // Debug.Log("Place Arrived");
            }
            else
            {
                if (playerReloadPosition != null)
                {
                    transform.position = playerReloadPosition.position + transform.up * 2f;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

        }
    }

    public void setMaxReloadTime(float maxReloadTime)
    {
        Reloadslider.maxValue = maxReloadTime;
        Reloadslider.value = 0;
        fill.color = gradient.Evaluate(0f);

    }

    public void setReloadTime(float currentReloadTime)
    {
        Reloadslider.value = currentReloadTime;
        fill.color = gradient.Evaluate(Reloadslider.normalizedValue);
    }

}

