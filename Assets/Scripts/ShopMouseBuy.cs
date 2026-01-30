using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopMouseBuy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hoverOver;
    bool purchaseError;
    Shop shop;
    MapSetup setup;
    // Start is called before the first frame update
    void Start()
    {
        purchaseError = false;
        hoverOver = false;
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        setup = GameObject.Find("SceneHandler").GetComponent<MapSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setup.isInBetweenRounds() && shop.itemsSelected())
        {
        if (shop.itemsSelected())
        {
            int option = shop.getOption();
            if(option != -1)
            {
                transform.gameObject.GetComponent<TextMeshProUGUI>().text = "Buy (" + shop.getItem(option).getPointCost() + ")";
            }
            else
            {
                transform.gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }

            if (Input.GetKeyDown(ControlAssignment.select2()) && hoverOver && shop.getItem(option) != null)
            {
                purchaseError = !shop.buyItem(option);
                Debug.Log("Attempt to buy item");
            }
            if(hoverOver && purchaseError)
            {
                transform.gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverOver = true;
    }

    public void OnPointerStay(PointerEventData eventData)
    {
        hoverOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverOver = false;
        transform.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        purchaseError = false;
    }
}
