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
        
    }

    // Update is called once per frame
    void Update()
    {
        flashingColor = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time, 1));
        GetComponent<SpriteRenderer>().color = flashingColor;

        if (noteCollided)
        {
            if (Input.GetKeyDown(control.pickUpOrDrop()) && !isNoteShowing)
            {
                isNoteShowing = true;
                notepadUI.GetComponent<Image>().enabled = true;
                notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = noteText;
                notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
            }

            else if (Input.GetKeyDown(control.pickUpOrDrop()) && isNoteShowing)
            {
                isNoteShowing = false;
                notepadUI.GetComponent<Image>().enabled = false;
                notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        else
        {
            isNoteShowing = false;
            notepadUI.GetComponent<Image>().enabled = false;
            notepadUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            noteCollided = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            noteCollided = false;

        }
    }

}
