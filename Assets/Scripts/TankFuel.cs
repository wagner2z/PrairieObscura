using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankFuel : MonoBehaviour
{
    Player p;
    Gas g;
    Color flashingColor;
    TextMeshProUGUI subMessageUI;
    string[] parts;
    int broughtCount;
    const int itemsNeeded = 2;
    bool tankDropped;
    Inventory inventory;

    void Start()
    {
        //inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        parts = new string[2] { "gas", "siphon" };
        broughtCount = 0;
        p = GameObject.Find("Player").GetComponent<Player>();
        g = GameObject.Find("gas").GetComponent<Gas>();
        tankDropped = false;
        subMessageUI = GameObject.Find("SubMessage").GetComponent<TextMeshProUGUI>();
        subMessageUI.enabled = false;
        flashingColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (broughtCount != itemsNeeded)
        {
            flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
            GetComponent<SpriteRenderer>().color = flashingColor;
        }
        else if (broughtCount == itemsNeeded && !tankDropped)
        {
            g.transform.position = new Vector3(-41f, 19f, 0f);
            g.GetComponent<Renderer>().enabled = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            tankDropped = true;
            g.GetComponent<Gas>().setFull();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.gameObject.tag == "Player"))
        {
            subMessageUI.text = "Can probably get some fuel out of this thing with a siphon and a container to put it in";
            if (p.carryingObject())
            {
                foreach (string part in parts)
                {
                    if (p.carriedObject().name == part)
                    {
                        if (part == "gas" && !p.carriedObject().GetComponent<Gas>().isFull())
                        {

                        }
                        else
                        {
                            subMessageUI.text = "Brought the " + p.carriedObject().name;
                            p.removeCarryableObject();
                            broughtCount++;
                        }

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
