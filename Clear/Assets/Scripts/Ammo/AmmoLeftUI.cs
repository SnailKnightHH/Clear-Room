using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoLeftUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProGui;
    public Shooting shooting;
    public AutomaticShooting automaticShooting;

    // Start is called before the first frame update
    /*    void Start()
        {
            shooting = GetComponent<Shooting>();
        }
    */

    private void Awake()
    {
        // shooting = GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponSwitch.Instance.selectedWeapon == 0)
            textMeshProGui.SetText(shooting.currentAmmo.ToString());
        if(WeaponSwitch.Instance.selectedWeapon == 1)
            textMeshProGui.SetText(automaticShooting.rifleCurrentAmmo.ToString());
    }
}

