using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int[] ammo;
    const int maxAmmoType = 5;
    // Start is called before the first frame update
    void Start()
    {
        ammo = new int[maxAmmoType];
        for(int i = 0; i < maxAmmoType; i++)
        {
            ammo[i] = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getAmmoCount(int a)
    {
        return ammo[a];
    }

    public void addAmmo(int a, int addValue)
    {
        ammo[a] += addValue;
    }

    public void reduceAmmo(int a, int subValue)
    {
        if (ammo[a] < subValue)
        {
            ammo[a] = 0;
        }
        else
        {
            ammo[a] -= subValue;
        }
    }
}
