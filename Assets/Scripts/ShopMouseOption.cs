using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopMouseSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int option;
    Shop shop;
    MapSetup setup;
    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        setup = GameObject.Find("SceneHandler").GetComponent<MapSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.gameObject.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        shop.setOption(option);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        shop.setOption(-1);
    }
}
