using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponNameUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponSwitch.Instance.selectedWeapon == 0)
            textMeshProUGUI.SetText("M1911");
        if (WeaponSwitch.Instance.selectedWeapon == 1)
            textMeshProUGUI.SetText("M4A1");
    }
}
