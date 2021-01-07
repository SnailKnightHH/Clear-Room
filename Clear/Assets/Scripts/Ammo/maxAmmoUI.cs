using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class maxAmmoUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public Shooting shooting;
    public AutomaticShooting automaticShooting;

    // Start is called before the first frame update
    void Start()
    {
   //     shooting = GetComponent<Shooting>();

    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponSwitch.Instance.selectedWeapon == 0)
            textMeshProUGUI.SetText(shooting.pistolTotalAmmo.ToString());
        if (WeaponSwitch.Instance.selectedWeapon == 1)
            textMeshProUGUI.SetText(automaticShooting.rifleTotalAmmo.ToString());
    }
}
