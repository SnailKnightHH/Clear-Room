using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public static WeaponSwitch Instance { get; private set; }
    public int selectedWeapon = 0;
    public Sprite characterWithRifle;
    public Sprite characterWithPistol;
    public PlayerController player;
    public AutomaticShooting automaticShooting;
    public Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        switchWeapon();
        //player = SpriteRenderer.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"{AutomaticShooting.Instance.rifleIsReloading}");
        // if(!AutomaticShooting.Instance.getRifleIsReloading && !Shooting.Instance.getIsReloading)
        //   {
        int previousSelectedWeapon = selectedWeapon;
        if (automaticShooting.getRifleIsReloading == false && shooting.getIsReloading == false) // weapons cant be switched during reload, prevent bug
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.spriteRenderer.sprite = characterWithPistol;
                selectedWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                player.spriteRenderer.sprite = characterWithRifle;
                selectedWeapon = 1;
            }
            if (previousSelectedWeapon != selectedWeapon)
            {

                switchWeapon();
            }
        }
        // }

    }

    void switchWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
