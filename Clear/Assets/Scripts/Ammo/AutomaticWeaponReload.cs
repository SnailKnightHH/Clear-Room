using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticWeaponReload : MonoBehaviour
{
    public Slider Reloadslider;
    public Image fill;
    public Transform playerReloadPosition;
    public Gradient gradient;

    public AutomaticShooting automaticShooting;
    public CanvasGroup canvasGroup;
    public GameObject rifle;

    // Start is called before the first frame update
    void Start()
    {
        // automaticShooting = GetComponent<AutomaticShooting>();
        //canvasGroup = GetComponent<CanvasGroup>(); Not sure if needed

        canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(GameObject.Find("Rifle") != null) // When the game starts, rifle is disabled, so rifleCurrentAmmo is not <= 0, so the reload bar would appear 
        {
            canvasGroup.alpha = 0f;
            rifleReloadBarIfDisplay();
        }

    }
   
    void rifleReloadBarIfDisplay()
    {
       // Debug.Log(automaticShooting.rifleCurrentAmmo);
      //  canvasGroup.alpha = 0f;
        if (automaticShooting.getRifleIsReloading == true)
        {
            // Debug.Log("Activated");
            canvasGroup.alpha = 1f;
        }
        if (canvasGroup.alpha == 1f)
        {
            if (automaticShooting.getRifleIsReloading == false)
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
