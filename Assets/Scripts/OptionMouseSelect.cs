using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionMouseSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int option;
    Studio_Logo_Cutscene title;
    // Start is called before the first frame update
    void Start()
    {
        title = GameObject.Find("SceneHandler").GetComponent<Studio_Logo_Cutscene>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        title.setOption(option);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
