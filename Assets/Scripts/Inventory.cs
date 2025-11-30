using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int[] ammo;
    int[] capacity;
    bool keys;
    const int maxAmmoType = 3;
    // Start is called before the first frame update
    void Start()
    {
        ammo = new int[maxAmmoType];
        capacity = new int[maxAmmoType];
        keys = false;

        ammo[0] = 18;
        ammo[1] = 4;
        ammo[2] = 4;

        capacity[0] = 60;
        capacity[1] = 40;
        capacity[2] = 40;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getAmmoCount(int a)
    {
        return ammo[a];
    }

    public int getAmmoCapacity(int a)
    {
        return capacity[a];
    }

    public bool haveKeys()
    { 
        return keys;
    }

    public void setKeys(bool k)
    {
        keys = k;
    }

    public int getMaxAmmoType()
    {
        return maxAmmoType;
    }

    public void setAmmoCapacity(int a, int c)
    {
        capacity[a] = c;
    }

    public void addAmmo(int a, int addValue)
    {
        if (ammo[a] + addValue >= capacity[a])
        {
            ammo[a] = capacity[a];
        }
        else
        {
            ammo[a] += addValue;
        }
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
