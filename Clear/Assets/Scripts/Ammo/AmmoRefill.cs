using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoRefill : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public Transform playerTransform;
    public CanvasGroup canvasGroup;
    public PlayerController playerController;
    public Shooting shooting;
    public AutomaticShooting automaticShooting;
    public ThrowingGrenades throwingGrenades;
    public float refillTime;
    public bool isRefilling { get; private set; }
    public RefillBar refillBar;
    public GameObject rifle;
    public AudioSource audioSource;
    public AudioClip refillSFX;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f;
        textMeshProUGUI.SetText("Press E to Refill Ammo");
        isRefilling = false;
        refillBar.setMaxRefillTime(refillTime);
    }

    // Update is called once per frame
    void Update()
    {
        /*        if (Vector2.Distance(playerTransform.position, gameObject.transform.position) < distanceForDisplaying)
                {
                    canvasGroup.alpha = 1f;
                }
                else
                {
                    canvasGroup.alpha = 0f;
                }*/

        //  RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, (Vector2)firePoint.position + lookDir.normalized, 1.5f);
       
        if (Vector2.Distance(playerController.transform.position, transform.position) < 3f)
        {
            StartCoroutine(reFillAmmo());
        }
        else
        {
            canvasGroup.alpha = 0f;
        }
    }
    IEnumerator reFillAmmo()
    {
        canvasGroup.alpha = 1f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRefilling = true;
            audioSource.PlayOneShot(refillSFX);
            for (float i = 0; i < refillTime; i += 0.1f)
            {
                float progress = Mathf.Clamp01(i / 0.9f);
                refillBar.setRefillTime(progress);
                yield return new WaitForSeconds(0.1f);
            }
            shooting.currentAmmo = shooting.maxAmmo;
            shooting.pistolTotalAmmo = shooting.pistolTotalAmmoCopy;
            Debug.Log(automaticShooting);
/*            if(GameObject.Find("Rifle") != null) // rifle total ammo will become zero if rifle is not active at the first time of refill 
            {    */                                // same as automatic weapon reload
            // well actually I think somehow this doesn't matter any more, it now works perfectly
                automaticShooting.rifleCurrentAmmo = automaticShooting.rifleMaxAmmo;
                automaticShooting.rifleTotalAmmo = automaticShooting.rifleTotalAmmoCopy;
            // }

            throwingGrenades.currentGrenadesCount = throwingGrenades.grenadesMaxCount;

            isRefilling = false;
        }

    }


}
