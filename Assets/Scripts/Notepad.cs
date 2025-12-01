using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notepad : MonoBehaviour
{
    public string noteText;
    ControlAssignment control = new ControlAssignment();
    Player p;
    Color flashingColor;
    GameObject notepadUI;
    bool isNoteShowing;
    bool noteCollided;
    const float pickUpWaitTime = 0.2f;
    float tempWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        isNoteShowing = false;
        notepadUI = GameObject.Find("NotepadView");
        notepadUI.GetComponent<Image>().enabled = false;
        notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        //inventory = GameObject.Find("SceneHandler").GetComponent<Inventory>();
        flashingColor = Color.yellow;
        noteCollided = false;
        tempWaitTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
        GetComponent<SpriteRenderer>().color = flashingColor;

        if (tempWaitTime > 0)
        {
            tempWaitTime -= Time.deltaTime;
        }

        if (noteCollided && Input.GetKeyDown(control.pickUpOrDrop()) && !isNoteShowing && tempWaitTime <= 0)
        {
             Debug.Log("Showing notepad");
             tempWaitTime = pickUpWaitTime;
             isNoteShowing = true;
             notepadUI.GetComponent<Image>().enabled = true;
             notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = noteText;
             notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
                
        }
        else if (noteCollided && Input.GetKeyDown(control.pickUpOrDrop()) && isNoteShowing && tempWaitTime <= 0)
        {
            Debug.Log("Not showing notepad");
            tempWaitTime = pickUpWaitTime;
            isNoteShowing = false;
            notepadUI.GetComponent<Image>().enabled = false;
            notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else if (!noteCollided)
        {
            tempWaitTime = pickUpWaitTime;
            isNoteShowing = false;
            notepadUI.GetComponent<Image>().enabled = false;
            notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Collided with notepad");
            noteCollided = true;
            
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Collided with notepad");
            noteCollided = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Not collided with notepad");
            noteCollided = false;
  
        }
    }

}
