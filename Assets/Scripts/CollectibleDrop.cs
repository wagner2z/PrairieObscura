using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDrop : MonoBehaviour
{
    int collectibleCount;
    GameObject collectibleDropped;
    Inventory inventory;
    Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        collectibleCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropRandomCollectible(Vector3 pos)
    {
        int tempR = Random.Range(0, collectibleCount);
        collectibleDropped = transform.GetChild(tempR).gameObject;
        while (collectibleDropped.GetComponent<Collectible>().getActive())
        {
            tempR = Random.Range(0, collectibleCount);
            collectibleDropped = transform.GetChild(tempR).gameObject;
        }
        if(collectibleDropped.GetComponent<Collectible>().getType() == "ammo")
        {
            List<GunTypes> unlockedGuns = p.getUnlockedGuns();
            tempR = Random.Range(0, unlockedGuns.Count);
            if (unlockedGuns[tempR].getAmmoInventoryPosition() == 0)
            {
                //set sprite
                collectibleDropped.GetComponent<Collectible>().setAmmoType(0);
            }
            else if (unlockedGuns[tempR].getAmmoInventoryPosition() == 1)
            {
                //set sprite
                collectibleDropped.GetComponent<Collectible>().setAmmoType(1);
            }
            else if (unlockedGuns[tempR].getAmmoInventoryPosition() == 2)
            {
                //set sprite
                collectibleDropped.GetComponent<Collectible>().setAmmoType(2);
            }
        }

        collectibleDropped.transform.position = pos;
        collectibleDropped.GetComponent<Collectible>().startSpawnTime();
        collectibleDropped.GetComponent<Collectible>().setActive(true);
        Debug.Log("Collectible dropped at" + pos.x + pos.y + pos.z);
    }
}
