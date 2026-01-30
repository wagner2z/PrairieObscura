using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo
{
    int ammoCount;
    int ammoType;

    public Ammo(int count, int type)
    {
        ammoCount = count;
        ammoType = type;
    }

    public int getAmmoCount()
    {
        return ammoCount;
    }

    public void setAmmoCount(int c)
    {
        ammoCount = c;
    }

    public int getAmmoType()
    {
        return ammoType;
    }

    public void setAmmoType(int t)
    {
        ammoType = t;
    }

    public string getAmmoName()
    {
        switch(ammoType)
        {
            case 0:
                return "Pistol";
            case 1:
                return "Rifle";
            case 2:
                return "Shotgun";
            default:
                return "";
        }
    }

    /*public Sprite getAmmoSprite()
    {

    }*/
}
