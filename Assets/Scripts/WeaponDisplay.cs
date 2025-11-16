using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    int weaponPosition;
    GameObject gunDisplay;
    GunTypes currentGun;
    Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform u in gameObject.transform)
        {
            u.gameObject.GetComponent<Image>().enabled = false;
        }

        currentGun = p.getCurrentGun();

        if (currentGun != null)
        {
            currentGun.getGunUI().gameObject.GetComponent<Image>().enabled = true;
            if (currentGun.outOfAmmo())
            {
                currentGun.getGunUI().GetComponent<Image>().color = Color.red;
            }
            else
            {
                currentGun.getGunUI().GetComponent<Image>().color = Color.white;
            }
        }
    }
}
