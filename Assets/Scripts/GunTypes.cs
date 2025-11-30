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
    int inventoryAmmoPos;
    int ammoPerShot;
    float gunReloadTime;
    bool gunAvailable;
    Transform gunUI;
    RuntimeAnimatorController animControl;
    bool singleHanded;

    public GunTypes(string name, int upgrade, int damage, int ammo, int ammoInventoryPosition, int perShot, float reload, int melee, bool available, Transform interfaceObj, RuntimeAnimatorController ac, bool hand)
    {
        gunName = name;
        gunUpgrade = upgrade;
        gunMaxDamage = damage;
        meleeDamage = melee;
        maxGunAmmo = ammo;
        currentAmmo = maxGunAmmo;
        inventoryAmmoPos = ammoInventoryPosition;
        ammoPerShot = perShot;
        gunReloadTime = reload;
        gunAvailable = available;
        gunUI = interfaceObj;
        animControl = ac;
        singleHanded = hand;
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

    public void setGunUpgrade(int u)
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

    public int getAmmoInventoryPosition()
    {
        return inventoryAmmoPos;
    }

    public void setAmmoInventoryPosition(int i)
    {
        inventoryAmmoPos = i;
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

    public int reload(int i)
    {
        int ammoDif = maxGunAmmo - currentAmmo;
        if (i >= ammoDif)
        {
            currentAmmo += ammoDif;
            return ammoDif;
        }
        else
        {
            currentAmmo += i;
            return i;
        }
        
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

    public bool isSingleHanded()
    {
        return singleHanded;
    }

    public void setSingleHanded(bool s) { 
        singleHanded = s;
    }

    public RuntimeAnimatorController getAnimator()
    {
        return animControl;
    }
}


