using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    PointCounter point;
    GameObject shopMenu;
    Player p;
    Inventory inventory;
    public Sprite handgunAmmo;
    public Sprite rifleAmmo;
    public Sprite shotgunAmmo;
    public Sprite healthPackSprite;
    const float offScreenX = -110f;
    const float offScreenY = 85f;
    const float spawnX = 34.5f;
    const float spawnY = -5.8f;
    int option;
    bool selectShopItems;
    MapSetup setup;
    int randomPosition;
    GunTypes randomNewGun;
    GunTypes randomUpgrade1;
    GunTypes randomUpgrade2;
    GunTypes randomUpgrade3;
    Ammo randomAmmo1;
    Ammo randomAmmo2;
    HealingItem healthPack;
    ShopItems[] items;
    const int itemCount = 7;
    const int hpHeal = 5;

    // Start is called before the first frame update
    void Start()
    {
        items = new ShopItems[itemCount];
        p = GameObject.Find("Player").GetComponent<Player>();
        setup = GameObject.Find("SceneHandler").GetComponent<MapSetup>();
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        point = GameObject.Find("Point Counter").GetComponent<PointCounter>();
        option = -1;
        shopMenu = GameObject.Find("ShopMenu");
        transform.position = new Vector3(offScreenX, offScreenY, 0);
        selectShopItems = false;
        randomNewGun = null;
        randomUpgrade1 = null;
        randomUpgrade2 = null;
        randomUpgrade3 = null;
        randomAmmo1 = null;
        randomAmmo2 = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(setup.isInBetweenRounds())
        {
            transform.position = new Vector3(spawnX, spawnY, 0);
            // TODO: Instead of Player, create a new class for gun list
            if (!selectShopItems)
            {
                randomNewGun = null;
                randomUpgrade1 = null;
                randomUpgrade2 = null;
                randomUpgrade3 = null;
                randomAmmo1 = null;
                randomAmmo2 = null;

                List<GunTypes> sellableGuns = p.getNewLockedGuns();
                if (sellableGuns.Count > 0)
                {
                    randomPosition = Random.Range(0, sellableGuns.Count);
                    randomNewGun = sellableGuns[randomPosition];
                    items[0] = new ShopItems(randomNewGun.getGunName(), 150, randomNewGun.getGunUI().gameObject.GetComponent<Image>().sprite);
                }

                List<GunTypes> sellableUpgrades = p.getUpgradeLockedGuns();
                if (sellableUpgrades.Count > 0)
                {
                    randomPosition = Random.Range(0, sellableUpgrades.Count);
                    randomUpgrade1 = sellableUpgrades[randomPosition];
                    sellableUpgrades.RemoveAt(randomPosition);
                    items[1] = new ShopItems(randomUpgrade1.getGunName() + " V" + randomUpgrade1.getGunUpgrade(), 25 + (50 * (randomUpgrade1.getGunUpgrade() - 1)), randomUpgrade1.getGunUI().gameObject.GetComponent<Image>().sprite);
                }
                if (sellableUpgrades.Count > 0)
                {
                    randomPosition = Random.Range(0, sellableUpgrades.Count);
                    randomUpgrade2 = sellableUpgrades[randomPosition];
                    sellableUpgrades.RemoveAt(randomPosition);
                    items[2] = new ShopItems(randomUpgrade2.getGunName() + " V" + randomUpgrade2.getGunUpgrade(), 25 + (50 * (randomUpgrade2.getGunUpgrade() - 1)), randomUpgrade2.getGunUI().gameObject.GetComponent<Image>().sprite);
                }
                if (sellableUpgrades.Count > 0)
                {
                    randomPosition = Random.Range(0, sellableUpgrades.Count);
                    randomUpgrade3 = sellableUpgrades[randomPosition];
                    items[3] = new ShopItems(randomUpgrade2.getGunName() + " V" + randomUpgrade2.getGunUpgrade(), 25 + (50 * (randomUpgrade3.getGunUpgrade() - 1)), randomUpgrade3.getGunUI().gameObject.GetComponent<Image>().sprite);
                }

                // select Ammo and health
                List<GunTypes> unlockedGuns = p.getUnlockedGuns();
                if (unlockedGuns.Count > 0)
                {
                    randomPosition = Random.Range(0, unlockedGuns.Count);
                    randomAmmo1 = new Ammo(10, unlockedGuns[randomPosition].getAmmoInventoryPosition());
                    items[4] = new ShopItems(randomAmmo1.getAmmoName() + " Ammo (" + randomAmmo1.getAmmoCount() + ")", randomAmmo1.getAmmoCount() * 5, getAmmoSprite(randomAmmo1.getAmmoType()));
                }
                if (unlockedGuns.Count > 0)
                {
                    randomPosition = Random.Range(0, unlockedGuns.Count);
                    randomAmmo2 = new Ammo(10, unlockedGuns[randomPosition].getAmmoInventoryPosition());
                    items[5] = new ShopItems(randomAmmo2.getAmmoName() + " Ammo (" + randomAmmo2.getAmmoCount() + ")", randomAmmo2.getAmmoCount() * 5, getAmmoSprite(randomAmmo2.getAmmoType()));
                }
                items[6] = new ShopItems("Health Pack (" + hpHeal + "HP)", 30, healthPackSprite);
                selectShopItems = true;
            }
        }
        else
        {
            transform.position = new Vector3(offScreenX, offScreenY, 0);
            selectShopItems = false;
        }
    }

    /*public string getShopObjectName(int option)
    {
        switch (option)
        {
            case 0: 
        }
    }*/

    public int getOption()
    {
        return option;
    }
    public void setOption(int o)
    {
        option = o;
    }

    public bool itemsSelected()
    {
        return selectShopItems;
    }

    public ShopItems getItem(int i)
    {
        return items[i];
    }

    public bool buyItem(int o)
    {
        if (point.getPoints() >= items[o].getPointCost())
        {
            point.decreasePoints(items[o].getPointCost());
            switch (o)
            {
                case 0:
                    p.unlockGun(randomNewGun.getGunName());
                    p.unequipWeapon();
                    items[0] = null;
                    break;
                case 1:
                    p.upgradeGun(randomUpgrade1.getGunName(), randomUpgrade1.getGunUpgrade());
                    p.unequipWeapon();
                    items[1] = null;
                    break;
                case 2:
                    p.upgradeGun(randomUpgrade2.getGunName(), randomUpgrade2.getGunUpgrade());
                    p.unequipWeapon();
                    items[2] = null;
                    break;
                case 3:
                    p.upgradeGun(randomUpgrade3.getGunName(), randomUpgrade3.getGunUpgrade());
                    p.unequipWeapon();
                    items[3] = null;
                    break;
                case 4:
                    inventory.addAmmo(randomAmmo1.getAmmoType(), randomAmmo1.getAmmoCount());
                    items[4] = null;
                    break;
                case 5:
                    inventory.addAmmo(randomAmmo1.getAmmoType(), randomAmmo2.getAmmoCount());
                    items[5] = null;
                    break;
                case 6:
                    p.addHP(hpHeal);
                    items[6] = null;
                    break;
                default:
                    break;
            }
            option = -1;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Sprite getAmmoSprite(int a)
    {
        switch(a)
        {
            case 0:
                return handgunAmmo;
            case 1:
                return rifleAmmo;
            case 2:
                return shotgunAmmo;
            default:
                return handgunAmmo;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //shopMenu.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            shopMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            foreach (Transform s in shopMenu.transform.GetChild(2))
            {
                s.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            shopMenu.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            shopMenu.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            foreach (Transform s in shopMenu.transform.GetChild(2))
            {
                s.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                s.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            option = -1;
        }
    }

}
