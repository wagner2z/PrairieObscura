using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarFix : MonoBehaviour
{
    Player p;
    Color flashingColor;
    TextMeshProUGUI subMessageUI;
    string[] parts; 
    int broughtCount;
    const int itemsNeeded = 4;
    Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        parts = new string[4] { "tire", "battery", "gas", "toolbox" };
        broughtCount = 0;
        p = GameObject.Find("Player").GetComponent<Player>();
        subMessageUI = GameObject.Find("SubMessage").GetComponent<TextMeshProUGUI>();
        subMessageUI.enabled = false;
        flashingColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkWon())
        {
            flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
            GetComponent<SpriteRenderer>().color = flashingColor;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public bool checkWon()
    {
        if (broughtCount == itemsNeeded && inventory.haveKeys())
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
        subMessageUI.text = "";
        if ((collision.collider.gameObject.tag == "Player") && p.carryingObject())
        {
            foreach (string part in parts)
            {
                if (p.carriedObject().name == part)
                {
                    if(part == "gas" && !p.carriedObject().GetComponent<Gas>().isFull())
                    {
                        subMessageUI.text = "The gas can is empty";
                    }
                    subMessageUI.text = "Brought the " + p.carriedObject().name;
                    subMessageUI.enabled = true;
                    p.removeCarryableObject();
                    broughtCount++;

                }
            }
        }
        if (inventory.haveKeys() && broughtCount != itemsNeeded)
        {
            subMessageUI.text += "Tried to start with the keys, but didn't go";
            subMessageUI.enabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            subMessageUI.text = "";
            subMessageUI.enabled = false;
        }
    }
}
