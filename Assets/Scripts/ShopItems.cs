using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems
{
    string itemName;
    int pointCost;
    Sprite itemSprite;
    //int itemType;
    //int itemPosition;

    public ShopItems(string name, int points, Sprite sprite)
    {
        itemName = name;
        pointCost = points;
        itemSprite = sprite;
    }

    public string getItemName()
    {
        return itemName;
    }

    public void setItemName(string n)
    {
        itemName = n;
    }

    public int getPointCost()
    {
        return pointCost;
    }

    public void setPointCost(int p)
    {
        pointCost = p;
    }

    public Sprite getItemSprite()
    {
        return itemSprite;
    }

    public void setItemSprite(Sprite s)
    {
        itemSprite = s;
    }

}
