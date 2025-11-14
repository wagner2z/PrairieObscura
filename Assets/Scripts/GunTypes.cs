using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GunTypes
{
    string gunName;
    int gunUpgrade; // 1 for base, 2 for first upgrade, 3 for second upgrade
    int gunMaxDamage;
    int gunAmmo;
    float gunReloadTime;
    bool gunAvailable;
    GameObject gunUI;

    public GunTypes(string name, int upgrade, int damage, int ammo, float reload, bool available, GameObject interfaceObj)
    {
        gunName = name;
        gunUpgrade = upgrade;
        gunMaxDamage = damage;
        gunAmmo = ammo;
        gunReloadTime = reload;
        gunAvailable = available;
        gunUI = interfaceObj;
    }

    public string getGunName() 
    {
        return gunName;
    }

    public void setGunName(string n)
    {
        gunName = n;
    }

    public int getGunUpgrade()
    {
        return gunUpgrade;
    }

    public void setGunUpograde(int u)
    {
        gunUpgrade = u;
    }

    public int getGunDamage()
    {
        return gunMaxDamage;
    }

    public void setGunDamage(int d)
    {
        gunMaxDamage = d;
    }

    public int getGunAmmo()
    {
        return gunAmmo;
    }

    public void setGunAmmo(int a)
    {
        gunAmmo = a;
    }

    public float getGunReloadTime()
    {
        return gunReloadTime;
    }

    public void setGunReloadTime(float r)
    {
        gunReloadTime = r;
    }

    public bool getGunAvailable()
    {
        return gunAvailable;
    }

    public void setGunAvailable(bool a)
    {
        gunAvailable = a;
    }

    public GameObject getGunUI()
    {
        return gunUI;
    }

    public void setGunUI(GameObject ui)
    {
        gunUI = ui;
    }
}


