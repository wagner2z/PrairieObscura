using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Well : MonoBehaviour
{

    Player p;
    Color flashingColor;
    TextMeshProUGUI subMessageUI;
    string[] parts;
    int broughtCount;
    const int itemsNeeded = 2;
    bool keysDropped;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        parts = new string[2] { "bucket", "handle" };
        broughtCount = 0;
        p = GameObject.Find("Player").GetComponent<Player>();
        keysDropped = false;
        subMessageUI = GameObject.Find("SubMessage").GetComponent<TextMeshProUGUI>();
        subMessageUI.enabled = false;
        flashingColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if(broughtCount != itemsNeeded)
        {
            flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
            GetComponent<SpriteRenderer>().color = flashingColor;
        }
        else if(broughtCount == itemsNeeded && !keysDropped)
        {
            inventory.setKeys(true);
            keysDropped = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag == "Player"))
        {
            subMessageUI.text = "Need a handle and a bucket to be able to retrieve anything from the well";
            if (p.carryingObject())
            {
                foreach (string part in parts)
                {
                    if (p.carriedObject().name == part)
                    {
                        subMessageUI.text = "Brought the " + p.carriedObject().name;

                        p.removeCarryableObject();
                        broughtCount++;

                    }
                }
            }
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
