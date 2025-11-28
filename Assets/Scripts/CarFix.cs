using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFix : MonoBehaviour
{
    Player p;
    string[] parts; 
    int broughtCount;
    const int itemsNeeded = 5;
    // Start is called before the first frame update
    void Start()
    {
        parts = new string[5] { "tire", "battery", "fuel", "keys", "dice" };
        broughtCount = 0;
        p = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool checkWon()
    {
        if (broughtCount == itemsNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag == "Player") && p.carryingObject())
        {
            foreach (string part in parts)
            {
                if (p.carriedObject().name == part)
                {
                    Debug.Log("Brought the " + p.carriedObject().name);
                    p.removeCarryableObject();
                    broughtCount++;

                }
            }
        }
    }
}
