using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunAmmo : MonoBehaviour
{
    TextMeshProUGUI ammoUI;
    Player p;
    GunTypes currentGun;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        ammoUI = gameObject.GetComponent<TextMeshProUGUI>();
        ammoUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        currentGun = p.getCurrentGun();

        if(currentGun == null)
        {
            ammoUI.text = "";
        }

        else
        {
            ammoUI.text = currentGun.getCurrentGunAmmo() + "/" + inventory.getAmmoCount(currentGun.getAmmoInventoryPosition());
        }

    }
}
