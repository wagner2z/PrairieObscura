using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTypes
{
    string gunName;
    int gunUpgrade; // 1 for base, 2 for first upgrade, 3 for second upgrade
    int gunMaxDamage;
    int meleeDamage;
    int maxGunAmmo;
    int currentAmmo;
    int ammoPerShot;
    float gunReloadTime;
    bool gunAvailable;
    Transform gunUI;
    RuntimeAnimatorController animControl;

    public GunTypes(string name, int upgrade, int damage, int ammo, int perShot, float reload, int melee, bool available, Transform interfaceObj, RuntimeAnimatorController ac)
    {
        gunName = name;
        gunUpgrade = upgrade;
        gunMaxDamage = damage;
        meleeDamage = melee;
        maxGunAmmo = ammo;
        currentAmmo = maxGunAmmo;
        ammoPerShot = perShot;
        gunReloadTime = reload;
        gunAvailable = available;
        gunUI = interfaceObj;
        animControl = ac;
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

    public int getMeleeDamage()
    {
        return meleeDamage;
    }

    public void setMeleeDamage(int m)
    {
        meleeDamage = m;
    }

    public int getMaxGunAmmo()
    {
        return maxGunAmmo;
    }

    public int getCurrentGunAmmo()
    {
        return currentAmmo;
    }

    public void reduceAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo -= ammoPerShot;
        }
    }

    public bool outOfAmmo()
    {
        if(currentAmmo == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void reload()
    {
        currentAmmo = maxGunAmmo;
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

    public Transform getGunUI()
    {
        return gunUI;
    }

    public void setGunUI(Transform ui)
    {
        gunUI = ui;
    }

    public RuntimeAnimatorController getAnimator()
    {
        return animControl;
    }
}


