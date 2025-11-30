using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Player p;
    Color flashingColor;
    public string type;
    int ammoType;
    const float offScreenX = -105f;
    const float offScreenY = 80f;
    const float spawnTime = 10f;
    float tempTime;
    bool isActive;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        flashingColor = Color.yellow;
        tempTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
        GetComponent<SpriteRenderer>().color = flashingColor;

        if(isActive && (gameObject.tag != "CarryObject"))
        {
            if(tempTime > 0f)
            {
                tempTime -= Time.deltaTime;
            }
            else
            {
                gameObject.transform.position = new Vector3(offScreenX, offScreenY, 0);
                isActive = false;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (gameObject.tag != "CarryObject")
            {
                if(type == "medical")
                {
                    p.addHP(p.getMaxHP()/Random.Range(2, 5));
                }
                else if(type == "ammo")
                {
                    inventory.addAmmo(ammoType, Random.Range(1, 6));
                }
                gameObject.transform.position = new Vector3(offScreenX, offScreenY, 0);
                isActive = false;
            }
        }
    }

    public string getType()
    {
        return type;
    }

    public void setAmmoType(int t)
    {
        ammoType = t;
    }

    public void setActive(bool a)
    {
        isActive = a;
    }

    public void startSpawnTime()
    {
        tempTime = spawnTime;
    }

    public bool getActive()
    {
        return isActive;
    }
}
