using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    GameObject shopMenu;
    Player p;
    const float offScreenX = -110f;
    const float offScreenY = 85f;
    const float spawnX = -34.5f;
    const float spawnY = -5.8f;
    int option;
    MapSetup setup;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        setup = GameObject.Find("SceneHandler").GetComponent<MapSetup>();
        option = -1;
        shopMenu = GameObject.Find("ShopMenu");
        transform.position = new Vector3(offScreenX, offScreenY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(setup.isInBetweenRounds())
        {
            transform.position = new Vector3(spawnX, spawnY, 0);
            // TODO: Instead of Player, create a new class for gun list
            //GunTypes randomNewGun = 
        }
        else
        {
            transform.position = new Vector3(offScreenX, offScreenY, 0);
        }
    }

    public int getOption()
    {
        return option;
    }
    public void setOption(int o)
    {
        option = o;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
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
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            foreach (Transform s in shopMenu.transform.GetChild(2))
            {
                s.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                s.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            option = -1;
        }
    }

}
